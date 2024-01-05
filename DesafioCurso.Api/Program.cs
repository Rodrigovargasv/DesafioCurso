using DesafioCurso.Infra.Ioc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
            // Desabilidade model state
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adicona os servi�os de inje��o de dep�ndencia.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configura��o de middlewares: GlobalException, Authentication e Authorization
app.UseInfrastructure(builder.Configuration);
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
