using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace ExcelLib
{
    public class Excel
    {
        string Path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;

        public Excel(string path, int sheetNum)
        {
            this.Path = path;
            try
            {
                wb = excel.Workbooks.Open(path);
            }
            catch
            {
                var app = new Microsoft.Office.Interop.Excel.Application();
                wb = app.Workbooks.Add();
            }
            finally
            {
                for(int i = 1; i < sheetNum; ++i)
                {
                    wb.Worksheets.Add();
                }
            }
        }

        public void Close()
        {
            wb.Close();
        }
        public void Close(string path)
        {
            wb.SaveAs(path);
            wb.Close();
        }

        public string ReadCell(int i, int j, int sheet)
        {
            Worksheet ws = wb.Worksheets[sheet];
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2.ToString();
            else
                return "";
        }
        public void WriteCell(int i, int j, dynamic x, int sheet)
        {
            Worksheet ws = wb.Worksheets[sheet];
            i++;
            j++;
            if (x == null) x = "";
            ws.Cells[i, j].Value2 = x;
        }
    }
}
