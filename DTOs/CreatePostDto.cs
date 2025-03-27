namespace BlogPost.Api.DTOs;

public class CreatePostDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    // Author fijo internamente, por lo tanto omitido aquí.
}
