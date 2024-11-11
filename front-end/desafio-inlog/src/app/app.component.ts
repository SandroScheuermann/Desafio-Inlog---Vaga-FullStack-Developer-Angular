import { VehiclesListComponent } from './vehicles-list/vehicles-list.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehicleTelemetryComponent } from './vehicle-telemetry/vehicle-telemetry.component';
import { Router, RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';
import { MatChipsModule } from '@angular/material/chips';
import { MapComponent } from "./map/map.component";
import { VehicleListPageComponent } from './pages/vehicle-list-page/vehicle-list-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [VehiclesListComponent, VehicleListPageComponent, RouterOutlet, VehicleFormComponent, VehicleTelemetryComponent, MatChipsModule, MapComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  latitude: number | null = null;
  longitude: number | null = null;
  selectedVehicle: any;
  mode: 'default' | 'historico' = 'default';
  isVehicleListPage =  false;

  constructor(private router: Router) { }

  onVehicleSelected(vehicle: any) {
    this.selectedVehicle = vehicle;
  }

  onMapClick(coords: { latitude: number, longitude: number }) {
    this.latitude = coords.latitude;
    this.longitude = coords.longitude;
  }

  onModeChange(newMode: 'default' | 'historico') {
    this.mode = newMode;
  }
  navigateTo(route: string) {
    this.isVehicleListPage = route === '/listar';
    this.router.navigate([route]);
  }
}

