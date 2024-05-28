using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkins.DoCheckIn;
public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _dbContext;
    public DoAttendeeCheckInUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseRegisteredJson> Execute(Guid attendeeId)
    {
        await Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        await _dbContext.CheckIn.AddAsync(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisteredJson 
        { 
            Id = entity.Id,
        };
    }

    private async Task Validate(Guid attendeeId)
    {
       var existAttendee = await _dbContext.Attendees.AnyAsync(att => att.Id.Equals(attendeeId));

        if (!existAttendee) {
            throw new NotFoundException("The attendee with this id was not found");
        }

        var existCheckIn = await _dbContext.CheckIn.AnyAsync(ch => ch.Id.Equals(attendeeId));

        if (existCheckIn) {
            throw new ConflictException("Attendee can not do checkin twice in the same event");
        }
    }
}
