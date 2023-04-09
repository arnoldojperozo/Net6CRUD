using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCRUDApi.Database;
using NetCRUDApi.Models;

namespace NetCRUDApi.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EmployeesController : Controller
{
    private readonly DatabaseContext _dbContext;
    
    public EmployeesController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        var employees = await  _dbContext.Employees.ToListAsync();

        return Ok(employees);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
    {
        var employee = await  _dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
    {
        employee.Id = Guid.NewGuid();

        await _dbContext.Employees.AddAsync(employee);

        await _dbContext.SaveChangesAsync();

        return Ok(employee);
    }
    
    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee employee)
    {
        var employeeData = await  _dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);

        if (employeeData == null)
        {
            return NotFound();
        }

        employeeData.Department = employee.Department;
        employeeData.Email = employee.Email;
        employeeData.Salary = employee.Salary;
        employeeData.Name = employee.Name;
        employeeData.Phone = employee.Phone;

        await _dbContext.SaveChangesAsync();

        return Ok(employee);
    }
    
    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
    {
        var employeeData = await  _dbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);

        if (employeeData == null)
        {
            return NotFound();
        }

        _dbContext.Employees.Remove(employeeData);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}