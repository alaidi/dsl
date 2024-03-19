using JournalScraper;
namespace JournalScraper;

public static class SaveCsv
{
    static string EscapeCsvField(string? field)
    {
        if (field == null)
            return "";

        // Check if the field contains comma or double quote
        if (field.Contains(",") || field.Contains("\""))
        {
            // Escape double quotes by doubling them and surround the field with double quotes
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        else
        {
            return field;
        }
    }
    public static bool Save(List<Journal> journals, string path = "journals.csv")
    {
        try
        {
            using (var writer = new StreamWriter(path))
            {
                // Write header
                writer.WriteLine("JournalTitle,ISSN,JournalLink,FakeJournalLink,Publisher,Indexing,Reason,JournalType");

                // Write data
                foreach (var journal in journals)
                {
                    if (journal == null)
                        continue;
                    writer.WriteLine($"{EscapeCsvField(journal.JournalTitle)},{EscapeCsvField(journal.ISSN)},{EscapeCsvField(journal.JournalLink)},{EscapeCsvField(journal.FakeJournalLink)},{EscapeCsvField(journal.Publisher)},{EscapeCsvField(journal.Indexing)},{EscapeCsvField(journal.Reason)},{EscapeCsvField(journal.JournalType)}");
                }
            }
            return true;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            return false;
        }
    }



}
