/* ==================================================
BANCO DE DADOS: db_meatflow
DESCRIÇÃO: Controle de comercialização e pedidos de carnes.
AUTOR: Arthur Oliveira Ladeira
===================================================== */


-- ==================================================
-- DROP CASO NECESSÁRIO
USE master;
GO

ALTER DATABASE db_meatflow
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE db_meatflow;
GO
-- ==================================================

-- ==================================================
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'db_meatflow')
    BEGIN
        CREATE DATABASE db_meatflow;
    END
GO

USE db_meatflow;
GO
-- ==================================================


-- ================================================
-- TABELA: estado
-- DESCRIÇÃO: Cadastro de estados.
-- ================================================
IF OBJECT_ID('estado', 'U') IS NOT NULL
    DROP TABLE estado;
GO
CREATE TABLE estado (
    idt_estado UNIQUEIDENTIFIER NOT NULL,
    sigla_estado VARCHAR(2) NOT NULL,
    nome_estado VARCHAR(100) NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_estado PRIMARY KEY (IDT_ESTADO),
    CONSTRAINT uq_estado_sigla UNIQUE (sigla_estado));
GO


-- ================================================
-- TABELA: cidade
-- DESCRIÇÃO: Cadastro de cidades vinculadas a um estado.
-- ================================================
IF OBJECT_ID('cidade', 'U') IS NOT NULL
    DROP TABLE cidade;
GO
CREATE TABLE cidade (
    idt_cidade UNIQUEIDENTIFIER NOT NULL,
    idt_estado UNIQUEIDENTIFIER NOT NULL,
    nome_cidade VARCHAR(150) NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_cidade PRIMARY KEY (IDT_CIDADE),
    CONSTRAINT fk_cidade_estado FOREIGN KEY (IDT_ESTADO) REFERENCES estado(IDT_ESTADO)
);
GO

CREATE INDEX idx_cidade_estado
ON cidade (IDT_ESTADO);
GO


-- ================================================
-- TABELA: carne
-- DESCRIÇÃO: Cadastro de carnes comercializadas.
-- ================================================
IF OBJECT_ID('carne', 'U') IS NOT NULL
    DROP TABLE carne;
GO
CREATE TABLE carne (
    idt_carne UNIQUEIDENTIFIER NOT NULL,
    descricao_carne VARCHAR(200) NOT NULL,
    origem_carne VARCHAR(100) NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_carne PRIMARY KEY (IDT_CARNE)
);
GO


-- ================================================
-- TABELA: comprador
-- DESCRIÇÃO: Cadastro de compradores.
-- ================================================
IF OBJECT_ID('comprador', 'U') IS NOT NULL
    DROP TABLE comprador;
GO
CREATE TABLE comprador (
    idt_comprador UNIQUEIDENTIFIER NOT NULL,
    documento_fiscal VARCHAR(14) NOT NULL,
    nome_comprador VARCHAR(200) NOT NULL,
    idt_cidade UNIQUEIDENTIFIER NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_comprador PRIMARY KEY (IDT_COMPRADOR),
    CONSTRAINT uq_comprador_documento UNIQUE (documento_fiscal),
    CONSTRAINT fk_comprador_cidade FOREIGN KEY (IDT_CIDADE) REFERENCES cidade(IDT_CIDADE)
);
GO

CREATE INDEX idx_comprador_cidade ON comprador (IDT_CIDADE);
GO


-- ================================================
-- TABELA: pedido
-- DESCRIÇÃO: Cabeçalho do pedido realizado pelo comprador.
-- ================================================
IF OBJECT_ID('pedido', 'U') IS NOT NULL
    DROP TABLE pedido;
GO
CREATE TABLE pedido (
    idt_pedido UNIQUEIDENTIFIER NOT NULL,
    idt_comprador UNIQUEIDENTIFIER NOT NULL,
    data_pedido DATETIME2 NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_pedido PRIMARY KEY (IDT_PEDIDO),
    CONSTRAINT fk_pedido_comprador FOREIGN KEY (IDT_COMPRADOR) REFERENCES comprador(IDT_COMPRADOR)
);
GO

