namespace WebScraper.Classes
{
    using System;

    public class DadosDespesa
    {
        public string Orgao { get; set; }
        public string Empenho { get; set; }
        public int AnoEmpenho { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal Valor { get; set; }
        public decimal Liquidado { get; set; }
        public decimal ValorPago { get; set; }
        public decimal PEPercentual { get; set; }
    }
}