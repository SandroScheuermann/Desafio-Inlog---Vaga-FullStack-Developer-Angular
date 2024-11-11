import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable, Subject, tap} from 'rxjs';

interface Vehicle {
  id: number;
  chassi: string;
  placa: string;
  tipoVeiculo: number;
  cor: string;
  ultimaTelemetria: Telemetry;
}

interface ListarVeiculoResponse {
  veiculos: Vehicle[];
}

interface Telemetry {
  latitude: number;
  longitude: number;
  dataHora: Date;
  idVeiculo: number;
}

interface HistoricoTelemetria {
  historicoPosicao: Telemetry[];
}

interface TipoVeiculo {
  value: number;
  label: string;
}

@Injectable({
  providedIn: 'root'
})

export class VehicleService {
  private apiUrl = 'http://localhost:5000/Veiculo';

  private veiculoAdicionadoSource = new Subject<void>();
  private telemetriaAdicionadaSource = new Subject<void>();
  private localizacaoSource = new BehaviorSubject<{ latitude: number; longitude: number } | null>(null);
  private veiculoSelecionadoSource = new BehaviorSubject<Vehicle | null>(null);

  veiculoSelecionado$ = this.veiculoSelecionadoSource.asObservable();
  veiculoAdicionado$ = this.veiculoAdicionadoSource.asObservable();
  telemetriaAdicionada$ = this.telemetriaAdicionadaSource.asObservable();
  localizacaoAtualizada$ = this.localizacaoSource.asObservable();

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
    return this.http.get<HistoricoTelemetria>(`${this.apiUrl}/ObterHistoricoTelemetria/${veiculoId}`);
  }

  obterTipoVeiculo(): Observable<TipoVeiculo[]> {
    return this.http.get<TipoVeiculo[]>(`${this.apiUrl}/TiposVeiculo`);
  }

  obterTipoVeiculoOptions(): Observable<TipoVeiculo[]> {
    return this.obterTipoVeiculo().pipe(
      map((tipoVeiculos: TipoVeiculo[]) =>
        tipoVeiculos.map((tipo: TipoVeiculo) => ({
          value: tipo.value,
          label: tipo.label
        }))
      )
    );
  }

  selecionarVeiculo(vehicle: Vehicle) {
    this.veiculoSelecionadoSource.next(vehicle);
  }

  atualizarLocalizacao(latitude: number, longitude: number) {
    this.localizacaoSource.next({ latitude, longitude });
  }

}

