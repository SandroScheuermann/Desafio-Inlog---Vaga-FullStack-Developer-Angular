CREATE DATABASE TesteInlog;
GO
USE TesteInlog;
GO

CREATE TABLE TiposVeiculo (
    Id INT PRIMARY KEY,               
    Descricao NVARCHAR(50) NOT NULL   
);

INSERT INTO TiposVeiculo (Id, Descricao) VALUES 
(1, 'Onibus'),
(2, 'Caminhao');

CREATE TABLE Veiculos (
    Id INT IDENTITY PRIMARY KEY,
    Chassi NVARCHAR(50) NOT NULL,
    Placa NVARCHAR(20) NOT NULL,
    TiposVeiculo INT NOT NULL,             
    Cor NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_Veiculo_TiposVeiculo FOREIGN KEY (TiposVeiculo) REFERENCES TiposVeiculo(Id)
);

CREATE TABLE TelemetriasHistorico (
    Id INT IDENTITY PRIMARY KEY,
    IdVeiculo INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    CONSTRAINT FK_TelemetriaHistorico_Veiculo FOREIGN KEY (IdVeiculo) REFERENCES Veiculo(Id)
);

CREATE TABLE Telemetrias (
    Id INT IDENTITY PRIMARY KEY,
    IdVeiculo INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    CONSTRAINT FK_Telemetria_Veiculo FOREIGN KEY (IdVeiculo) REFERENCES Veiculo(Id)
);

