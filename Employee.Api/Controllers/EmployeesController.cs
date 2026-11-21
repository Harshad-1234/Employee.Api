using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee.Api.Model;

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDBContext _context;

        public EmployeesController(EmployeeDBContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeData>>> GetEmployees()
        {
          if (_context.EmployeeData == null)
          {
              return NotFound();
          }
            return await _context.EmployeeData.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeData>> GetEmployee(int id)
        {
          if (_context.EmployeeData == null)
          {
              return NotFound();
          }
            var employee = await _context.EmployeeData.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeData employee)
        {
            if (id != employee.employeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeData>> PostEmployee(EmployeeData employee)
        {
          if (_context.EmployeeData == null)
          {
              return Problem("Entity set 'EmployeeDBContext.Employees'  is null.");
          }
            _context.EmployeeData.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.employeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.EmployeeData == null)
            {
                return NotFound();
            }
            var employee = await _context.EmployeeData.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeData.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.EmployeeData?.Any(e => e.employeeId == id)).GetValueOrDefault();
        }
    }
}
