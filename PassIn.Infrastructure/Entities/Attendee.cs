using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [ForeignKey("Attendee_Id")]
    public CheckIn? CheckIn { get; set; }
}
