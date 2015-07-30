#define Force_Formula_Recalculations
#define Viktor
#define Alex

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using ProjectTheRecreation.Models;

namespace ProjectTheRecreation.Excel
{
    public sealed class ReportViewExcelBuilder : BaseExcelBuilder
    {
        public override void addDataRows(SheetData sheetData, DataTable dt)
        {
            // бежим по строкам
            foreach (DataRow dataRow in dt.Rows)
            {
                var row = new Row();

                for (int i = 0; i < dataRow.ItemArray.Length; i++)
                {
                    var cell = new Cell();
                    var cellValue = new CellValue();

                    ApplyStyle(dataRow[i], RestMetadata.GetType(dt.Columns[i].ColumnName), ref cell, ref cellValue);

                    cell.Append(cellValue);
                    row.AppendChild(cell);
                }
                sheetData.AppendChild(row);
            }
        }

    }
}
