using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.IO;
using WebScraper.Helpers;

namespace WebScraper
{
    class Program
    {
        static void Main()
        {
            var chromeOptions = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(chromeOptions);


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
    }
}