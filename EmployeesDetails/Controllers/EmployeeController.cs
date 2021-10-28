using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeesDetails;
using Rotativa;

namespace EmployeesDetails.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeesEntities db = new EmployeesEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.EmployeeDetails.ToList());
        }

        public ActionResult Index1()
        {
            var employeeList = db.EmployeeDetails.ToList();
            return View(employeeList);
        }

        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {

            EmployeeDetail employee = db.EmployeeDetails.FirstOrDefault(c => c.ID == id);

                var report = new PartialViewAsPdf("~/Views/Employee/Details.cshtml", employee);
                return report;

        }


        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetail);
        }

        // GET: Employee/Create
        //[ActionName("EnterDetails")]
        public ActionResult EnterEmployeeDetails()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterEmployeeDetails([Bind(Include = "ID,Name,Mobile,Email,TechnicalSkills,ProjectDetails,RelevantExperience")] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDetails.Add(employeeDetail);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.SuccessMessage = "Data saved successfully !";
            }

            return View(employeeDetail);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetail);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Mobile,Email,TechnicalSkills,ProjectDetails,RelevantExperience")] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeDetail);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            if (employeeDetail == null)
            {
                return HttpNotFound();
            }
            return View(employeeDetail);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeDetail employeeDetail = db.EmployeeDetails.Find(id);
            db.EmployeeDetails.Remove(employeeDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
