using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.IO;
using WebScraper.Classes;

namespace WebScraper
{
    class Program
    {
        static void Main()
        {
            string projetoPath = Directory.GetCurrentDirectory(); // Obtém o diretório do projeto
            string driverPath = GetChromeDriverPath(projetoPath);

            var chromeOptions = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(driverPath, chromeOptions);

            try
            {
                string url = "https://sisazul.sjp.pr.gov.br/webapp/portaltransparencia/despesas";
                int ano = 2023; // Substitua pelo ano desejado
                string mes = "Janeiro"; // Substitua pelo mês desejado

                // Criar uma instância do Scraper
                var scraper = new Scraper(driver);

                // Executar o scraping
                scraper.ExecutarScraping(url, ano, mes);

                // ...
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                // Fechar o navegador após o scraping
                driver.Quit();
            }
        }

        static string GetChromeDriverPath(string projetoPath)
        {
            var driversPath = Path.Combine(projetoPath, "Drivers");
            var chromeDriverPath = Path.Combine(driversPath, "chromedriver.exe");

            if (!File.Exists(chromeDriverPath))
            {
                Console.WriteLine("O arquivo chromedriver.exe não foi encontrado. Certifique-se de que está na pasta 'Drivers'.");
                // Pode adicionar lógica adicional aqui, como baixar o chromedriver automaticamente.
            }

            return driversPath;
        }
    }
}