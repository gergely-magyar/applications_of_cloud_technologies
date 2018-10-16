using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACTDemo.Models;

namespace ACTDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private testdbEntities dbConnection = new testdbEntities();

        private int getAvailableId()
        {
            int availableId = 0;
            foreach (Meal meal in dbConnection.Meals.ToList())
            {
                if (meal.Id > availableId)
                {
                    availableId = meal.Id;
                }
            }
            return availableId + 1;
        }

        // GET api/values
        public IEnumerable<Meal> Get()
        {
            return dbConnection.Meals.ToList();
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            Meal meal = dbConnection.Meals.Find(id);
            if (meal == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Could not find record with id {0}", id));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, meal);
            }
        }

        // POST api/values
        public HttpResponseMessage Post(Meal mealToAdd)
        {
            try
            {
                mealToAdd.Id = this.getAvailableId();
                dbConnection.Meals.Add(mealToAdd);
                dbConnection.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not add new record");
            } 
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
