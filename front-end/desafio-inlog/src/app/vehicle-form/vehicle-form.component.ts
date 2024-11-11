import { Component } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatToolbarModule, CommonModule, MatFormFieldModule, MatOptionModule, MatAutocompleteModule, MatSelectModule],
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent {
  vehicleForm: FormGroup;
  tiposVeiculo: any[] = [];

  constructor(private vehicleService: VehicleService, private fb: FormBuilder) {
    this.vehicleForm = this.fb.group({
      chassi: ['', [Validators.required, Validators.minLength(17), Validators.maxLength(17)]],
      placa: ['', [Validators.required, Validators.pattern(/^[A-Z]{3}\d{4}$/)]],
      tipoVeiculo: ['', Validators.required],
      cor: ['', Validators.required]
    });

  }

  onSubmit() {
    if (this.vehicleForm.valid) {
      const vehicleData = this.vehicleForm.value;
      this.vehicleService.cadastrarVeiculo(vehicleData).subscribe({
        next: response => {
          console.log('Veículo cadastrado com sucesso', response);
          this.resetForm();
        },
        error: err => {
          console.error('Erro ao cadastrar o veículo', err);
        }
      });
    } else {
      console.log('Formulário inválido');
      this.vehicleForm.markAllAsTouched();
    }
  }

  private resetForm() {
    this.vehicleForm.reset();
    this.vehicleForm.markAsPristine();
  }

  ngOnInit(): void {
    this.vehicleService.obterTipoVeiculoOptions().subscribe((tipos: any[]) => {
      this.tiposVeiculo = tipos;
    });
  }

}
