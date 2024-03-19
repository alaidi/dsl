using System.Text.Json;
using JournalScraper;


var journalList2 = ScrapingUsingPlaywright.DoScraping();
//return;
// var journalList = ScrapingUsingSelenium.DoScraping();
// string jsonString = JsonSerializer.Serialize(journalList, options: new JsonSerializerOptions { WriteIndented = true });
// File.WriteAllText("journals.json", jsonString);