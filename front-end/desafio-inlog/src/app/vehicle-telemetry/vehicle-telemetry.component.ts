import { Component, Input } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { latitudeValidator, longitudeValidator } from '../validators/customvalidators';

@Component({
  selector: 'app-vehicle-telemetry',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatButtonModule, FormsModule, MatToolbarModule, ReactiveFormsModule],
  templateUrl: './vehicle-telemetry.component.html',
  styleUrl: './vehicle-telemetry.component.css'
})
export class VehicleTelemetryComponent {
  @Input() selectedVehicle: any | null = null;

  telemetryForm: FormGroup;

  constructor(private vehicleService: VehicleService, private fb: FormBuilder) {
    this.telemetryForm = this.fb.group({
      latitude: ['', [Validators.required, latitudeValidator]],
      longitude: ['', [Validators.required, longitudeValidator]],
    });
  }

  onSubmit() {

    if (this.selectedVehicle && this.telemetryForm.valid) {
      const telemetryData = {
        ...this.telemetryForm.value,
        idVeiculo: this.selectedVehicle.id,
        datahora: new Date()
      };

      this.vehicleService.inserirTelemetria(telemetryData).subscribe({
        next: response => {
          console.log('Telemetria inserida com sucesso', response);
          this.resetForm();
        },
        error: err => {
          console.error('Erro ao inserir a telemetria', err);
        }
      });
    } else {
      console.log('Formulário inválido');
      this.telemetryForm.markAllAsTouched();
    }
  }

  private resetForm() {
    this.telemetryForm.reset();
    this.selectedVehicle = null;
    this.telemetryForm.markAsPristine();
  }
}
