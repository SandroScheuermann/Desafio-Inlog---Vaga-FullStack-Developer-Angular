import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet-polylinedecorator'
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-map',
  standalone: true,
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
  @Input() mode: 'default' | 'historico' = 'historico';
  @Input() selectedVehicle: any | null = null;

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

  private initMap(): void {
    this.map = L.map('map').setView([-27.5954, -48.5480], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '© OpenStreetMap'
    }).addTo(this.map);
  }

  private loadMapData(): void {

    if (this.selectedVehicle) {
      this.loadHistoricalMode();
    }
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


