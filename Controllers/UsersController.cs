using DataServicesLayer;
using DataServicesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace WebApiInnoCV.Controllers
{
    public class UsersController : ApiController
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _usersService.GetUsers();

        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            User user = _usersService.GetUserById(id);
            if (user == null)
                return Json(new
                {
                    Result = HttpStatusCode.NotFound,
                    Message = "User not found"
                });
            
            return Ok(user);
        }

        [HttpPost]
        public IHttpActionResult AddUser([FromBody]User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usersService.AddUser(user);
                    return Ok(user);
                }
                else
                    return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    Result = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateUser([FromBody]User user)
        {
            User userToUpdate = _usersService.GetUserById(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Birthdate = user.Birthdate;
                _usersService.UpdateUser(userToUpdate);
            }
            else
                _usersService.AddUser(user);
            return Ok(user);
               

        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            User userToDelete = _usersService.GetUserById(id);
            if (userToDelete != null)
            {
                _usersService.DeleteUser(userToDelete);
                return Ok(userToDelete);
            }
            else
                return Json(new
                {
                    Result = HttpStatusCode.NotFound,
                    Message = "User not found"
                });
        }



    }
}
