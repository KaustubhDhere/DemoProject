using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using DemoProject.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var DB = Client.GetDatabase("Employee");
            var collection = DB.GetCollection<EmployeeDetails>("EmployeeDetails");
            var filter = Builders<EmployeeDetails>.Filter.Eq("Id", id);
            var employee = collection.Find(filter).FirstOrDefault();
            if (employee == null)
            {
                return HttpNotFound(); // Or handle the case when the record is not found
            }

            return View(employee);
            
        }



        // GET: Home
        [HttpPost]
        public ActionResult Index(EmployeeDetails Emp)
        {
            if (ModelState.IsValid)
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("Employee");
                var collection = DB.GetCollection<EmployeeDetails>("EmployeeDetails");
                collection.InsertOneAsync(Emp);
                return RedirectToAction("emplist");
            }
            return View();
        }

    
        public ActionResult emplist()
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            var Client = new MongoClient(constr);
            var db = Client.GetDatabase("Employee");
            var collection = db.GetCollection<EmployeeDetails>("EmployeeDetails").Find(new BsonDocument()).ToList();

            return View(collection);
        }

        public ActionResult Delete(string id)
        {
            if (ModelState.IsValid)
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var DB = Client.GetDatabase("Employee");
                var collection = DB.GetCollection<EmployeeDetails>("EmployeeDetails");
                var DeleteRecored = collection.DeleteOneAsync(
                               Builders<EmployeeDetails>.Filter.Eq("Id", id));
                return RedirectToAction("emplist");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Edit(EmployeeDetails Empdet)
        {
            if (ModelState.IsValid)
            {
                string constr = ConfigurationManager.AppSettings["connectionString"];
                var Client = new MongoClient(constr);
                var Db = Client.GetDatabase("Employee");
                var collection = Db.GetCollection<EmployeeDetails>("EmployeeDetails");

                var update = collection.FindOneAndUpdateAsync(Builders<EmployeeDetails>.Filter.Eq("Id", Empdet.Id), Builders<EmployeeDetails>.Update.Set("Name", Empdet.Name).Set("Department", Empdet.Department).Set("Address", Empdet.Address).Set("City", Empdet.City).Set("Country", Empdet.Country));
              
                return RedirectToAction("emplist");
            }
            return View();

        }
    }
}