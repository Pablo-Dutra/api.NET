Instruções Iniciais

Instalar o Visual Studio 2022 com os requisitos:
 - ASP.NET and web development;
 - .NET desktop development;
 
Instalar o MS SQL Express;

Criar o banco de dados: todoappDB;

Criar a tabela: dbo.notes
	create table dbo.notes(
		id bigint identity(1,1),
		description nvarchar(max)
	)

Inserir alguns registros:
	insert into dbo.notes values ('primeiro item')
	insert into dbo.notes values ('segundo item')
	
Abrir o Visual Studio;

Criar um novo projeto do tipo: "ASP.NET Core Web API";	
	Framework: .NET 6.0 (LTS)
	Sem autenticação;
	Desmarcar HTTPS;
	OpenAPI support;
	Use controllers;
	
Adicionar JSON Serialize, em 'Dependências', adicionar pacote NuGet: Microsoft.AspNetCore.Mvc.NewtonsoftJson versão 6.0.0
