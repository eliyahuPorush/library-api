
namespace Domain.Dtos;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public AddressDto Address { get; set; }
    public virtual List<BookDto> Books { get; set; }
}