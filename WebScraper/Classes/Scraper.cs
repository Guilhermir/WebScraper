using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using WebScraper.Helpers;

namespace WebScraper.Classes
{
    public class Scraper
    {
        private readonly IWebDriver _driver;
        private List<Dados> dadosDaPagina = new List<Dados>();

        public Scraper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ExecutarScraping(string url, int ano, string mes)
        {

            InteragirPagina.AcessarURL(_driver, url);

            InteragirPagina.PreencherAno(_driver, ano);

            InteragirPagina.PreencherMes(_driver, mes);

            InteragirPagina.ClicarPesquisar(_driver);

            InteragirPagina.AguardarPaginaCarregar(_driver);

            for (int pagina = 1; pagina <= 10; pagina++)
            {
                Console.WriteLine($"Processando página {pagina}");

                // Extrair dados da página atual e adicionar à lista de dados.
                dadosDaPagina.AddRange(ExtrairDados.ExtracaoDados(_driver));

                if (!InteragirPagina.NavegarParaProximaPagina(_driver))
                {
                    Console.WriteLine("Não há mais páginas disponíveis.");
                    break;
                }

                InteragirPagina.AguardarPaginaCarregar(_driver);
            }

            // Especificar o caminho onde o arquivo Excel será salvo.
            string caminhoArquivoExcel = Path.Combine(Directory.GetCurrentDirectory(), "Dados.xlsx");

            // Salvar os dados em um arquivo Excel.
            SalvarExcel.SalvarDadosEmExcel(dadosDaPagina, caminhoArquivoExcel);
        }
    }
}



