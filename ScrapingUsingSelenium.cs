namespace JournalScraper;


public static class ScrapingUsingSelenium
{

    static Journal SaveJournal(ChromeDriver driver, string nameAttributeValue)
    {
        try
        {
            var document = new HtmlDocument();
            Journal journal = new();
            var wait2 = new WebDriverWait(driver, new TimeSpan(0, 0, 30));

            var inputElement = wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath($"//input[@name='{nameAttributeValue}'][@value='عـرض']")));
            inputElement.Click();
            document.LoadHtml(driver.PageSource);
            journal.JournalTitle = document.DocumentNode.SelectSingleNode("//table[1]//tr[2]//td[1]").InnerText.Trim();
            journal.ISSN = document.DocumentNode.SelectSingleNode("//table[1]//tr[2]//td[2]").InnerText.Trim();
            journal.JournalLink = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[1]//a").GetAttributeValue("href", "");
            // var fakeJournalLinkNode = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[2]//a");
            // string? fakeJournalLink = fakeJournalLinkNode?.GetAttributeValue("href", "");
            journal.FakeJournalLink = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[2]//a")?.GetAttributeValue("href", "");

            // journal.FakeJournalLink = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[2]//a").GetAttributeValue("href", "");
            journal.Publisher = document.DocumentNode.SelectSingleNode("//table[3]//tr[2]//td[1]").InnerText.Trim();
            journal.Indexing = document.DocumentNode.SelectSingleNode("//table[3]//tr[2]//td[2]").InnerText.Trim();
            if (nameAttributeValue.Contains("edit1"))
            {
                journal.Reason = document.DocumentNode.SelectSingleNode("//table[4]//tr[2]//td[1]").InnerText.Trim();
                journal.JournalType = "Commercial";
            }
            var backLink = driver.FindElement(By.XPath("//a[contains(@href, 'dis.php')]"));
            backLink.Click();
            return journal;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
            return new Journal();
        }
    }
    public static List<Journal> DoScraping()
    {
        var journalList = new List<Journal>();
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-dev-shm-usage");
        chromeOptions.AddExcludedArguments(new List<string> { "enable-automation", "enable-logging" });
        chromeOptions.UseSpecCompliantProtocol = true;
        ChromeDriverService? service;
        try
        {
            service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            ChromeDriver driver = new(service, chromeOptions);
            driver.Navigate()
                .GoToUrl($"https://jor.rdd.edu.iq/index.php");

            var wait1 = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'المجلات المزيفة والتجارية')]"))).Click();

            var document = new HtmlDocument();
            document.LoadHtml(driver.PageSource);
            var rows = document.DocumentNode.SelectNodes("//table[@class='arabic']//tr[not(@style='color: white; background-color: black')]");
            foreach (var row in rows)
            {
                var journalTitle2 = row.SelectSingleNode(".//td[2]").InnerText.Trim();
                var link = row.SelectSingleNode(".//td[3]//input");
                if (link?.GetAttributeValue("name", "").StartsWith("edit") == false) continue;
                var nameAttributeValue = link?.GetAttributeValue("name", "");
                if (nameAttributeValue == null)
                {
                    continue;
                };
                // if (nameAttributeValue.Contains("edit1"))
                //{
                journalList.Add(SaveJournal(driver, nameAttributeValue));
                // }

            }

            //  System.Console.WriteLine(journalList.Count);
            //SaveAsCsv(journalList);
            //  string jsonString = JsonSerializer.Serialize(journalList, options: new JsonSerializerOptions { WriteIndented = true });

            // Write JSON string to file
            //   File.WriteAllText("journals.json", jsonString);

            driver.Quit();
            service.Dispose();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return journalList;
    }
}