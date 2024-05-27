using System.ComponentModel.DataAnnotations;

namespace PassIn.Infrastructure.Entities;
public class Attendee
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Created_At { get; set; }

    public Guid Event_Id { get; set; }
    public Event? Event { get; set; }
}
