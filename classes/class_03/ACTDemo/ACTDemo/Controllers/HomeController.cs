using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACTDemo.Models;

namespace ACTDemo.Controllers
{
    public class HomeController : Controller
    {
        private testdbEntities dbConnection = new testdbEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="Id")] Meal mealToadd)
        {
            try
            {
                // TODO: Add insert logic here
                dbConnection.Meals.Add(mealToadd);
                dbConnection.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            // Meal mealToEdit = dbConnection.Meals.Find(id);
            var mealToEdit = (from meal in dbConnection.Meals
                              where meal.Id == id
                              select meal).First();
            return View(mealToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Meal mealToEdit)
        {
            var originalValues = (from meal in dbConnection.Meals
                                  where meal.Id == mealToEdit.Id
                                  select meal).First();
            try
            {
                // TODO: Add update logic here
                dbConnection.Entry(originalValues).CurrentValues.SetValues(mealToEdit);
                dbConnection.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(originalValues);
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Meal mealToRemove = dbConnection.Meals.Find(id);
            return View(mealToRemove);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Meal mealToRemove = dbConnection.Meals.Find(id);
                dbConnection.Meals.Remove(mealToRemove);
                dbConnection.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
