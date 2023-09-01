using System.Security.Claims;
using fullstack.API.Extensions;
using fullstack.API.Helpers;
using Fullstack.Application.Contratos;
using Fullstack.Application.Dtos;
using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fullstack.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IUtil _util;
        private readonly string _destino = "Perfil";

        public AccountController(IAccountService accountService, ITokenService tokenService, IUtil util)
        {
            _util = util;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUsernameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obte usuario. Erro: {ex.Message}");
            }
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName))
                    return BadRequest("Usuário já existe");

                var user = await _accountService.CreateAccountAsync(userDto);

                if (user != null)
                {

                    return Ok(new
                    {
                        userName = user.UserName,
                        PrimeiroNome = user.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result
                    });
                }

                return BadRequest("Usuário não criado, tente novamente mais tarde");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar registar o usuario. Erro: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUsernameAsync(userLogin.Username);
                if (user == null) return Unauthorized("Usuario ou senha está errado");
                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new
                {
                    userName = user.UserName,
                    PrimeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar o login. Erro: {ex.Message}");
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName())
                    return Unauthorized("Token/User Mismatch");
                var user = await _accountService.GetUserByUsernameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Usuario Invalido");
                var userReturn = await _accountService.UpdateAccount(userUpdateDto);
                if (user == null) return NoContent();
                return Ok(new
                {
                    userName = userReturn.UserName,
                    PrimeiroNome = userReturn.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar Usuario. Erro: {ex.Message}");
            }
        }
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var user = await _accountService.GetUserByUsernameAsync(User.GetUserName());
                if (user == null) return NoContent();
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _util.DeleteImage(user.ImagemUrl, _destino);
                    user.ImagemUrl = await _util.SaveImage(file, _destino);
                }
                var userRetorno = await _accountService.UpdateAccount(user);
                return Ok(userRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar upload da foto de perfil do usuario. Erro: {ex.Message}");
            }
        }

    }
}