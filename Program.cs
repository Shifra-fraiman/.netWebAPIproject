using project.Utilities;
using project;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//to use with extension method
builder.Services.AddTask();


//ClearProviders מידלוואר מובנה
builder.Logging.ClearProviders();
//מידלוואר שכותב לקונסול
builder.Logging.AddConsole();
var app = builder.Build();

app.Map("/favicon.ico", (a) =>
    a.Run(async c => await Task.CompletedTask));

app.UseMyLogMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 /*js*/
app.UseFileServer();
app.UseDefaultFiles();
app.UseStaticFiles();
// /*js (remove "launchUrl" from Properties\launchSettings.json*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
