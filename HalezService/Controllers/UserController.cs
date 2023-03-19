using HalezService.Controllers.Base;
using HalezService.Model.Dtos.User;
using HalezService.Model.Enums;
using HalezService.Model.Shared;
using HalezService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HalezService.Controllers
{
    public class UserController : BaseController
    {

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result<Tuple<LoginResult, string>>> Login([FromBody] LoginInput loginInput)
        {
            try
            {
                UserManager userManager = new();

                LoginResult loginResult = userManager.LoginControl(username: loginInput.Username, password: loginInput.Password);
                if (loginResult != null)
                {
                    Shared shared = new Shared();
                    string token = shared.GenerateToken(loginResult: loginResult);
                    return Ok(new Result<Tuple<LoginResult, string>>()
                    {
                        Success = true,
                        Data = new Tuple<LoginResult, string>(
                        loginResult,
                        token)
                    });
                }

                return NotFound(new Result<Tuple<LoginResult, string>>() { Success = false, Message = "Kullanıcı Adı Şifre Yanlış Lütfen Tekrar Deneyiniz." });
            }
            catch (Exception exception)
            {
                return BadRequest(new Result<Tuple<LoginResult, string>>() { Success = false, Message = MyExceptionHandler.GetAllExceptionMessages(exception: exception) });
            }
        }

        [HttpGet("GetUserRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result<UserTypes>> GetUserRole(string token)
        {
            try
            {
                Shared shared = new Shared();
                UserTypes customerType = shared.GetUserMode(token);


                return Ok(new Result<UserTypes>() { Data = customerType, Success = true });
            }
            catch (Exception exception)
            {
                return BadRequest(new Result<UserTypes>() { Success = false, Message = MyExceptionHandler.GetAllExceptionMessages(exception: exception) });
            }
        }

        [HttpGet("GetList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ResultList<UserResult>> GetList()
        {
            try
            {
                UserManager userManager = new();
                List<UserResult> userResults = userManager.GetList();

                return Ok(new ResultList<UserResult>()
                {
                    Success = true,
                    Data = userResults,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultList<UserResult>() { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result> Add([FromBody] AddUserInput addUserInput)
        {
            try
            {
                Shared shared = new();
                UserManager userManager = new();
                int userId = shared.GetUserId(token: HttpContext.Request.Headers["Authorization"]);
                userManager.Insert(addUserInput: addUserInput, userId: userId);

                return Ok(new Result()
                {
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result> Delete(int id)
        {
            try
            {
                Shared shared = new();
                int userId = shared.GetUserId(token: HttpContext.Request.Headers["Authorization"]);
                UserManager userManager = new();
                userManager.Delete(id: id, userId: userId);

                return Ok(new Result<UserFullResult>()
                {
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result<UserFullResult>> Get(int id)
        {
            try
            {
                UserManager userManager = new();
                UserFullResult userFullResult = userManager.Get(id: id);
                return Ok(new Result<UserFullResult>()
                {
                    Data = userFullResult,
                    Success = true
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new ResultList<UserResult>()
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Result> Update([FromBody] UpdateUserInput updateUserInput, int userId, int updateUserId)
        {
            try
            {
                UserManager userManager = new();
                Shared shared = new();

                userManager.Update(updateUserInput: updateUserInput, userId: userId, updateUserId: updateUserId);

                return Ok(new Result()
                {
                    Success = true
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new Result()
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    

}
}
