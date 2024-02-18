using Portfolio.Core;
using Portfolio.Data.PostgreSql;
using Portfolio.Data.S3;
using Portfolio.Web.Authentication;
using Portfolio.Web.Swagger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
	.AddSwagger()
	.AddHttpContextAccessor()
	.AddUserContext()
	.AddCustomHeaderAuthentication(services)
	.AddCore()
	.AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"))
	.AddS3Storage(configuration.Get<S3Options>());

services.AddControllers();

var app = builder.Build();
{
	using (var scope = app.Services.CreateScope())
	{
		var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
		await migrator.MigrateAsync();
	}

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
