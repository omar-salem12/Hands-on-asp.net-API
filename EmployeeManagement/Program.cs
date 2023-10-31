using EmployeeManagement.Extentions;
using EmployeeManagement.Repository;
using Microsoft.EntityFrameworkCore;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
builder.Services.ConfigureRepositoryManager();
builder.Services.AddControllers();
builder.Services.AddDbContext<RespositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
