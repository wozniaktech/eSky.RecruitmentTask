using eSky.RecruitmentTask.Config;
using eSky.RecruitmentTask.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IHttpService, HttpService>();
builder.Services.AddHttpClient();
builder.Services.Configure<EndpointConfig>(builder.Configuration.GetSection("Endpoints"));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();