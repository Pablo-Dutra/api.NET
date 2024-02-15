using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JSON Serializer
// Este método estende a configuração dos controladores para utilizar o Newtonsoft.Json como serializador JSON padrão.
// Ele também configura as opções do serializador para ignorar loops de referência (quando há referências circulares entre objetos).
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options=>
    options.SerializerSettings.ContractResolver=new DefaultContractResolver());

//  Este método finaliza a construção da aplicação usando o WebApplicationFactoryBuilder e retorna uma instância de WebApplication.
//  Este objeto app pode então ser usado para executar a aplicação ASP.NET Core.
var app = builder.Build();

// Habilita CORS
app.UseCors(c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


// Configura a requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
