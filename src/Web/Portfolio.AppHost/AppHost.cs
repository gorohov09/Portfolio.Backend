using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
	.WithDataVolume()
	.AddDatabase("portfolio-db");

var minio = builder.AddMinioContainer(
	name: "minio",
	rootUser: builder.AddParameter("minio-user", "minio"),
	rootPassword: builder.AddParameter("minio-password", "password"),
	port: 9000
).WithDataVolume();

var apiService = builder.AddProject<Projects.Portfolio_Web>("portfolio-api")
	.WithReference(database)
	.WithReference(minio)
	.WaitFor(minio)
	.WaitFor(database);

builder.AddViteApp(name: "portfolio-frontend", workingDirectory: "../Portfolio.Frontend")
	.WithReference(apiService)
	.WaitFor(apiService)
	.WithNpmPackageInstallation()
	.WithEnvironment("PORT", "5174");

builder.Build().Run();
