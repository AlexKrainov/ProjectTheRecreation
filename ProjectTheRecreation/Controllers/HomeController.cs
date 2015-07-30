using ClosedXML.Excel;
using ProjectTheRecreation.Excel;
using ProjectTheRecreation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjectTheRecreation.Controllers
{
    public class HomeController : Controller
    {
        private RestManager db;
        public HomeController()
        {
            var selectListItem = new List<SelectListItem>();
            selectListItem.Add(new SelectListItem { Text = "Экскурсия", Value = "NameTour" });
            selectListItem.Add(new SelectListItem { Text = "Номер школы", Value = "NumberSchool" });
            selectListItem.Add(new SelectListItem { Text = "Дата", Value = "Date" });
            selectListItem.Add(new SelectListItem { Text = "ФИО учителя", Value = "NameTeacher" });
            selectListItem.Add(new SelectListItem { Text = "Email", Value = "Email" });
            selectListItem.Add(new SelectListItem { Text = "Количество детей", Value = "CountOfChildren" });
            selectListItem.Add(new SelectListItem { Text = "Номер тел. учителя", Value = "PhoneNumber" });
            selectListItem.Add(new SelectListItem { Text = "Пожелания", Value = "NextTour" });
            selectListItem.Add(new SelectListItem { Text = "ГАИ", Value = "GAI" });
            selectListItem.Add(new SelectListItem { Text = "Стоимость", Value = "Money" });
            selectListItem.Add(new SelectListItem { Text = "Комментарий", Value = "Comment" });

            ViewBag.selectListItem = selectListItem;

            db = new RestManager();
        }


        public ActionResult Index()
        {
            ViewBag.Rests = db.GetRests();
            return View();
        }
        [HttpPost]
        public ActionResult Index(string GlobalShearchTextBox, string CollectionColumn, bool Excel)
        {
            if (string.IsNullOrEmpty(GlobalShearchTextBox))
            {
                ViewBag.Rests = db.GetRests();

                if (Excel)
                    ExcelReport(ViewBag.Rests);

                return View();
            }

            #region Filter
            if (CollectionColumn == "NameTour")
                ViewBag.Rests = db.GetRests().Where(x => x.NameTour.Contains(GlobalShearchTextBox));
            else if (CollectionColumn == "NumberSchool")
            {
                int result;
                int.TryParse(GlobalShearchTextBox, out result);
                ViewBag.Rests = db.GetRests().Where(x => x.NumberSchool == result);
            }
            else if (CollectionColumn == "Date")
            {
                DateTime result;
                int resultInt;
                var dateBool = DateTime.TryParse(GlobalShearchTextBox, out result);
                var dateBoolInt = int.TryParse(GlobalShearchTextBox, out resultInt);

                if (dateBoolInt)
                {
                    ViewBag.Rests = db.GetRests().Where(x => x.Date.Value.Year == resultInt);    
                }else 
                ViewBag.Rests = db.GetRests().Where(x => x.Date.Value == result);
            }
            else if (CollectionColumn == "NameTeacher")
                ViewBag.Rests = db.GetRests().Where(x => x.NameTeacher.Contains(GlobalShearchTextBox));
            else if (CollectionColumn == "Email")
                ViewBag.Rests = db.GetRests().Where(x => x.Email.Contains(GlobalShearchTextBox));
            else if (CollectionColumn == "CountOfChildren")
            {
                int result;
                int.TryParse(GlobalShearchTextBox, out result);
                ViewBag.Rests = db.GetRests().Where(x => x.CountOfChildren == result);
            }
            else if (CollectionColumn == "PhoneNumber")
                ViewBag.Rests = db.GetRests().Where(x => x.PhoneNumber.Contains(GlobalShearchTextBox));
            else if (CollectionColumn == "NextTour")
                ViewBag.Rests = db.GetRests().Where(x => x.NextTour.Contains(GlobalShearchTextBox));
            else if (CollectionColumn == "GAI")
            {
                bool result = false;
                var value = GlobalShearchTextBox.ToLower();

                if (value == "да" || value == "true" || value == "yes")
                    result = true;

                ViewBag.Rests = db.GetRests().Where(x => x.GAI == result);
            }
            else if (CollectionColumn == "Money")
            {
                decimal result;
                decimal.TryParse(GlobalShearchTextBox, out result);
                ViewBag.Rests = db.GetRests().Where(x => x.Money == result);
            }
            if (CollectionColumn == "Comment")
                ViewBag.Rests = db.GetRests().Where(x => x.Comment.Contains(GlobalShearchTextBox));
            #endregion

            if (Excel)
                ExcelReport(ViewBag.Rests);

            return View();
        }


        #region Edit
        public ActionResult Edit(int id)
        {
            return View(db.GetRest(id));
        }

        [HttpPost]
        public ActionResult Edit(Rest rest)
        {
            if (this.ModelState.IsValid)
            {
                db.SaveRest(rest);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Rest rest)
        {
            if (this.ModelState.IsValid)
            {
                db.CreateNewRest(rest);
                return RedirectToAction("Index");
            }
            else
                return View();
        }
        #endregion
        public ActionResult Delete(int? id)
        {
            if (id != null)
                db.Delete(id);

            return RedirectToAction("Index");
        }

        public void ExcelReport(IQueryable<Rest> rests)
        {
            DataTableClass dt = new DataTableClass(rests);

            using (var stream = new MemoryStream())
            {
                //var builder = new ExcelReportBuilder();
                var builder = new ReportViewExcelBuilder();
                builder.Create(stream, dt.dt);

                Response.Clear();
                Response.ContentType = "text/html";
                Response.AddHeader("content-disposition", "attachment;fileName=RestReport" + DateTime.Now.ToString("_dd_MM_yyyy") + ".xlsx");
                Response.ContentEncoding = Encoding.UTF8;
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }

            // XLWorkbook wb = new XLWorkbook();
            // wb.Worksheets.Add(dt.dt, "WorksheetName");
            // var work = wb.Worksheets;
            // var z = "C:\\Users\\" + wb.Author + "\\Downloads\\RestReport" + DateTime.Now.ToString("_dd_MM_yyyy") + ".xlsx";
            //wb.SaveAs("C:\\Users\\" + wb.Author + "\\Downloads\\RestReport" + DateTime.Now.ToString("_dd_MM_yyyy") + ".xlsx");

            //ViewBag.Rests = db.GetRests();

            //return RedirectToAction("Index");
        }
    }
}