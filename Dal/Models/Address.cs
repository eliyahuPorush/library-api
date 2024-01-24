using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;

namespace Dal.Models;

public class Address
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int House { get; set; }
}