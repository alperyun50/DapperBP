using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperBP.Data;
using DapperBP.Models;
using DapperBP.Repository;

namespace DapperBP.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly ICompanyRepository _compRepo;

        [BindProperty]
        public Employee employee { get; set; }

        public EmployeesController(IEmployeeRepository empRepo, ICompanyRepository compRepo)
        {
            _empRepo = empRepo;
            _compRepo = compRepo;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return _empRepo != null ?
                    View(_empRepo.GetAll()) :
                    Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
                _empRepo.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _empRepo == null)
            {
                return NotFound();
            }

            employee = _empRepo.Find(id.GetValueOrDefault());
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _empRepo.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _empRepo == null)
            {
                return NotFound();
            }

            _empRepo.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }

    }
}
