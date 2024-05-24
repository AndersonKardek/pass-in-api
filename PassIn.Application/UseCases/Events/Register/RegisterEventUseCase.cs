using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register;

public class RegisterEventUseCase
{
    public async Task<ResponseRegisteredJson> Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var entity = new Infrastructure.Entities.Event
        {
            Title = request.Title,
            Details = request.Details,
            Slug = request.Title.ToLower().Replace(" ","-"),
            Maximum_Attendees = request.MaximumAttendees,
        };

        await dbContext.Events.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return new ResponseRegisteredJson
        {
            Id = entity.Id
        };
    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0) 
        {
            throw new ErrorOnValidationException("The maximun attendees is invalid");    
        }

        if(string.IsNullOrWhiteSpace(request.Title)) 
        {
            throw new ErrorOnValidationException("The title is invalid");
        }

        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ErrorOnValidationException("The title is invalid");
        }
    }
}
