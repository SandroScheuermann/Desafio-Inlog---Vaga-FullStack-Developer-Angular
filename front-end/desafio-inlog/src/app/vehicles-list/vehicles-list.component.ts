import { Component, Output, EventEmitter } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicles-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule],
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css']
})
export class VehiclesListComponent {
  @Output() vehicleSelected = new EventEmitter<any>();

  displayedColumns: string[] = ['chassi', 'placa', 'tipoVeiculo', 'cor'];
  vehicles: any[] = [];
  selectedVehicle: any = null;

  constructor(private vehicleService: VehicleService) { }

  ngOnInit(): void {
    this.loadVehicles();

    this.vehicleService.veiculoAdicionado$.subscribe(() => {
      this.loadVehicles();
    })
  }

  private loadVehicles() {
    this.vehicleService.listarVeiculos().subscribe({
      next: (data) => {
        this.vehicles = data.veiculos;
        console.log(this.vehicles);
      },
      error: (err) => {
        console.error('Erro ao listar veículos:', err);
      }
    });

  }
  selectVehicle(vehicle: any) {
    this.selectedVehicle = vehicle;
    this.vehicleSelected.emit(vehicle);
  }
}

