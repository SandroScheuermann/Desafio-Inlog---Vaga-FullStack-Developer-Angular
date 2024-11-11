import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, tap } from 'rxjs';

interface Vehicle {
  id: string;
  chassi: string;
  placa: string;
  tipoVeiculo: number;
  cor: string;
}

interface ListarVeiculoResponse {
  veiculos: Vehicle[];
}

interface Telemetry {
  latitude: number;
  longitude: number;
  dataHora: Date;
  veiculoId: string;
}

interface HistoricoTelemetria {
  ultimaPosicao: Telemetry;
  historicoPosicao: Telemetry[];
}

@Injectable({
  providedIn: 'root'
})

export class VehicleService {
  private apiUrl = 'http://localhost:5000/Veiculo';

  private veiculoAdicionadoSource = new Subject<void>();
  private telemetriaAdicionadaSource = new Subject<void>();

  veiculoAdicionado$ = this.veiculoAdicionadoSource.asObservable();
  telemetriaAdicionada$ = this.telemetriaAdicionadaSource.asObservable();

  constructor(private http: HttpClient) { }

  inserirTelemetria(request: Telemetry): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/InserirTelemetria`, request).pipe(
      tap(() => {
        this.telemetriaAdicionadaSource.next();
      }))
  }

  cadastrarVeiculo(request: Vehicle): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Cadastrar`, request).pipe(
      tap(() => {
        this.veiculoAdicionadoSource.next();
      }))
  }

  listarVeiculos(): Observable<ListarVeiculoResponse> {
    return this.http.get<ListarVeiculoResponse>(`${this.apiUrl}/Listar`);
  }

  obterHistoricoTelemetria(veiculoId: string): Observable<HistoricoTelemetria> {
    return this.http.get<HistoricoTelemetria>(`${this.apiUrl}/ObterTelemetriaCompleta/${veiculoId}`);
  }


}

