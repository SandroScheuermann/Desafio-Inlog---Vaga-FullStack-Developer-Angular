import { Component } from '@angular/core';
import { VehicleFormComponent } from '../../vehicle-form/vehicle-form.component';

@Component({
  selector: 'app-vehicle-register-page',
  standalone: true,
  imports: [VehicleFormComponent],
  templateUrl: './vehicle-register-page.component.html',
  styleUrl: './vehicle-register-page.component.css'
})
export class VehicleRegisterPageComponent {
}
