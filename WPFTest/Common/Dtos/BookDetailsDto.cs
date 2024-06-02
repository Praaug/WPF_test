namespace Common;

public class BookDetailsDto : BookDto
{
    public int BookId { get; set; }
    public double Price { get; set; }
    public string Publisher { get; set; }
    public string Language { get; set; }
    public string Edition { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int PageCount { get; set; }
    public double CompletionPercentage { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
}