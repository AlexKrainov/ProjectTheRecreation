using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjectTheRecreation.Models
{
    [MetadataType(typeof(RestMetadata))]
    public partial class Rest
    {
    }

    public class RestMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Экскурсия")]
        public string NameTour { get; set; }

        [Display(Name = "Номер школы")]
        public Nullable<int> NumberSchool { get; set; }

        [Display(Name = "Дата")]
        public Nullable<System.DateTime> Date { get; set; }

        [Display(Name = "ФИО учителя")]
        public string NameTeacher { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Количество детей")]
        public Nullable<int> CountOfChildren { get; set; }

        [Display(Name = "Номер тел. учителя")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Пожелания")]
        public string NextTour { get; set; }

        [Display(Name = "ГАИ")]
        public Nullable<bool> GAI { get; set; }

        [Display(Name = "Стоимость")]
        public Nullable<decimal> Money { get; set; }

        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public static RestType GetType(string NameColumn)
        {
            if (NameColumn == "Дата")
                return RestType.dateType;
            else if (NameColumn == "Стоимость")
                return RestType.decimalType;
            else if (NameColumn == "Номер школы" || NameColumn == "Количество детей")
                return RestType.intType;

            return RestType.stringType;
        }
    }

    public enum RestType
    {
        stringType,
        decimalType,
        intType,
        dateType
    }

}