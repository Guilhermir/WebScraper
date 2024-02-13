using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using WebScraper.Classes;

namespace WebScraper.Helpers
{
    public static class ExtrairDados
    {
        public static List<Dados> ExtracaoDados(IWebDriver driver)
        {
            var listaDados = new List<Dados>();

            var tabela = driver.FindElement(By.CssSelector("#tblDespesasLista"));

            var linhas = tabela.FindElements(By.CssSelector("tr"));

            for (int i = 1; i < linhas.Count; i++)
            {
                var colunas = linhas[i].FindElements(By.CssSelector("td"));

                var dadosDespesa = SalvarDados(colunas);
                listaDados.Add(dadosDespesa);
            }

            return listaDados;
        }

        // Método privado para criar um objeto Dados a partir de colunas específicas da tabela.
        private static Dados SalvarDados(IReadOnlyList<IWebElement> colunas)
        {
            return new Dados
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
        }
    }
}