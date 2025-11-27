using Audit360.Application.Interfaces;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Infrastructure.Data;
using Audit360.Infrastructure.Repositories.Read;
using Audit360.Infrastructure.Repositories.Write;
using Audit360.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using FluentValidation;
using Audit360.API.Middleware;
using Audit360.API.Extensions;

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
builder Services.AddScoped<IAuditStatusWriteRepository, AuditStatusWriteRepository>();

builder.Services.AddScoped<IFindingTypeReadRepository, FindingTypeReadRepository>();
builder.Services.AddScoped<IFindingTypeWriteRepository, FindingTypeWriteRepository>();

builder.Services.AddScoped<IFindingSeverityReadRepository, FindingSeverityReadRepository>();
builder.Services.AddScoped<IFindingSeverityWriteRepository, FindingSeverityWriteRepository>();

builder.Services.AddScoped<IFollowUpStatusReadRepository, FollowUpStatusReadRepository>();
builder.Services.AddScoped<IFollowUpStatusWriteRepository, FollowUpStatusWriteRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Audit360.Application.Mapping.DomainToDtoProfile).Assembly);

// MediatR
builder.Services.AddMediatR(typeof(Audit360.Application.Features.Users.Handlers.UserQueryHandler).Assembly);

// FluentValidation: register all validators from Application assembly
builder.Services.AddValidatorsFromAssembly(typeof(Audit360.Application.Features.Users.Handlers.UserQueryHandler).Assembly);

// Add ValidationBehavior to MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Audit360.Application.Behaviors.ValidationBehavior<,>));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

app.UseHttpsRedirection();

// Use middleware for validation exceptions
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
