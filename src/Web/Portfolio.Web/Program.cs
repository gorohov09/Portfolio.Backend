using Portfolio.Core;
using Portfolio.Data.PostgreSql;
using Portfolio.Web.Authentication;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
	.AddHttpContextAccessor()
	.AddUserContext()
	.AddCore()
	.AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"));

services.AddControllers();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
