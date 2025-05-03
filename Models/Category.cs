using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_MVC.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Item>? Items { get; set; } = new List<Item>();

}