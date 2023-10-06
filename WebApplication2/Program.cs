using GroupChatDemo.Database;
using GroupChatDemo.Hubs;
using GroupChatDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSqlServer<GroupChatDbContext>("Data Source=.;Initial Catalog=GroupChatDemo;Integrated Security=True; Encrypt=False");
builder.Services.AddScoped<IChatServices, ChatServices>();
builder.Services.AddScoped<ChatHub>();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<ChatHub>("group-notification");
app.MapControllers();

app.Run();
