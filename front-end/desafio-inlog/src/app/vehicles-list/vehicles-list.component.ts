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
            vehicle.distance = null; // Define como null se os dados de telemetria não forem válidos
          }

          console.log(`Veículo ${vehicle.chassi}:`, vehicle.distance);
          return vehicle;
        });

        // Ordena os veículos pela distância, se disponível
        this.vehicles.sort((a, b) => (a.distance ?? Infinity) - (b.distance ?? Infinity));
      },
      error: (err) => {
        console.error('Erro ao listar veículos:', err);
      }
    });
  }


  private calculateDistance(lat1: number, lon1: number, lat2: number, lon2: number): number {
    const R = 6371e3; // Raio da Terra em metros
    const radLat1 = this.toRadians(lat1);
    const radLat2 = this.toRadians(lat2);
    const deltaLat = this.toRadians(lat2 - lat1);
    const deltaLon = this.toRadians(lon2 - lon1);

    const a =
      Math.sin(deltaLat / 2) * Math.sin(deltaLat / 2) +
      Math.cos(radLat1) * Math.cos(radLat2) * Math.sin(deltaLon / 2) * Math.sin(deltaLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));

    return R * c; // Retorna a distância em metros
  }

  private toRadians(degrees: number): number {
    return degrees * (Math.PI / 180);
  }

  selectVehicle(vehicle: any) {
    this.selectedVehicle = vehicle;
    this.vehicleSelected.emit(vehicle);
  }
}

