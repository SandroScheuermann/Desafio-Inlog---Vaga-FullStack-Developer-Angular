CREATE DATABASE TesteInlog;
GO
USE TesteInlog;
GO

-- Criação da tabela TipoVeiculo
CREATE TABLE TipoVeiculo (
    Id INT PRIMARY KEY,               
    Descricao NVARCHAR(50) NOT NULL   
);

INSERT INTO TipoVeiculo (Id, Descricao) VALUES 
(1, 'Onibus'),
(2, 'Caminhao');

-- Criação da tabela Veiculos
CREATE TABLE Veiculos (
    Id INT IDENTITY PRIMARY KEY,
    Chassi NVARCHAR(50) NOT NULL,
    Placa NVARCHAR(20) NOT NULL,
    IdTipoVeiculo INT NOT NULL,             
    Cor NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_Veiculos_TipoVeiculo FOREIGN KEY (IdTipoVeiculo) REFERENCES TipoVeiculo(Id)
);

-- Criação da tabela TelemetriasHistorico
CREATE TABLE TelemetriasHistorico (
    Id INT IDENTITY PRIMARY KEY,
    IdVeiculo INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    CONSTRAINT FK_TelemetriasHistorico_Veiculos FOREIGN KEY (IdVeiculo) REFERENCES Veiculos(Id)
);

-- Criação da tabela Telemetrias
CREATE TABLE Telemetrias (
    Id INT IDENTITY PRIMARY KEY,
    IdVeiculo INT NOT NULL,
    DataHora DATETIME NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    CONSTRAINT FK_Telemetrias_Veiculos FOREIGN KEY (IdVeiculo) REFERENCES Veiculos(Id)
);
