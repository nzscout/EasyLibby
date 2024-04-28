using EasyLibby.Api.Data;
using EasyLibby.Api.Endpoints;
using EasyLibby.Api.Settings;
using Serilog;
using Serilog.Templates.Themes;
using SerilogTracing.Expressions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(Formatters.CreateConsoleTextFormatter(theme: TemplateTheme.Literate))
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Information("Starting up");

try
{
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // Add services to the container.
    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

    builder.Services.AddRepositories(builder.Configuration);

    builder.Services.AddSerilog();
    builder.Services.AddSwaggerGen()
                   .AddEndpointsApiExplorer();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseAuthentication();
    //app.UseAuthorization();

    //Apply migrations and seed data
    await app.Services.InitializeDbAsync();

    app.MapBookEndpoints();
    app.MapAuthorEndpoints();
    app.MapMemberEndpoints();

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}

public partial class Program { }