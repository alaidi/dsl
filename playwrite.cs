using Microsoft.Playwright;

namespace JournalScraper;

public static class ScrapingUsingPlaywright
{
    public static async Task<List<Journal>> DoScraping()
    {
        var journalList = new List<Journal>();
        // var document = new HtmlDocument();
        // var playwrightOptions = new BrowserTypeLaunchOptions
        // {
        //     Headless = true,
        //     Args = ["--no-sandbox", "--disable-dev-shm-usage"]
        // };
        try
        {
            System.Console.WriteLine("Scraping using Playwright");
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://playwright.dev/dotnet");
            await page.ScreenshotAsync(new()
            {
                Path = "screenshot.png"
            });
            System.Console.WriteLine("Screenshot saved to screenshot.png");
            // var playwright = await Playwright.CreateAsync();
            // var browser = await playwright.Chromium.LaunchAsync(playwrightOptions);
            // var page = await browser.NewPageAsync();
            // await page.GotoAsync("https://jor.rdd.edu.iq/index.php");
            // await page.GetByRole(AriaRole.Link, new() { Name = "المجلات المزيفة والتجارية" }).ClickAsync();
            // var html = await page.InnerHTMLAsync("body");
            // document.LoadHtml(html);

            // soup = BeautifulSoup(html, 'html.parser');
            // var wait1 = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            // var inputElement = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath($"//input[@name='{nameAttributeValue}'][@value='عـرض']")));
            // inputElement.Click();
            // var document = new HtmlDocument();
            // document.LoadHtml(await page.ContentAsync());
            // journal.JournalTitle = document.DocumentNode.SelectSingleNode("//table[1]//tr[2]//td[1]").InnerText.Trim();
            // journal.ISSN = document.DocumentNode.SelectSingleNode("//table[1]//tr[2]//td[2]").InnerText.Trim();
            // journal.JournalLink = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[1]//a").GetAttributeValue("href", "");
            // journal.FakeJournalLink = document.DocumentNode.SelectSingleNode("//table[2]//tr[2]//td[2]//a")?.GetAttributeValue("href", "");
            // journal.Publisher = document.DocumentNode.SelectSingleNode("//table[3]//tr[2]//td[1]").InnerText.Trim();
            // journal.Indexing = document.DocumentNode.SelectSingleNode("//table[3]//tr[2]//td[2]").InnerText.Trim();
            // if (nameAttributeValue.Contains("edit1"))
            // {
            //     journal.Reason = document.DocumentNode.SelectSingleNode("//table[4]//tr[2]//td[1]").InnerText.Trim();
            //     journal.JournalType = "Commercial";
            // }
            // var backLink = driver.FindElement(By.XPath("//a[contains(@href, 'dis.php')]"));
            // backLink.Click();
            // return journal;
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
        return journalList;
    }
}