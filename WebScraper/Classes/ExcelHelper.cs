using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using WebScraper.Classes;

namespace WebScraper.Helpers
{
    public static class ExcelHelper
    {
        static ExcelHelper()
        {
            // Configura o contexto de licença para uso não comercial do EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static void SalvarDadosEmExcel(List<DadosDespesa> listaDados, string caminhoArquivo)
        {
            // Cria um novo pacote Excel
            using (var package = new ExcelPackage())
            {
                // Adiciona uma planilha ao pacote Excel
                var worksheet = package.Workbook.Worksheets.Add("DadosDespesa");

                // Adiciona cabeçalhos à planilha
                worksheet.Cells[1, 1].Value = "Orgao";
                worksheet.Cells[1, 2].Value = "Empenho";
                worksheet.Cells[1, 3].Value = "AnoEmpenho";
                worksheet.Cells[1, 4].Value = "DataEmissao";
                worksheet.Cells[1, 5].Value = "Valor";
                worksheet.Cells[1, 6].Value = "Liquidado";
                worksheet.Cells[1, 7].Value = "ValorPago";
                worksheet.Cells[1, 8].Value = "PEPercentual";

                // Preenche os dados na planilha
                for (int i = 0; i < listaDados.Count; i++)
                {
                    var row = i + 2; // Começa da segunda linha
                    var dado = listaDados[i];

                    worksheet.Cells[row, 1].Value = dado.Orgao;
                    worksheet.Cells[row, 2].Value = dado.Empenho;
                    worksheet.Cells[row, 3].Value = dado.AnoEmpenho;
                    worksheet.Cells[row, 4].Value = dado.DataEmissao.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 5].Value = dado.Valor;
                    worksheet.Cells[row, 6].Value = dado.Liquidado;
                    worksheet.Cells[row, 7].Value = dado.ValorPago;
                    worksheet.Cells[row, 8].Value = dado.PEPercentual;
                }

                // Salva o pacote Excel em um arquivo
                FileInfo excelFile = new FileInfo(caminhoArquivo);
                package.SaveAs(excelFile);

                Console.WriteLine($"Os dados foram salvos no arquivo: {caminhoArquivo}");
            }
        }
    }
}
