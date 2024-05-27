using System.ComponentModel.DataAnnotations;

namespace PassIn.Infrastructure.Entities;
public class CheckIn
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Created_at { get; set; }


    public Guid Attendee_Id { get; set; }
    public Attendee? Attendee { get; set; }
}
