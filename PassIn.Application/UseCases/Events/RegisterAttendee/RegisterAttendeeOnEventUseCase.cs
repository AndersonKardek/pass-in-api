using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterAttendeeOnEventUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid eventId, RequestRegisterEventJson request)
    {
        await Validate(eventId, request);

        var entity = new Infrastructure.Entities.Attendee()
        {
            Name = request.Name,
            Email = request.Email,
            Event_Id = eventId,
            Created_At = DateTime.UtcNow,
            
        };

        await _dbContext.Attendees.AddAsync(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private async Task Validate(Guid eventId, RequestRegisterEventJson request)
    {
        var eventEntity = await _dbContext.Events.FindAsync(eventId);
       
        if (eventEntity is null)
        {
            throw new NotFoundException("Event not found with this Id.");
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("The name is invalid.");
        }

        if (EmailIsValid(request.Email) == false)
        {
            throw new ErrorOnValidationException("The email is invalid.");
        }

        var attendeeAlredyExists = await _dbContext
           .Attendees
           .AnyAsync(a => a.Email.Equals(request.Email) && a.Event_Id == eventId);

        if (attendeeAlredyExists) 
        {
            throw new ConflictException("User alredy existis on this event.");
        }

        var attendeesForEvent =_dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);
        

        if(attendeesForEvent == eventEntity.Maximum_Attendees)
        {
            throw new ErrorOnValidationException("Maximum attendees alredy complete for this event");
        }
    }

    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
