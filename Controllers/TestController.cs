using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prof_edna_teles_shop_api.Data;
using prof_edna_teles_shop_api.Models;

namespace prof_edna_teles_shop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    AppDbContext _db;

    public TestController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public IActionResult GetTest()
    {
        return Ok("A API funciona.");
    }

    [HttpGet("All")]
    public async Task<IActionResult> StartTestAsync()
    {
        Product? product = await _db.Products.FirstOrDefaultAsync(p => p.GameType == null);

        if (product != null)
        {
            Product p = product;
            p.GameType = "Dominó";

            _db.Entry(product).CurrentValues.SetValues(p);
        }
        await _db.SaveChangesAsync();

        return Ok("A API funciona.");
    }

}