CREATE INDEX idx_pedido_comprador ON pedido (IDT_COMPRADOR);
CREATE INDEX idx_pedido_data ON pedido (data_pedido);
GO


-- ================================================
-- TABELA: item_pedido
-- DESCRIÇÃO: Itens pertencentes ao pedido. (RESPONSÁVEL PELO RELACIONAMENTO N:N ENTRE PEDIDO E CARNE.)
-- ================================================
IF OBJECT_ID('item_pedido', 'U') IS NOT NULL
    DROP TABLE item_pedido;
GO
CREATE TABLE item_pedido (
    idt_item_pedido UNIQUEIDENTIFIER NOT NULL,
    idt_pedido UNIQUEIDENTIFIER NOT NULL,
    idt_carne UNIQUEIDENTIFIER NOT NULL,
    quantidade_kg DECIMAL(18,3) NOT NULL,
    valor_unitario DECIMAL(18,2) NOT NULL,
    codigo_moeda CHAR(3) NOT NULL,
    dat_criacao DATETIME2 NOT NULL,
    dat_atualizacao DATETIME2 NOT NULL,

    CONSTRAINT pk_item_pedido PRIMARY KEY (IDT_ITEM_PEDIDO),
    CONSTRAINT fk_item_pedido_pedido FOREIGN KEY (IDT_PEDIDO) REFERENCES pedido(IDT_PEDIDO),
    CONSTRAINT fk_item_pedido_carne FOREIGN KEY (IDT_CARNE) REFERENCES carne(IDT_CARNE)
);
GO

CREATE INDEX idx_item_pedido_pedido ON item_pedido (IDT_PEDIDO);
CREATE INDEX idx_item_pedido_carne ON item_pedido (IDT_CARNE);
GO




-- ===============================================================================
-- CARGA INICIAL - ESTADO
-- ===============================================================================
INSERT INTO estado (idt_estado, sigla_estado, nome_estado, dat_criacao, dat_atualizacao) 
VALUES 
('11111111-1111-1111-1111-111111111111', 'MG', 'Minas Gerais', GETDATE(), GETDATE()),
('22222222-2222-2222-2222-222222222222', 'SP', 'São Paulo', GETDATE(), GETDATE()),
('33333333-3333-3333-3333-333333333333', 'RJ', 'Rio de Janeiro', GETDATE(), GETDATE()),
('44444444-4444-4444-4444-444444444444', 'BA', 'Bahia', GETDATE(), GETDATE()),
('55555555-5555-5555-5555-555555555555', 'RS', 'Rio Grande do Sul', GETDATE(), GETDATE());
GO


-- ===============================================================================
-- CARGA INICIAL - CIDADE
-- ===============================================================================
INSERT INTO cidade (idt_cidade, idt_estado, nome_cidade, dat_criacao, dat_atualizacao)
VALUES
('11111111-AAAA-1111-AAAA-111111111111', '11111111-1111-1111-1111-111111111111', 'Cataguases', GETDATE(), GETDATE()),
('22222222-BBBB-2222-BBBB-222222222222', '11111111-1111-1111-1111-111111111111', 'Juiz de Fora', GETDATE(), GETDATE()),
('33333333-CCCC-3333-CCCC-333333333333', '22222222-2222-2222-2222-222222222222', 'São Paulo', GETDATE(), GETDATE()),
('44444444-DDDD-4444-DDDD-444444444444', '33333333-3333-3333-3333-333333333333', 'Rio de Janeiro', GETDATE(), GETDATE()),
('55555555-EEEE-5555-EEEE-555555555555', '44444444-4444-4444-4444-444444444444', 'Salvador', GETDATE(), GETDATE()),
('66666666-FFFF-6666-FFFF-666666666666', '44444444-4444-4444-4444-444444444444', 'Feira de Santana', GETDATE(), GETDATE()),
('77777777-GGGG-7777-GGGG-777777777777', '55555555-5555-5555-5555-555555555555', 'Porto Alegre', GETDATE(), GETDATE()),
('88888888-HHHH-8888-HHHH-888888888888', '55555555-5555-5555-5555-555555555555', 'Caxias do Sul', GETDATE(), GETDATE());
GO


