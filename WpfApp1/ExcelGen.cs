using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfApp1
{
    class ExcelGen
    {
        Excel.Application _app;
        Excel.Workbook _workbook;
        Excel.Worksheet _worksheet;
        public ExcelGen()
        {
            _app = new Excel.Application();
            _workbook = _app.Workbooks.Add();
            _worksheet = _app.Worksheets[1];
            _worksheet.Name = "VeryIMPOSSIBLEOTCHET";
        }
        public void Generate(string path)
        {
            Random random = new Random();
            _worksheet.Cells[1, 1] = "Траты за месяц";
            for (int i = 2; i <= 32; i++)
            {
                _worksheet.Cells[i, 1] = (i - 1).ToString();
                _worksheet.Cells[i, 2] = ((random.Next(15) * 100).ToString());
            }
            Excel.Range begin = _worksheet.Cells[2, 2];
            Excel.Range end = _worksheet.Cells[32, 2];
            Excel.Range range = _worksheet.Range[begin,end];

            _worksheet.Cells[33,2].Formula = String.Format("=SUM({0})", range.Address);
            _worksheet.Cells[33, 2].Calculate();
            _worksheet.Cells[33, 1] = "Итого: ";

            Excel.Chart chart = _app.Charts.Add();
            chart.Location(Excel.XlChartLocation.xlLocationAsObject, "VeryIMPOSSIBLEOTCHET");
            Excel.Shape shape = _worksheet.Shapes.Item(1);
            shape.Left = 100;
            Excel.SeriesCollection seriesCollection =
            (Excel.SeriesCollection)_workbook.ActiveChart.SeriesCollection(Type.Missing);
            Excel.Series series = seriesCollection.Item(1);

            series.Values = range;
            series.Name = "Trati";
            series.ChartType = Excel.XlChartType.xl3DBarClustered;
            

            _workbook.SaveAs(path);
            _workbook.Close();
        }
    }

}
