using System.ComponentModel.DataAnnotations;

namespace BlogPost.Api.Models;
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty.ToString();
    public string Role { get; set; } = "User";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}

