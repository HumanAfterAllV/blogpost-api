namespace BlogPost.Api.Models;

public class Post
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; } = DateTime.UtcNow;

    //Forengkey con el modelo Comment
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}