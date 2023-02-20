using Microsoft.EntityFrameworkCore;
using InterviewReviewAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Currently using "produdction db" in development

if (builder.Environment.IsDevelopment())
{
    //builder.Services.AddDbContext<InterviewDbContext>(optionsAction: opt =>
    //opt.UseInMemoryDatabase("InterviewProcessList"));

    // AZURE_SQL_CONNECTIONSTRING is in appsettings.Development.json which is untracked by git
    builder.Services.AddDbContext<InterviewDbContext>(optionsAction: opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
}
else
{
    // ConnectionStrings is in azure
    builder.Services.AddDbContext<InterviewDbContext>(optionsAction: opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline. For this website it's okay to use SwaggerUI in production
app.UseSwagger();
app.UseSwaggerUI(); 

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
