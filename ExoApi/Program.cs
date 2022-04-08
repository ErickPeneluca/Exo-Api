using ExoApi.Contexts;
using ExoApi.interfaces;
using ExoApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => { builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod(); });
});

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Version = "v1", Title = "ExoWebApi"});
    }
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao")),
        ClockSkew = TimeSpan.FromMinutes(60),
        ValidIssuer = "exoapi.webapi",
        ValidAudience = "exoapi.webapi"
    };
});

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddTransient<ExoRepository, ExoRepository>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExoWebApi");
    c.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();