using Microsoft.EntityFrameworkCore;
using SocialNetwork.Entities;
using SocialNetwork.Persistence.DataBase;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PostsRepository>();
builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CommentsService>();
builder.Services.AddScoped<CommentsRepository>();
builder.Services.AddScoped<LikeRepository>();
builder.Services.AddScoped<LikeService>();

builder.Services.AddControllers();

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
