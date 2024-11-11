import { ComponentFixture, TestBed } from '@angular/core/testing';
import { VehiclesListComponent } from './vehicles-list.component';
import { VehicleService } from '../services/vehicle.service';
import { of } from 'rxjs';
import { MatTableModule } from '@angular/material/table';
import { By } from '@angular/platform-browser';

describe('VehiclesListComponent', () => {
  let component: VehiclesListComponent;
  let fixture: ComponentFixture<VehiclesListComponent>;
  let vehicleServiceSpy: jasmine.SpyObj<VehicleService>;

  const mockVehicles = [
    {
      id: 1,
      chassi: 'ABC123',
      placa: 'XYZ456',
      tipoVeiculo: 1,
      cor: 'Vermelho',
      ultimaTelemetria: { latitude: 0, longitude: 0, dataHora: new Date(), idVeiculo: 1 }
    },
    {
      id: 2,
      chassi: 'DEF456',
      placa: 'LMN789',
      tipoVeiculo: 2,
      cor: 'Azul',
      ultimaTelemetria: { latitude: 1, longitude: 1, dataHora: new Date(), idVeiculo: 2 }
    }
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('VehicleService', [
      'listarVeiculos',
      'selecionarVeiculo',
      'obterTipoVeiculo'
    ]);

    spy.listarVeiculos.and.returnValue(of({ veiculos: mockVehicles }));
    spy.obterTipoVeiculo.and.returnValue(of([]));
    spy.veiculoAdicionado$ = of();
    spy.telemetriaAdicionada$ = of();

    await TestBed.configureTestingModule({
      imports: [VehiclesListComponent, MatTableModule],
      providers: [{ provide: VehicleService, useValue: spy }]
    }).compileComponents();

    vehicleServiceSpy = TestBed.inject(VehicleService) as jasmine.SpyObj<VehicleService>;
    fixture = TestBed.createComponent(VehiclesListComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('deve renderizar dois veículos na tabela', (done) => {
    fixture.detectChanges();
    fixture.whenStable().then(() => {
      const rows = fixture.debugElement.queryAll(By.css('.mat-table tbody tr'));
      expect(rows.length).toBe(2);
      done();
    });
  });


  it('deve chamar selecionarVeiculo ao selecionar um veículo', () => {
    const vehicle = mockVehicles[0];
    component.onVehicleSelected(vehicle);
    expect(vehicleServiceSpy.selecionarVeiculo).toHaveBeenCalledWith(vehicle);
  });
});

