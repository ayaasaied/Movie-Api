using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);
//to read connection string from app setting
var myconnectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<movieDbContext>(options => options.UseSqlServer(myconnectionstring));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//injection services and interface
builder.Services.AddTransient<ItypeMovie, typeMovieservices>();
builder.Services.AddTransient<Imovie, movieservise>();



builder.Services.AddSwaggerGen();
//enable cors
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//best place to add before authorization
app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
