import { ComponentFixture, TestBed } from '@angular/core/testing';
import { VehicleFormComponent } from './vehicle-form.component';
import { VehicleService } from '../services/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { of } from 'rxjs';

describe('VehicleFormComponent', () => {
  let component: VehicleFormComponent;
  let fixture: ComponentFixture<VehicleFormComponent>;
  let vehicleServiceSpy: jasmine.SpyObj<VehicleService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('VehicleService', ['cadastrarVeiculo', 'obterTipoVeiculoOptions']);

    spy.obterTipoVeiculoOptions.and.returnValue(of([
      { value: 1, label: 'Carro' },
      { value: 2, label: 'Moto' }
    ]));

    await TestBed.configureTestingModule({
      imports: [
        VehicleFormComponent,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatToolbarModule,
        BrowserAnimationsModule
      ],
      providers: [{ provide: VehicleService, useValue: spy }]
    }).compileComponents();

    vehicleServiceSpy = TestBed.inject(VehicleService) as jasmine.SpyObj<VehicleService>;
    fixture = TestBed.createComponent(VehicleFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('deve inicializar o formulário com os controles corretos', () => {
    expect(component.vehicleForm.contains('chassi')).toBeTrue();
    expect(component.vehicleForm.contains('placa')).toBeTrue();
    expect(component.vehicleForm.contains('tipoVeiculo')).toBeTrue();
    expect(component.vehicleForm.contains('cor')).toBeTrue();
  });

  it('deve ser inválido se os campos obrigatórios estiverem vazios', () => {
    component.vehicleForm.setValue({
      chassi: '',
      placa: '',
      tipoVeiculo: '',
      cor: ''
    });
    expect(component.vehicleForm.valid).toBeFalse();
  });

  it('deve ser válido com valores corretos', () => {
    component.vehicleForm.setValue({
      chassi: '1HGBH41JXMN109186',
      placa: 'ABC1234',
      tipoVeiculo: 1,
      cor: 'Azul'
    });
    expect(component.vehicleForm.valid).toBeTrue();
  });

  it('deve chamar VehicleService.cadastrarVeiculo ao submeter um formulário válido', (done) => {
    const mockVehicleData = {
      chassi: '1HGBH41JXMN109186',
      placa: 'ABC1234',
      tipoVeiculo: 1,
      cor: 'Azul'
    };

    vehicleServiceSpy.cadastrarVeiculo.and.returnValue(of({ success: true }));

    component.vehicleForm.setValue(mockVehicleData);
    component.onSubmit();

    expect(vehicleServiceSpy.cadastrarVeiculo).toHaveBeenCalledWith(jasmine.objectContaining(mockVehicleData));
    done();
  });

  it('não deve chamar VehicleService.cadastrarVeiculo ao submeter um formulário inválido', () => {
    component.vehicleForm.setValue({
      chassi: '',
      placa: '',
      tipoVeiculo: '',
      cor: ''
    });

    component.onSubmit();

    expect(vehicleServiceSpy.cadastrarVeiculo).not.toHaveBeenCalled();
  });
});

