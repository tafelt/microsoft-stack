using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplication().AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

builder
  .Services
  .AddSwaggerGen(options =>
  {
    options.EnableAnnotations();
  });

builder
  .Services
  .Configure<RouteOptions>(options =>
  {
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
  });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
