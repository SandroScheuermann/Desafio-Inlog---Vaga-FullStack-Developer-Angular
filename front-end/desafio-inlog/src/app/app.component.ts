import { Component } from '@angular/core';
import { VehiclesListComponent } from './vehicles-list/vehicles-list.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehicleTelemetryComponent } from './vehicle-telemetry/vehicle-telemetry.component';
import { MapComponent } from './map/map.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [VehiclesListComponent, VehicleFormComponent, VehicleTelemetryComponent, MapComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  selectedVehicleLocation: { lat: number, lng: number } | null = null;
  selectedVehicle: any | null = null;
  latitude: number | null = null;
  longitude: number | null = null;
  mode: 'default' | 'historico' = 'default';

  onVehicleSelected(vehicle: any) {
    this.selectedVehicleLocation = vehicle.location;
    this.selectedVehicle = vehicle;
  }

  onMapClick(coords: { latitude: number, longitude: number }) {
    this.latitude = coords.latitude;
    this.longitude = coords.longitude;
  }

  onModeChange(newMode: 'default' | 'historico') {
    this.mode = newMode;
  }
}

