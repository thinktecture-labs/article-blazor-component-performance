using Blazor.WASM.Performance.WebApi.Services;
using Microsoft.Extensions.Hosting;


var _corsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(_corsPolicy,
        builder =>
        {
            builder
                .AllowCredentials()
                .AllowAnyHeader()
                .WithHeaders(new[] { "GET", "HEAD", "PUT", "POST", "DELETE" })
                .WithOrigins("https://localhost:5003");
        });
});

builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ConferencesService>();
builder.Services.AddScoped<ContributionsService>();
builder.Services.AddScoped<SpeakerService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    // Get the DbContext instance
    var confService = scope.ServiceProvider.GetRequiredService<ConferencesService>();
    var contributionsService = scope.ServiceProvider.GetRequiredService<ContributionsService>();
    var speakerService = scope.ServiceProvider.GetRequiredService<SpeakerService>();

    //Do the migration asynchronously
    await confService.InitAsync();
    await contributionsService.InitAsync();
    await speakerService.InitAsync();
}


app.UseHttpsRedirection();

app.UseCors(_corsPolicy);

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
