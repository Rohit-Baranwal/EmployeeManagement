using EmployeeManagement.DAL;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeDAL empDal;
        private readonly ILocationDAL locDal;

        public EmployeeController(IEmployeeDAL _empDal, ILocationDAL _locDal)
        {
            empDal = _empDal;
            locDal = _locDal;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = empDal.GetEmployees();
            return View(employees);
        }

        // CREATE - GET
        public ActionResult Create()
        {
            ViewBag.States = new SelectList(locDal.GetStates(), "StateId", "StateName");
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                empDal.AddEmployee(emp);
                return RedirectToAction("Index");
            }

            ViewBag.States = new SelectList(locDal.GetStates(), "StateId", "StateName");
            return View(emp);
        }

        // EDIT - GET
        public ActionResult Edit(int id)
        {
            var emp = empDal.GetEmployees().Find(e => e.EmployeeId == id);

            ViewBag.States = new SelectList(
                locDal.GetStates(), "StateId", "StateName", emp.StateId);

            ViewBag.Cities = new SelectList(
                locDal.GetCitiesByState(emp.StateId), "CityId", "CityName", emp.CityId);

            return View(emp);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                empDal.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }

            ViewBag.States = new SelectList(locDal.GetStates(), "StateId", "StateName", emp.StateId);
            ViewBag.Cities = new SelectList(locDal.GetCitiesByState(emp.StateId), "CityId", "CityName", emp.CityId);
            return View(emp);
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            empDal.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        // AJAX – Get Cities
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = locDal.GetCitiesByState(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
    }
}