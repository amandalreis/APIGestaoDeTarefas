using Infra;

var builder = WebApplication.CreateBuilder(args);

ApiConfig.ConfigurarInjecaoDependencias(builder);

builder.Services.AddControllers();

ApiConfig.ConfigurarCors(builder);

ApiConfig.ConfigurarSwagger(builder);

ApiConfig.ConfigurarDbContext(builder);

ApiConfig.ConfigurarIdentity(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
} else
{
    app.UseCors("Production");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Para mais informações sobre a API, navegue até a rota /swagger");

app.Run();
