namespace Domain.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public virtual AuthorDto? Author { get; set; }
}