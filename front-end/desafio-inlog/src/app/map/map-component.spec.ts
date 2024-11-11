import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MapComponent } from './map.component';
import { VehicleService } from '../services/vehicle.service';
import { of } from 'rxjs';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { FormsModule } from '@angular/forms';

describe('MapComponent', () => {
  let component: MapComponent;
  let fixture: ComponentFixture<MapComponent>;
  let vehicleServiceSpy: jasmine.SpyObj<VehicleService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('VehicleService', [
      'listarVeiculos',
      'obterHistoricoTelemetria',
      'atualizarLocalizacao',
      'telemetriaAdicionada$',
      'veiculoSelecionado$'
    ]);

    spy.listarVeiculos.and.returnValue(of({ veiculos: [] }));
    spy.obterHistoricoTelemetria.and.returnValue(of({ historicoPosicao: [] }));
    spy.telemetriaAdicionada$ = of();
    spy.veiculoSelecionado$ = of(null);

    await TestBed.configureTestingModule({
      imports: [MapComponent, MatButtonToggleModule, FormsModule],
      providers: [{ provide: VehicleService, useValue: spy }]
    }).compileComponents();

    vehicleServiceSpy = TestBed.inject(VehicleService) as jasmine.SpyObj<VehicleService>;
    fixture = TestBed.createComponent(MapComponent);
    component = fixture.componentInstance;
  });

  it('deve inicializar o mapa corretamente', () => {
    fixture.detectChanges();

    expect(component.map).toBeDefined();
  });

  it('deve alternar o modo do mapa e chamar loadMapData', () => {
    spyOn(component, 'loadMapData');

    component.mode = 'default';
    fixture.detectChanges();
    component.loadMapData();

    expect(component.loadMapData).toHaveBeenCalled();

    component.mode = 'historico';
    fixture.detectChanges();
    component.loadMapData();

    expect(component.loadMapData).toHaveBeenCalled();
  });

  it('deve carregar as últimas localizações quando no modo "default"', () => {
    spyOn(component, 'loadLastLocations');

    component.mode = 'default';
    component.loadMapData();

    expect(component.loadLastLocations).toHaveBeenCalled();
  });

  it('deve carregar o histórico de localização quando no modo "historico"', () => {
    spyOn(component, 'loadHistoricalMode');

    component.mode = 'historico';
    component.selectedVehicle = { id: 1 };
    component.loadMapData();

    expect(component.loadHistoricalMode).toHaveBeenCalled();
  });
});

