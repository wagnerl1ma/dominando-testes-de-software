using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace NerdStore.BDD.Tests.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, string caminhoDriver, bool headless) //headless: navegar de forma invisivel, o browser vai estar aberto mas não é possível ver
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    var optionsFireFox = new FirefoxOptions();
                    if (headless)
                        optionsFireFox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(caminhoDriver, optionsFireFox);

                    break;
                case Browser.Chrome:
                    var options = new ChromeOptions();
                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(caminhoDriver, options);

                    break;
            }

            return webDriver;
        }
    }
}

