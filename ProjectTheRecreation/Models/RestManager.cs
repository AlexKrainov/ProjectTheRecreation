using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTheRecreation.Models
{
    public class RestManager
    {
        private  RestDatabaseEntities db;

        public RestManager()
        {
            db = new RestDatabaseEntities();
            
        }

        public IQueryable<Rest> GetRests()
        {
            return db.Rests;
        }

        public Rest GetRest(int id)
        {
            return db.Rests.SingleOrDefault(x => x.Id == id);
        }


        internal void SaveRest(Rest rest)
        {
            var oldRest = db.Rests.SingleOrDefault(x => x.Id == rest.Id);
            oldRest.Comment = rest.Comment;
            oldRest.CountOfChildren = rest.CountOfChildren;
            oldRest.Date = rest.Date;
            oldRest.GAI = rest.GAI;
            oldRest.Money = rest.Money;
            oldRest.NameTeacher = rest.NameTeacher;
            oldRest.NameTour = rest.NameTour;
            oldRest.NextTour = rest.NextTour;
            oldRest.NumberSchool = rest.NumberSchool;
            oldRest.PhoneNumber = rest.PhoneNumber;
            oldRest.Email = rest.Email;
            
            db.SaveChanges();
        }

        internal void CreateNewRest(Rest rest)
        {
            db.Rests.Add(rest);
            db.SaveChanges();
        }

        internal void Delete(int? id)
        {
            var deleteRest = db.Rests.First(x => x.Id == id);
            db.Rests.Remove(deleteRest);

            db.SaveChanges();
        }
    }
}