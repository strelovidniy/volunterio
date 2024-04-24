using Serilog;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Server.DependencyInjection;

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .ReadFrom
        .Configuration(builder.Configuration)
        .CreateLogger();

    builder.Services.RegisterApplication(builder.Configuration);

    var app = builder.Build();

    app.UseApplication();

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Logger.Error(exception, ErrorMessage.ProgramStopped);
}
finally
{
    await Log.CloseAndFlushAsync();
}