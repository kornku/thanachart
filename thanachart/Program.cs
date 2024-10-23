using Autofac;
using Framework.Api;
using infrastrucrue;
using infrastrucrue.Database;

var builder = Framework.Api.Framework.CreateDefaultWebApplicationBuilder(args);
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext(defaultConnectionString);
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new InfrastructureModule());
});
var app = builder.CreateDefaultWebApplication();
// await app.MigrateDatabase();
app.Run();