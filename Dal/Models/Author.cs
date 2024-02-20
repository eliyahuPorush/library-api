﻿
namespace Dal.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
    public virtual List<Book> Books { get; set; }
}