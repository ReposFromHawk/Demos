using CareHomeTaskManager.Core;
using CareHomeTaskManager.Core.DataInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CareHomeTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareHomeTaskController : ControllerBase
    {
        private ICareTaskManager _careTaskManager;
        private IUserManager _userManager;
        public CareHomeTaskController(ICareTaskManager careTaskManager,IUserManager userManager)
        {
            _careTaskManager = careTaskManager;
            _userManager = userManager;
        }

        // GET: api/<CareHomeTaskController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var res = _careTaskManager.GetAllTasks();
            if (res != null && res.Any())
            {
                return Ok();
            }
            else {

                return NotFound();
            }
            
        }

        // GET api/<CareHomeTaskController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>  Get(int id)
        {
            var res = _careTaskManager.GetWithId(id);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest();
            }
            
        }

        // POST api/<CareHomeTaskController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CareTask careTask)
        {
            try
            {
                
                _careTaskManager.Save(careTask);
                return CreatedAtAction(nameof(Get), new { id = careTask.Id },careTask);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]User user)
        {
           
            var dbUser = _userManager.GetUser(user.Email);
            if (dbUser != null)
            {
                if (dbUser.Password == user.Password)
                {
                    var claims = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Email",user.Email),
                        new KeyValuePair<string, string>("Role","User")
                    };

                    dbUser.JwtToken = AuthenticationConfig.GenerateJWT(claims,TokenSettings.ClockSkew,
                        TokenSettings.ValidIssuer,TokenSettings.ValidAudience);
                    _userManager.SaveUser(dbUser);
                    return Ok(dbUser);
                }
                else
                {
                    return BadRequest("Password Doesn't Match");
                }
            }
            else
            {
                return NotFound("User not found!");
            }
        }
        [Route("api/newUser")]
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var dbUser = _userManager.GetUser(user.Email);
            if (dbUser == null)
            {
                _userManager.SaveUser(user);
                return Ok($"New User with {user.Email} is created!");
            }
            else
            {
                return BadRequest("User Already exist");
            }
        }
    }
}
