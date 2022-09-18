using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DogBreedsAPI.AuthorizationAuthentication;
using DogBreedsAPI.FIlters;
using DogBreedsAPI.Interfaces;
using DogBreedsAPI.Repositories;
using System.Text;
using DogBreedsAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(cors => cors.AddPolicy("AllowGetFromLocalhost", options => options
                .WithOrigins(new[] { "https://localhost" })
                .WithMethods(new[] { "GET" })
                ));

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomLogsFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informe o token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }

                });
    option.DocumentFilter<SwaggerTitleConfig>();
});

builder.Services.AddScoped(typeof(IDogBreedsRepository), typeof(DogBreedsRepository));

var tokenConfiguration = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration")).Configure(tokenConfiguration);
builder.Services.AddSingleton(tokenConfiguration);
var generateToken = new GenerateToken(tokenConfiguration);
builder.Services.AddScoped(typeof(GenerateToken));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = tokenConfiguration.Audience,
        ValidIssuer = tokenConfiguration.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
    };
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowGetFromLocalhost");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
