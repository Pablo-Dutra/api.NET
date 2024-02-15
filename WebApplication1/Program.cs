using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JSON Serializer
// Este m�todo estende a configura��o dos controladores para utilizar o Newtonsoft.Json como serializador JSON padr�o.
// Ele tamb�m configura as op��es do serializador para ignorar loops de refer�ncia (quando h� refer�ncias circulares entre objetos).
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options=>
    options.SerializerSettings.ContractResolver=new DefaultContractResolver());

//  Este m�todo finaliza a constru��o da aplica��o usando o WebApplicationFactoryBuilder e retorna uma inst�ncia de WebApplication.
//  Este objeto app pode ent�o ser usado para executar a aplica��o ASP.NET Core.
var app = builder.Build();

// Habilita CORS
app.UseCors(c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


// Configura a requisi��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
