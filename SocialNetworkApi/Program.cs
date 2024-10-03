using SocialNetworkApi.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSocialNetworkServices()
    .AddSocialNetworkRepositories(builder.Configuration)
    .AddSocialNetworkDbContext(builder.Configuration)
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseTracker API v1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
