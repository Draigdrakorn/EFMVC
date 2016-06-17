using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkWithMVC.Controllers
{
    public class TestController : Controller
    {
        EmployeeContext eContext = new EmployeeContext();
        public ActionResult Index()
        {
            return View(eContext.EmployeeInformations.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(eContext.DepartmentInformations, "DepartmentID", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeInformation employee)
        {
                eContext.employeeAdd(employee.Name, employee.Address, employee.DepartmentID);
                eContext.SaveChanges();
                return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EmployeeInformation empInfo = eContext.EmployeeInformations.Find(Id);
            ViewBag.DepartmentID = new SelectList(eContext.DepartmentInformations, "DepartmentID", "DepartmentName", empInfo.DepartmentID);
            return View(empInfo);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeInformation empInfo)
        {
            eContext.employeeUpdate(empInfo.Sno, empInfo.Name, empInfo.Address, empInfo.DepartmentID);
            eContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            EmployeeInformation empInfo = eContext.EmployeeInformations.Find(Id);
            return View(empInfo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmation(int Id)
        {
            eContext.employeeDelete(Id);
            eContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}