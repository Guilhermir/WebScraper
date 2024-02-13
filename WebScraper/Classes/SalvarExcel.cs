using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using WebScraper.Classes;

namespace WebScraper.Helpers
{
    public static class SalvarExcel
    {
        // O construtor estático é usado para configurar a licença do EPPlus para uso não comercial.
        static SalvarExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        public static void SalvarDadosEmExcel(List<Dados> listaDados, string caminhoArquivo)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DadosDespesa");

                // Adiciona cabeçalhos à planilha.
                worksheet.Cells[1, 1].Value = "Orgao";
                worksheet.Cells[1, 2].Value = "Empenho";
                worksheet.Cells[1, 3].Value = "AnoEmpenho";
                worksheet.Cells[1, 4].Value = "DataEmissao";
                worksheet.Cells[1, 5].Value = "Valor";
                worksheet.Cells[1, 6].Value = "Liquidado";
                worksheet.Cells[1, 7].Value = "ValorPago";
                worksheet.Cells[1, 8].Value = "PEPercentual";

                // Preenche os dados na planilha.
                for (int i = 0; i < listaDados.Count; i++)
                {
                    var row = i + 2;
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

                // Especifica o caminho onde o arquivo Excel será salvo.
                FileInfo excelFile = new FileInfo(caminhoArquivo);

                // Salva o pacote Excel em um arquivo.
                package.SaveAs(excelFile);

                Console.WriteLine($"Os dados foram salvos no arquivo: {caminhoArquivo}");
            }
        }
    }
}

