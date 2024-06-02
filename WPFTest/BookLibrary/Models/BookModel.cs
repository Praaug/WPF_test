using System.Windows.Controls;

namespace BookLibrary.Models;

public class BookModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public Image CoverImage { get; set; }
    
    public int Id { get; set; }
}