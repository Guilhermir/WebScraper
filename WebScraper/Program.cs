using System;
using WebScraper.Classes;
using WebScraper.Helpers;

namespace WebScraper
{
    class Program
    {
        static void Main()
        {
            using (var webDriverHelper = new ChromeDriver())
            {
                try
                {
                    // URL do site que será alvo do scraping
                    string url = "https://sisazul.sjp.pr.gov.br/webapp/portaltransparencia/despesas";

                    // Ano e mês para os quais você deseja extrair dados
                    int ano = 2023;
                    string mes = "Janeiro";

                    var scraper = new Scraper(webDriverHelper.Driver);

                    scraper.ExecutarScraping(url, ano, mes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }
        }
    }
}