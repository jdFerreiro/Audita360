using Audit360.API.Middleware;
using Audit360.Application.Interfaces;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Mapping;
using Audit360.Infrastructure.Data;
using Audit360.Infrastructure.Repositories.Read;
using Audit360.Infrastructure.Repositories.Write;
using Audit360.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<Audit360DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Password service
builder.Services.AddSingleton<IPasswordService, BcryptPasswordService>();

// Register repositories (read/write)
builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();

builder.Services.AddScoped<IAuditReadRepository, AuditReadRepository>();
builder.Services.AddScoped<IAuditWriteRepository, AuditWriteRepository>();

builder.Services.AddScoped<IResponsibleReadRepository, ResponsibleReadRepository>();
builder.Services.AddScoped<IResponsibleWriteRepository, ResponsibleWriteRepository>();

builder.Services.AddScoped<IFindingReadRepository, FindingReadRepository>();
builder.Services.AddScoped<IFindingWriteRepository, FindingWriteRepository>();

builder.Services.AddScoped<IFollowUpReadRepository, FollowUpReadRepository>();
builder.Services.AddScoped<IFollowUpWriteRepository, FollowUpWriteRepository>();

builder.Services.AddScoped<IRoleReadRepository, RoleReadRepository>();
builder.Services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();

builder.Services.AddScoped<IAuditStatusReadRepository, AuditStatusReadRepository>();
builder.Services.AddScoped<IAuditStatusWriteRepository, AuditStatusWriteRepository>();

builder.Services.AddScoped<IFindingTypeReadRepository, FindingTypeReadRepository>();
builder.Services.AddScoped<IFindingTypeWriteRepository, FindingTypeWriteRepository>();

builder.Services.AddScoped<IFindingSeverityReadRepository, FindingSeverityReadRepository>();
builder.Services.AddScoped<IFindingSeverityWriteRepository, FindingSeverityWriteRepository>();

builder.Services.AddScoped<IFollowUpStatusReadRepository, FollowUpStatusReadRepository>();
builder.Services.AddScoped<IFollowUpStatusWriteRepository, FollowUpStatusWriteRepository>();

// AutoMapper - use extension to register profiles from Application mapping assembly
builder.Services.AddAutoMapper(typeof(DomainToDtoProfile).Assembly);

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Audit360.Application.Features.Users.Handlers.UserQueryHandler).Assembly));

// FluentValidation: register all validators from Application assembly
builder.Services.AddValidatorsFromAssembly(typeof(Audit360.Application.Features.Users.Handlers.UserQueryHandler).Assembly);

// Add ValidationBehavior to MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Audit360.Application.Behaviors.ValidationBehavior<,>));

// Bind Jwt options
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtOptions = jwtSection.Get<Audit360.Application.Configurations.JwtOptions>();
if (jwtOptions == null || string.IsNullOrWhiteSpace(jwtOptions.Secret))
    throw new InvalidOperationException("JWT Secret not configured in appsettings.");

builder.Services.AddSingleton(jwtOptions);

var key = Encoding.UTF8.GetBytes(jwtOptions.Secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

// Swagger: registration with Bearer security and XML comments using Microsoft.OpenApi root types
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Bearer. Example: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Key must be OpenApiSecuritySchemeReference; value is list of scopes.
    var bearerRef = new OpenApiSecuritySchemeReference("Bearer");
    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            bearerRef,
            new List<string>() // empty scopes for JWT Bearer
        }
    };

});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audit360 API v1"));

app.UseHttpsRedirection();

// Use middleware for validation exceptions
app.UseMiddleware<ValidationExceptionMiddleware>();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
