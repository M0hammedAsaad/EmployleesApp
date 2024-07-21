using EmployleesApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployleesApp.Controllers
{
    public class EmployeeController : Controller
    {
        HRDatabaseContext dbContext = new HRDatabaseContext();
        public IActionResult Index()
        {
            //List<Employee> employee = dbContext.Employees.ToList();
            var employees = (from emp in dbContext.Employees
                             join dep in dbContext.Departments on emp.Departmentid
                             equals dep.DepartmentId
                             select new Employee
                             {
                                 EmployeeID = emp.EmployeeID,
                                 EmployeeName = emp.EmployeeName,
                                 DOB = emp.DOB,
                                 EmployeeNumber = emp.EmployeeNumber,
                                 HiringDate = emp.HiringDate,
                                 GrossSalary = emp.GrossSalary,
                                 NetSalary = emp.NetSalary,
                                 Departmentid = emp.Departmentid,
                                 DepartmentName = dep.DepartmentName,

                             }

                             );

            return View(employees);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Add(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create", data);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbContext.Employees.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbContext.Departments.ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int ID)
        {
            Employee data = this.dbContext.Employees.Where(e => e.EmployeeID == ID).FirstOrDefault();
            if (data != null)
            {
                dbContext.Employees.Remove(data);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
