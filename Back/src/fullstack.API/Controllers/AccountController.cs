using System.Security.Claims;
using Fullstack.Application.Contratos;
using Fullstack.Application.Dtos;
using Fullstack.Persistence.Contratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fullstack.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService,ITokenService tokenService){
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]        
        public async Task<IActionResult> GetUser()
        {
            try
            {   var userName = User.FindFirst(ClaimTypes.Name)?.Value; 
                var user = await _accountService.GetUserByUsernameAsync(userName);              
                return Ok(user);
            }
           catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.username)) return BadRequest("Usuário já existe");

                if (await _accountService.CreateAccountAsync(userDto) != null) return Ok("Usuário cadastrado");

                return BadRequest("Usuário não criado, tente novamente mais tarde");
            }
           catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
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
                var result = await _accountService.CheckUserPasswordAsync(user,userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

                return Ok(new{
                    userName = user.UserName,
                    PrimeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
           catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
    }
}