import { Component } from '@angular/core';
import { VehiclesListComponent } from '../../vehicles-list/vehicles-list.component';
import { VehicleTelemetryComponent } from '../../vehicle-telemetry/vehicle-telemetry.component';

@Component({
  selector: 'app-vehicle-list-page',
  standalone: true,
  imports: [VehiclesListComponent, VehicleTelemetryComponent],
  templateUrl: './vehicle-list-page.component.html',
  styleUrl: './vehicle-list-page.component.css'
})
export class VehicleListPageComponent {
  selectedVehicleLocation: { lat: number, lng: number } | null = null;
  selectedVehicle: any | null = null;

  onVehicleSelected(vehicle: any) {
    this.selectedVehicleLocation = vehicle.location;
    this.selectedVehicle = vehicle;
  }
}
