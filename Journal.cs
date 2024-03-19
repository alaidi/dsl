namespace JournalScraper;
public class Journal
{
    public string? JournalTitle { get; set; }
    public string? ISSN { get; set; }
    public string? JournalLink { get; set; }
    public string? FakeJournalLink { get; set; }
    public string? Publisher { get; set; }
    public string? Indexing { get; set; }
    public string? Reason { get; set; }
    public string JournalType { get; set; } = "Fake";
}