-- ===============================================================================
-- CARGA INICIAL - CARNE
-- ===============================================================================
INSERT INTO carne (idt_carne, descricao_carne, origem_carne, dat_criacao, dat_atualizacao)
VALUES
('AAAA1111-AAAA-1111-AAAA-111111111111', 'Picanha', 'Bovina', GETDATE(), GETDATE()),
('BBBB2222-BBBB-2222-BBBB-222222222222', 'Alcatra', 'Bovina', GETDATE(), GETDATE()),
('CCCC3333-CCCC-3333-CCCC-333333333333', 'Costela Suína', 'Suína', GETDATE(), GETDATE()),
('DDDD4444-DDDD-4444-DDDD-444444444444', 'Peito de Frango', 'Aves', GETDATE(), GETDATE());
GO


-- ===============================================================================
-- CARGA INICIAL - COMPRADOR
-- ===============================================================================
INSERT INTO comprador (idt_comprador, documento_fiscal, nome_comprador, idt_cidade, dat_criacao, dat_atualizacao)
VALUES
('AAAAAAAA-1111-1111-1111-AAAAAAAAAAAA','12345678901','Mercado Central','11111111-AAAA-1111-AAAA-111111111111',GETDATE(),GETDATE()),
('BBBBBBBB-2222-2222-2222-BBBBBBBBBBBB','12345678000199','Frigorífico Nacional','33333333-CCCC-3333-CCCC-333333333333',GETDATE(),GETDATE()),
('CCCCCCCC-3333-3333-3333-CCCCCCCCCCCC','98765432000188','Casa de Carnes Premium','44444444-DDDD-4444-DDDD-444444444444',GETDATE(),GETDATE());
GO


-- ===============================================================================
-- CARGA INICIAL - PEDIDO
-- ===============================================================================
INSERT INTO pedido (idt_pedido, idt_comprador, data_pedido, dat_criacao, dat_atualizacao)
VALUES
('99999999-1111-1111-1111-999999999999', 'AAAAAAAA-1111-1111-1111-AAAAAAAAAAAA', GETDATE(), GETDATE(), GETDATE()),
('99999999-2222-2222-2222-999999999999', 'BBBBBBBB-2222-2222-2222-BBBBBBBBBBBB', GETDATE(), GETDATE(), GETDATE());
GO


-- ===============================================================================
-- CARGA INICIAL - ITEM_PEDIDO
-- ===============================================================================
INSERT INTO item_pedido (idt_item_pedido, idt_pedido, idt_carne, quantidade_kg, valor_unitario, codigo_moeda, dat_criacao, dat_atualizacao)
VALUES
('EEEE1111-1111-1111-1111-EEEEEEEEEEEE', '99999999-1111-1111-1111-999999999999', 'AAAA1111-AAAA-1111-AAAA-111111111111', 10.500, 79.90, 'BRL', GETDATE(), GETDATE()),
('EEEE2222-2222-2222-2222-EEEEEEEEEEEE', '99999999-1111-1111-1111-999999999999', 'BBBB2222-BBBB-2222-BBBB-222222222222', 5.000, 52.90, 'BRL', GETDATE(), GETDATE()),
('EEEE3333-3333-3333-3333-EEEEEEEEEEEE', '99999999-2222-2222-2222-999999999999', 'CCCC3333-CCCC-3333-CCCC-333333333333', 20.000, 24.90, 'BRL', GETDATE(), GETDATE()),
('EEEE4444-4444-4444-4444-EEEEEEEEEEEE', '99999999-2222-2222-2222-999999999999', 'DDDD4444-DDDD-4444-DDDD-444444444444', 15.000, 18.50, 'BRL', GETDATE(), GETDATE());
GO



-- ========================================
-- DELETE DOS DADOS
-- ========================================
DELETE FROM estado;
DELETE FROM cidade;
DELETE FROM carne;
DELETE FROM comprador;
DELETE FROM pedido;
DELETE FROM item_pedido;


SELECT * FROM estado;
SELECT * FROM cidade;
SELECT * FROM carne;
SELECT * FROM comprador;
SELECT * FROM pedido;
SELECT * FROM item_pedido;