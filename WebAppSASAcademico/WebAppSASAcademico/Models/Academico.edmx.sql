
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/26/2017 23:23:33
-- Generated from EDMX file: c:\users\samuelp\documents\visual studio 2015\Projects\WebAppSASAcademico\WebAppSASAcademico\Models\Academico.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Academico];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Atividade'
CREATE TABLE [dbo].[Atividade] (
    [IdListaExercicio] int IDENTITY(1,1) NOT NULL,
    [nota] float  NULL,
    [descricao] nvarchar(max)  NOT NULL,
    [nomeAluno] nvarchar(max)  NULL,
    [idAluno] int  NULL,
    [IdTurma] int  NOT NULL
);
GO

-- Creating table 'Turma'
CREATE TABLE [dbo].[Turma] (
    [IdTurma] int IDENTITY(1,1) NOT NULL,
    [descricao] nvarchar(max)  NOT NULL,
    [condicaoLimiteUm] int  NOT NULL,
    [condicaoLimiteDois] int  NULL
);
GO

-- Creating table 'Exercicio'
CREATE TABLE [dbo].[Exercicio] (
    [IdExercicio] int IDENTITY(1,1) NOT NULL,
    [enunciado] nvarchar(max)  NOT NULL,
    [respostaAluno] nvarchar(max)  NULL,
    [respostaProfessor] nvarchar(max)  NULL,
    [comentario] nvarchar(max)  NULL,
    [IdListaExercicios] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdListaExercicio] in table 'Atividade'
ALTER TABLE [dbo].[Atividade]
ADD CONSTRAINT [PK_Atividade]
    PRIMARY KEY CLUSTERED ([IdListaExercicio] ASC);
GO

-- Creating primary key on [IdTurma] in table 'Turma'
ALTER TABLE [dbo].[Turma]
ADD CONSTRAINT [PK_Turma]
    PRIMARY KEY CLUSTERED ([IdTurma] ASC);
GO

-- Creating primary key on [IdExercicio] in table 'Exercicio'
ALTER TABLE [dbo].[Exercicio]
ADD CONSTRAINT [PK_Exercicio]
    PRIMARY KEY CLUSTERED ([IdExercicio] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdTurma] in table 'Atividade'
ALTER TABLE [dbo].[Atividade]
ADD CONSTRAINT [FK_TurmaAtividade]
    FOREIGN KEY ([IdTurma])
    REFERENCES [dbo].[Turma]
        ([IdTurma])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TurmaAtividade'
CREATE INDEX [IX_FK_TurmaAtividade]
ON [dbo].[Atividade]
    ([IdTurma]);
GO

-- Creating foreign key on [IdListaExercicios] in table 'Exercicio'
ALTER TABLE [dbo].[Exercicio]
ADD CONSTRAINT [FK_ExercicioListaExercicios]
    FOREIGN KEY ([IdListaExercicios])
    REFERENCES [dbo].[Atividade]
        ([IdListaExercicio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExercicioListaExercicios'
CREATE INDEX [IX_FK_ExercicioListaExercicios]
ON [dbo].[Exercicio]
    ([IdListaExercicios]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------