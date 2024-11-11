import { Component, Input, OnInit, OnChanges, SimpleChanges, EventEmitter, Output } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-polylinedecorator'
import { VehicleService } from '../services/vehicle.service';
import { MatButtonToggle, MatButtonToggleGroup } from '@angular/material/button-toggle';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [MatButtonToggleGroup, MatButtonToggle, FormsModule],
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, OnChanges {
  private map: any;
  private markers: L.CircleMarker[] = [];
  private polyline: L.Polyline | null = null;
  private polylineBorder: L.Polyline | null = null;
  private polylineDecorator: any = null;

  @Input() vehicleLocation: { lat: number, lng: number } | null = null;
  @Input() selectedVehicle: any | null = null;
  @Input() mode: 'default' | 'historico' = 'historico';
  @Output() mapClicked = new EventEmitter<{ latitude: number, longitude: number }>();

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.initMap();
    this.loadMapData();

    this.vehicleService.telemetriaAdicionada$.subscribe(() => {
      this.loadMapData();
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['mode'] || changes['selectedVehicle']) {
      this.loadMapData();
    }
  }

  onModeSelect(selectedMode: 'default' | 'historico') {
    this.mode = selectedMode;
    console.log(`Modo selecionado: ${this.mode}`);
  }

  private initMap(): void {
    this.map = L.map('map').setView([-27.5954, -48.5480], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '© OpenStreetMap'
    }).addTo(this.map);

    this.map.on('click', (event: L.LeafletMouseEvent) => {
      const { lat, lng } = event.latlng;
      console.log(`Latitude: ${lat}, Longitude: ${lng}`);
      this.mapClicked.emit({ latitude: lat, longitude: lng });
    });

  }

  protected loadMapData(): void {
    if (this.mode === 'historico' && this.selectedVehicle) {
      this.loadHistoricalMode();
    } else if (this.mode === 'default') {
      this.loadLastLocations();
    }
  }

  private loadLastLocations(): void {
    this.clearMarkers();
    this.clearPolyline();

    this.vehicleService.listarVeiculos().subscribe(response => {
      console.log('Resposta da API para última localização dos veículos:', response);

      if (Array.isArray(response.veiculos)) {
        response.veiculos.forEach(vehicle => {
          if (vehicle.ultimaTelemetria) { // Verifica se o veículo possui uma última telemetria
            const { latitude, longitude } = vehicle.ultimaTelemetria;

            const marker = L.circleMarker([latitude, longitude], {
              radius: 10,
              color: 'white',
              fillColor: 'blue',
              fillOpacity: 1
            }).bindPopup(`
          <div><strong>Veículo:</strong> ${vehicle.chassi}</div>
          <div><strong>Placa:</strong> ${vehicle.placa}</div>
          <div><strong>Latitude:</strong> ${latitude}</div>
          <div><strong>Longitude:</strong> ${longitude}</div>
          <div><strong>Data/Hora:</strong> ${new Date(vehicle.ultimaTelemetria.dataHora).toLocaleString()}</div>
          `);

            marker.addTo(this.map);
            this.markers.push(marker);
          }
        });

        const group = L.featureGroup(this.markers);
        this.map.fitBounds(group.getBounds());
      } else {
        console.error("Erro: Nenhum veículo com telemetria encontrado ou resposta inesperada.", response);
      }
    });
  }

  private loadHistoricalMode(): void {
    this.clearMarkers();
    this.clearPolyline();

    if (!this.selectedVehicle) return;

    this.vehicleService.obterHistoricoTelemetria(this.selectedVehicle.id).subscribe(response => {
      console.log('Resposta da API para histórico de telemetria:', response);

      if (Array.isArray(response.historicoPosicao)) {
        const latLngs = response.historicoPosicao.map(point => L.latLng(point.latitude, point.longitude));

        this.polylineBorder = L.polyline(latLngs, { color: 'darkgrey', weight: 17 }).addTo(this.map);
        this.polyline = L.polyline(latLngs, { color: 'blue', weight: 10 }).addTo(this.map);

        this.map.fitBounds(this.polyline.getBounds());

        response.historicoPosicao.forEach((point) => {
          const positionsMarkers = L.circleMarker([point.latitude, point.longitude], {
            radius: 8,
            color: 'darkgrey',
            fillColor: 'white',
            fillOpacity: 1
          }).bindPopup(`
          <div><strong>Data/Hora:</strong> ${new Date(point.dataHora).toLocaleString()}</div>
          <div><strong>Latitude:</strong> ${point.latitude}</div>
          <div><strong>Longitude:</strong> ${point.longitude}</div>`);
          positionsMarkers.addTo(this.map);
          this.markers.push(positionsMarkers);
        });

        //setas
        this.polylineDecorator = L.polylineDecorator(this.polyline, {
          patterns: [
            {
              offset: '10%',
              repeat: '5%',
              symbol: L.Symbol.arrowHead({
                pixelSize: 8,
                polygon: true,
                pathOptions: { fillOpacity: 1, fillColor: 'white', weight: 1, color: 'darkgrey' }
              })
            }
          ]
        }).addTo(this.map);

      } else {
        console.error("Erro: O histórico de posições não está disponível no formato esperado.", response);
      }
    });
  }

  private clearMarkers(): void {
    this.markers.forEach(marker => marker.remove());
    this.markers = [];
  }

  private clearPolyline(): void {
    this.polyline?.remove();
    this.polyline = null;

    this.polylineBorder?.remove();
    this.polylineBorder = null;

    this.polylineDecorator?.remove();
    this.polylineDecorator = null;
  }
}


