using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace WebScraper.Helpers
{
    /// <summary>
    /// Classe para gerenciar a criação e utilização de uma instância do ChromeDriver.
    /// </summary>
    public class ChromeDriver : IDisposable
    {
        private IWebDriver _driver;
        private readonly string _projetoPath;
        private readonly string _driverPath;

        public IWebDriver Driver => _driver;

        public ChromeDriver()
        {
            _projetoPath = Directory.GetCurrentDirectory();

            _driverPath = GetChromeDriverPath(_projetoPath);

            var chromeOptions = new ChromeOptions();

            _driver = new OpenQA.Selenium.Chrome.ChromeDriver(_driverPath, chromeOptions);
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        private string GetChromeDriverPath(string projetoPath)
        {
            var driversPath = Path.Combine(projetoPath, "Drivers");

            var chromeDriverPath = Path.Combine(driversPath, "chromedriver.exe");

            return driversPath;
        }
    }
}
