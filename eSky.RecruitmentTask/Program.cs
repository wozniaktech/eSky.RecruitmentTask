using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IHttpService, HttpService>();
builder.Services.AddHttpClient();
builder.Services.Configure<EndpointConfig>(builder.Configuration.GetSection("Endpoints"));
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();