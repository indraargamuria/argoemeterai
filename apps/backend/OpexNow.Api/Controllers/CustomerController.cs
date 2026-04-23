using Microsoft.AspNetCore.Mvc;
using OpexNow.Api.Data;
using OpexNow.Api.Models;

namespace OpexNow.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _db;

    public CustomersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_db.Customers.ToList());
    }

    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        _db.Customers.Add(customer);
        _db.SaveChanges();

        return Ok(customer);
    }
}