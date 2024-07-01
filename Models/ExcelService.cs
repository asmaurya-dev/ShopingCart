using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace ShopingCart.Models
{
    public class ExcelService
    {
        public byte[] GenerateExcelFile()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Sample data (replace with your actual data retrieval logic)
                var data = new List<object[]>
                {
                    new object[] { "John Doe", "john@example.com" },
                    new object[] { "Jane Smith", "jane@example.com" }
                };

                // Add headers
                worksheet.Cells[1, 1].Value = "CategoryName";
                worksheet.Cells[1, 2].Value = "IsActive";

                // Add data rows
                int row = 2;
                foreach (var rowValues in data)
                {
                    for (int i = 0; i < rowValues.Length; i++)
                    {
                        worksheet.Cells[row, i + 1].Value = rowValues[i];
                    }
                    row++;
                }

                // Save the Excel package to a MemoryStream
                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
}
