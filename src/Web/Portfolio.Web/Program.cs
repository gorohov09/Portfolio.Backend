using Portfolio.Core;
using Portfolio.Data.PostgreSql;
using Portfolio.Data.S3;
using Portfolio.Web.Authentication;
using Portfolio.Web.Hubs;
using Portfolio.Web.Swagger;
using Portfolio.Web.WebSocketServices;
using Portfolio.Worker;

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
	.AddS3Storage(configuration.GetSection("S3").Get<S3Options>())
	.AddHangfireWorker()
	.AddSignaler();

services.AddControllers();

var app = builder.Build();
{
	using (var scope = app.Services.CreateScope())
	{
		var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
		var s3Helper = scope.ServiceProvider.GetRequiredService<S3Helper>();

		await migrator.MigrateAsync();
		await s3Helper.PrepareAsync();
	}

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHangfireWorker(configuration.GetSection("Hangfire").Get<HangfireOptions>());

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.MapHub<NotificationsHub>("notifications");

	app.Run();
}
