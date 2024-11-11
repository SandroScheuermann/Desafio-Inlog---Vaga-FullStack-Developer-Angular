import { Component, Output, EventEmitter } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { VehicleService } from '../services/vehicle.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vehicles-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule, CommonModule],
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent {
  @Output() vehicleSelected = new EventEmitter<any>();

  displayedColumns: string[] = ['chassi', 'placa', 'tipoVeiculo', 'cor', 'distancia'];
  vehicles: any[] = [];
  selectedVehicle: any = null;
  tipoVeiculos: { [key: number]: string } = {};

  constructor(private vehicleService: VehicleService) { }

  ngOnInit(): void {

    this.getUserLocation();
    this.loadVehicles();

    this.vehicleService.veiculoAdicionado$.subscribe(() => {
      this.getUserLocation();
      this.loadVehicles();
    });

    this.vehicleService.telemetriaAdicionada$.subscribe(() => {
      this.getUserLocation();
      this.loadVehicles();
    });

    this.vehicleService.obterTipoVeiculo().subscribe((data) => {
      this.tipoVeiculos = data.reduce((acc, curr) => {
        acc[curr.value] = curr.label;
        return acc;
      }, {} as { [key: number]: string });
    });

  }

  private getUserLocation(): void {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const latitude = position.coords.latitude;
          const longitude = position.coords.longitude;
          this.loadVehicles(latitude, longitude);
        },
        (error) => {
          console.error("Erro ao obter localização do usuário:", error);
          this.loadVehicles();
        }
      );
    } else {
      console.error("Geolocalização não é suportada pelo navegador.");
      this.loadVehicles();
    }
  }

  private loadVehicles(userLatitude?: number, userLongitude?: number) {
    this.vehicleService.listarVeiculos().subscribe({
      next: (data) => {
        this.vehicles = data.veiculos.map((vehicle: any) => {
          if (
            vehicle.ultimaTelemetria.latitude !== 0 &&
            vehicle.ultimaTelemetria.longitude !== 0 &&
            userLatitude !== undefined &&
            userLongitude !== undefined
          ) {
            vehicle.distance = this.calculateDistance(
              userLatitude,
              userLongitude,
              vehicle.ultimaTelemetria.latitude,
              vehicle.ultimaTelemetria.longitude
            );
          } else {
            vehicle.distance = null;
          }

          console.log(`Veículo ${vehicle.chassi}:`, vehicle.distance);
          return vehicle;
        });

        this.vehicles.sort((a, b) => (a.distance ?? Infinity) - (b.distance ?? Infinity));
      },
      error: (err) => {
        console.error('Erro ao listar veículos:', err);
      }
    });
  }


  private calculateDistance(lat1: number, lon1: number, lat2: number, lon2: number): number {
    const R = 6371e3;
    const radLat1 = this.toRadians(lat1);
    const radLat2 = this.toRadians(lat2);
    const deltaLat = this.toRadians(lat2 - lat1);
    const deltaLon = this.toRadians(lon2 - lon1);

    const a =
      Math.sin(deltaLat / 2) * Math.sin(deltaLat / 2) +
      Math.cos(radLat1) * Math.cos(radLat2) * Math.sin(deltaLon / 2) * Math.sin(deltaLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    return R * c;
  }

  private toRadians(degrees: number): number {
    return degrees * (Math.PI / 180);
  }

  onVehicleSelected(vehicle: any) {
    this.vehicleService.selecionarVeiculo(vehicle);
  }

  getTipoVeiculoLabel(tipo: number): string {
    return this.tipoVeiculos[tipo] || 'Desconhecido';
  }
}

