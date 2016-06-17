using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkWithMVC;

namespace EntityFrameworkWithMVC.Controllers
{
    public class EmployeeInformationsController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: EmployeeInformations
        public ActionResult Index()
        {
            var employeeInformations = db.EmployeeInformations.Include(e => e.DepartmentInformation);
            return View(employeeInformations.ToList());
        }

        // GET: EmployeeInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInformation employeeInformation = db.EmployeeInformations.Find(id);
            if (employeeInformation == null)
            {
                return HttpNotFound();
            }
            return View(employeeInformation);
        }

        // GET: EmployeeInformations/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.DepartmentInformations, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: EmployeeInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sno,Name,Address,DepartmentID")] EmployeeInformation employeeInformation)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeInformations.Add(employeeInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.DepartmentInformations, "DepartmentID", "DepartmentName", employeeInformation.DepartmentID);
            return View(employeeInformation);
        }

        // GET: EmployeeInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInformation employeeInformation = db.EmployeeInformations.Find(id);
            if (employeeInformation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentInformations, "DepartmentID", "DepartmentName", employeeInformation.DepartmentID);
            return View(employeeInformation);
        }

        // POST: EmployeeInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sno,Name,Address,DepartmentID")] EmployeeInformation employeeInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentInformations, "DepartmentID", "DepartmentName", employeeInformation.DepartmentID);
            return View(employeeInformation);
        }

        // GET: EmployeeInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInformation employeeInformation = db.EmployeeInformations.Find(id);
            if (employeeInformation == null)
            {
                return HttpNotFound();
            }
            return View(employeeInformation);
        }

        // POST: EmployeeInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeInformation employeeInformation = db.EmployeeInformations.Find(id);
            db.EmployeeInformations.Remove(employeeInformation);
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
