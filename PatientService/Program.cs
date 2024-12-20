using Data_and_Models;
using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;
using PatientService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddDbContext<BloodPressureContext>();
builder.Services.AddTransient<FeatureFlagFilter>();
builder.Services.AddControllers();


//Setting up Feature hub - Client Key
var fhubConfig = new EdgeFeatureHubConfig("http://featurehub:8085", Environment.GetEnvironmentVariable("fhubkey"));

// Sets this apps country attribute to denmark - enables location based features - This app is only available to users IN denmark.
var fh = await fhubConfig.NewContext().Country(StrategyAttributeCountryName.Denmark).Build();
builder.Services.AddSingleton<IClientContext>(fh);
await fhubConfig.Init();
// awaited fhubconfig.Init to enable real-time updates about flag states.


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();