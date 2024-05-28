using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    private readonly PassInDbContext _dbContext;

    public GetEventByIdUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseEventJson>Execute(Guid id)
    {
        var even = await _dbContext.Events
            .Include(ev => ev.Attendees)
            .FirstOrDefaultAsync(ev => ev.Id.Equals(id));

        if (even is null)
        {
            throw new NotFoundException("Event not found with this Id");
        }

        return new ResponseEventJson
        {
            Id = even.Id,
            Title = even.Title,
            Details = even.Details,
            MaximumAttendees = even.Maximum_Attendees,
            AttendeesAmount = even.Attendees.Count()
        };
    }
}
