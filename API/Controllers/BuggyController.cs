using API.Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;

namespace API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret text";
    }
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var thing = context.Users.Find(-1);
        if (thing == null) return NotFound();

        return thing;
    }
    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        //try { 
        var thing = context.Users.Find(-1) ?? throw new Exception("A bad thing has happend");
            return thing;
       // }
        //catch(Exception ex)
        //{
            //return StatusCode(500, "Computer says no"); 
        //}
    }
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }

}

