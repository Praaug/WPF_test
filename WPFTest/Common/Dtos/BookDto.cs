namespace Common;

public class BookDto
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public bool IsCheckedOut { get; set; }
}