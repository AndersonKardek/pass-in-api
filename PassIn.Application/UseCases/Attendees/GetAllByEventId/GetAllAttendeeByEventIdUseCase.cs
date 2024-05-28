using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;
public class GetAllAttendeeByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllAttendeeByEventIdUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseAllAttendeesjson> Execute(Guid eventId)
    {
        var attendees = await _dbContext.Events
            .Include(ev => ev.Attendees)
            .FirstOrDefaultAsync(ev => ev.Id.Equals(eventId));

        if (attendees is null)
        {
            throw new NotFoundException("An event with this Id was not found");
        }

        return new ResponseAllAttendeesjson
        {
            Attendees = attendees.Attendees.Select(att => new ResponseAttendeeJson
            {
                Id = att.Id,
                Name = att.Name,
                Email = att.Email,
                CreatedAt = att.Created_At
            }).ToList(),
           
        };
    }

  


}
