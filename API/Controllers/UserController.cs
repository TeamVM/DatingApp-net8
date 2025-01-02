using API.Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(DataContext context) : ControllerBase
{
    [HttpGet]
    public  async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users =  await context.Users.ToListAsync();

        return Ok(users);   
    }
    [HttpGet("{id:int}")] //api/user/2
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = context.Users.Find(id);

        if(user == null) return NotFound();

        return user;
    }
}

