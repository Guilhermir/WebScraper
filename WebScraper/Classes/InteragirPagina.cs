using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using WebScraper.Classes;

namespace WebScraper.Helpers
{
    public static class InteragirPagina
    {
        public static void AcessarURL(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void PreencherAno(IWebDriver driver, int ano)
        {
            var campoAno = driver.FindElement(By.CssSelector("#nr_ano_empenho"));
            campoAno.Clear();
            campoAno.SendKeys(ano.ToString());
        }

        public static void PreencherMes(IWebDriver driver, string mes)
        {
            var dropdownMes = EsperarElemento(driver, By.CssSelector("#mes"));
            var selectElement = new SelectElement(dropdownMes);
            selectElement.SelectByText(mes);
        }

        public static void ClicarPesquisar(IWebDriver driver)
        {
            var botaoPesquisar = driver.FindElement(By.XPath("//*[@id='collapseOne']/div/div/div[5]/div/button[2]"));
            botaoPesquisar.Click();
        }

        public static void AguardarPaginaCarregar(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#tblDespesasLista")));
        }

        public static bool NavegarParaProximaPagina(IWebDriver driver)
        {
            var proximaPagina = driver.FindElements(By.CssSelector("#tblDespesasLista_next"));

            if (proximaPagina.Count > 0 && proximaPagina[0].Displayed)
            {
                proximaPagina[0].Click();
                return true;
            }

            return false;
        }

        // Método privado para esperar até que um elemento específico exista na página.
        private static IWebElement EsperarElemento(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            return wait.Until(ExpectedConditions.ElementExists(by));
        }
    }
}