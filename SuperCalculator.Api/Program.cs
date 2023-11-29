using System.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using SuperCalculator.Api.Models.Request;
using SuperCalculator.Api.Services;
using SuperCalculator.Api.Validators;
using SuperCalculator.Application.Builders;
using SuperCalculator.Application.Services;
using SuperCalculator.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
builder.Services.AddScoped<IRequestConverterService, RequestConverterService>();
builder.Services.AddScoped<IEmployeeSuperService, EmployeeSuperService>();
builder.Services.AddScoped<IRawDataReaderService, RawDataReaderService>();
builder.Services
    .AddScoped<IEmployeeSuperQuarterlyVarianceSummaryBuilder, EmployeeSuperQuarterlyVarianceSummaryBuilder>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();

// validation services
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services
    .AddScoped<IValidator<EmployeeSuperQuarterlyVarianceRequest>, EmployeeSuperQuarterlyVarianceRequestValidator>();
builder.Services.AddScoped<IValidator<DataSet>, RawEmployeeSuperDataSetValidator>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();

app.Run();