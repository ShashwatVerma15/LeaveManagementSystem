using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using LeaveManagementSystem.Web.Services.LeaveTypes;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)] // the user needs to be authorized to access this controller
    public class LeaveTypesController(ILeaveTypesService _leaveTypesService) : Controller
    {
        private const string NameExistsValidationMessage = "This leave type already exists in database";

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            // var data = Select * from LeaveTypes
            // Or we can do
            var viewdata = await _leaveTypesService.GetAll();
            
            // convert datamodel into a view model
            //var viewData = data.Select(q => new IndexVM
            //{
            //    Id = q.Id,
            //    Name = q.Name,
            //    NumberOfDays = q.NumberOfDays,
            //});

            // or we can use autoMapper
            //var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);

            // return the view model to the view
            return View(viewdata);
            // return View(await _context.LeaveTypes.ToListAsync());
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypesService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            //var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType); // using autoMapper

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            // adding custom state and validation
            //if (leaveTypeCreate.Name.Contains("vacation"))
            //{
            //    ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Name should not contain Vacation");
            //}

            if (await _leaveTypesService.checkIfLeaveTypeNameExists(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _leaveTypesService.Create(leaveTypeCreate);
                //var leaveType = _mapper.Map<LeaveType>(leaveTypeCreate);
                //_context.Add(leaveType);  // adding to DB
                //await _context.SaveChangesAsync();  // save changes
                return RedirectToAction(nameof(Index)); // returning to index
            }
            return View(leaveTypeCreate);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Parameterization - key for preventing SQL Injection attacks
            // Select * from LeaveTypes where ID = @Id

            var leaveType = await _leaveTypesService.Get<LeaveTypeEditVM>(id.Value);

            //var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            //var viewData = _mapper.Map<LeaveTypeEditVM>(leaveType);
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypesService.checkIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypesService.Edit(leaveTypeEdit);
                }
                catch (DbUpdateConcurrencyException) // if multiple users are updating the same ID
                {
                    if (!_leaveTypesService.LeaveTypeExists(leaveTypeEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEdit);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypesService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            //var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
