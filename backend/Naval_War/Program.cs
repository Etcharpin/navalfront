using Business;
using Microsoft.EntityFrameworkCore;
using NavalWar.Business;
using NavalWar.DAL;
using NavalWar.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddCors(option =>
{
	option.AddPolicy(name: "AutoCorsPolicy",
						policy =>
						{
							policy.SetIsOriginAllowed(origin => true)
							.AllowAnyHeader()
							.AllowAnyMethod()
							.AllowCredentials();
						});
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EFCore Options
builder.Services.AddDbContext<NavalContext>(options => options.UseSqlServer("Server=tcp:navalwargroup13.database.windows.net,1433;Initial Catalog=NavalWarDBGP13;Persist Security Info=False;User ID=etcharpin;Password=NWGP1363!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

// Dependencies
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AutoCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
