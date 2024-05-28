using PassIn.Api.Filters;
using PassIn.Application.UseCases.Attendees.GetAllByEventId;
using PassIn.Application.UseCases.Checkins.DoCheckIn;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddInfraStructure(builder.Configuration);

builder.Services.AddTransient<GetAllAttendeeByEventIdUseCase>();
builder.Services.AddTransient<DoAttendeeCheckInUseCase>();
builder.Services.AddTransient<GetEventByIdUseCase>();
builder.Services.AddTransient<RegisterEventUseCase>();
builder.Services.AddTransient<RegisterAttendeeOnEventUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
