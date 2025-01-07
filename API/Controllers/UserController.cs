using API.Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UserController(DataContext context) : BaseApiController
{
    [AllowAnonymous]
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

