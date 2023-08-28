using Hangfire;
using Hangfire.MemoryStorage;
using webapi.Services;
using Hangfire.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseMemoryStorage()
        );
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IServiceJob, ServiceJob>();
builder.Services.AddHangfireServer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
RecurringJob.AddOrUpdate<IServiceJob>("easyjob", x => x.GenString(), "* * * * * *");
app.MapControllers();
app.MapHangfireDashboard("job", new DashboardOptions()
{
    DashboardTitle = "HR TAS _ Background Job",
    Authorization = new []
    {
        new Hangfire
    }
});
app.Run();
