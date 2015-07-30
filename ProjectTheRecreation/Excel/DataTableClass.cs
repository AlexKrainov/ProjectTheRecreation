using ProjectTheRecreation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectTheRecreation.Excel
{
    public class DataTableClass
    {
        public DataTable dt;
        private IEnumerable<Rest> collection;
        public DataTableClass(IEnumerable<Rest> Collection)
        {
            this.collection = Collection;
            dt = new DataTable();
            CreateHeaderRow();
            CreateRow();
        }

        private void CreateHeaderRow()
        {
            dt.Columns.Add("Экскурсия");
            dt.Columns.Add("Номер школы");
            dt.Columns.Add("Дата");
            dt.Columns.Add("ФИО учителя");
            dt.Columns.Add("Email");
            dt.Columns.Add("Количество детей");
            dt.Columns.Add("Номер тел. учителя");
            dt.Columns.Add("Пожелания");
            dt.Columns.Add("ГАИ");
            dt.Columns.Add("Стоимость");
            dt.Columns.Add("Комментарий");
        }

        private void CreateRow()
        {
            var list = collection.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                object[] o = new object[] {
                    list[i].NameTour,
                    list[i].NumberSchool,
                    list[i].Date,
                    list[i].NameTeacher,
                    list[i].Email,
                    list[i].CountOfChildren,
                    list[i].PhoneNumber,
                    list[i].NextTour,
                    GetGai(list[i].GAI),
                    list[i].Money,
                    list[i].Comment
                };
                dt.Rows.Add(o);
            }

        }

        private object GetMoney(decimal? nullable)
        {
            if (!nullable.HasValue)
                return "";
            else
                return nullable.Value.ToString("### ### ### ### ###");
        }

        private object GetGai(bool? nullable)
        {
            if (nullable == null)
                return "";
            else if (nullable == false)
                return "Нет";
            else
                return "Да";
        }
    }
}