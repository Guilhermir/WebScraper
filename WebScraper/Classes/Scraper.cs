using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.IO;
using WebScraper.Helpers;

namespace WebScraper.Classes
{
    public class Scraper
    {
        private readonly IWebDriver _driver;
        private List<DadosDespesa> dadosDaPagina = new List<DadosDespesa>(); // Definindo a variável no escopo da classe

        public Scraper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ExecutarScraping(string url, int ano, string mes)
        {
            // Acessar a URL
            AcessarURL(url);

            // Preencher o campo de ano
            PreencherAno(ano);

            // Preencher o dropdown de mês
            PreencherMes(mes);

            // Clicar no botão de pesquisa
            ClicarPesquisar();

            // Esperar a página carregar
            AguardarPaginaCarregar();

            // Lógica para percorrer todas as páginas (exemplo: 20 páginas)
            for (int pagina = 1; pagina <= 20; pagina++)
            {
                Console.WriteLine($"Processando página {pagina}");

                // Lógica para extrair dados da página
                dadosDaPagina.AddRange(ExtrairDados());

                // Lógica para navegar para a próxima página (exemplo: clicar em um botão de próxima página)
                if (!NavegarParaProximaPagina())
                {
                    Console.WriteLine("Não há mais páginas disponíveis.");
                    break;
                }

                // Esperar a página carregar antes de continuar
                AguardarPaginaCarregar();
            }

            // Especificar o caminho onde o arquivo Excel será salvo
            string caminhoArquivoExcel = Path.Combine(Directory.GetCurrentDirectory(), "Dados.xlsx");

            // Salvar os dados em um arquivo Excel
            ExcelHelper.SalvarDadosEmExcel(dadosDaPagina, caminhoArquivoExcel);
        }

        private void AcessarURL(string url)
        {
            _driver.Navigate().GoToUrl(url);
            // Adicione lógica adicional de login, se necessário
        }

        private void PreencherAno(int ano)
        {
            var campoAno = _driver.FindElement(By.CssSelector("#nr_ano_empenho")); // Ajuste conforme o seletor correto
            campoAno.Clear();
            campoAno.SendKeys(ano.ToString());
        }

        private void PreencherMes(string mes)
        {
            var dropdownMes = EsperarElemento(_driver, By.CssSelector("#mes")); // Ajuste conforme o seletor correto
            var selectElement = new SelectElement(dropdownMes);
            selectElement.SelectByText(mes);
        }

        private void ClicarPesquisar()
        {
            var botaoPesquisar = _driver.FindElement(By.XPath("//*[@id='collapseOne']/div/div/div[5]/div/button[2]")); // Ajuste conforme o XPath correto
            botaoPesquisar.Click();
        }

        private void AguardarPaginaCarregar()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#tblDespesasLista"))); // Ajuste conforme o seletor correto
        }

        private List<DadosDespesa> ExtrairDados()
        {
            var listaDados = new List<DadosDespesa>();

            // Localizar a tabela (ajuste o seletor CSS conforme necessário)
            var tabela = _driver.FindElement(By.CssSelector("#tblDespesasLista"));

            // Localizar todas as linhas da tabela
            var linhas = tabela.FindElements(By.CssSelector("tr"));

            // Iterar sobre as linhas, começando da segunda linha para evitar cabeçalhos
            for (int i = 1; i < linhas.Count; i++)
            {
                var colunas = linhas[i].FindElements(By.CssSelector("td"));

                // Criar um objeto DadosDespesa e preencher com os dados da linha
                var dadosDespesa = new DadosDespesa
                {
                    Orgao = colunas[0].Text,
                    Empenho = colunas[1].Text,
                    AnoEmpenho = Convert.ToInt32(colunas[2].Text),
                    DataEmissao = DateTime.ParseExact(colunas[3].Text, "dd/MM/yyyy", null),
                    Valor = Convert.ToDecimal(colunas[4].Text.Replace("R$", "").Trim()),
                    Liquidado = Convert.ToDecimal(colunas[5].Text.Replace("R$", "").Trim()),
                    ValorPago = Convert.ToDecimal(colunas[6].Text.Replace("R$", "").Trim()),
                    PEPercentual = Convert.ToDecimal(colunas[7].Text.Replace("%", "").Trim())
                };

                // Adicionar o objeto à lista de dados
                listaDados.Add(dadosDespesa);
            }

            return listaDados;
        }

        private void ExibirDados(List<DadosDespesa> dados)
        {
            foreach (var dado in dados)
            {
                Console.WriteLine($"Orgao: {dado.Orgao}, Empenho: {dado.Empenho}, AnoEmpenho: {dado.AnoEmpenho}, " +
                                  $"DataEmissao: {dado.DataEmissao}, Valor: {dado.Valor}, Liquidado: {dado.Liquidado}, " +
                                  $"ValorPago: {dado.ValorPago}, PEPercentual: {dado.PEPercentual}");
            }
        }

        private bool NavegarParaProximaPagina()
        {
            var proximaPagina = _driver.FindElements(By.CssSelector("#tblDespesasLista_next"));

            if (proximaPagina.Count > 0 && proximaPagina[0].Displayed)
            {
                proximaPagina[0].Click();
                return true;
            }

            return false;
        }

        private IWebElement EsperarElemento(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementExists(by));
        }
    }
}


