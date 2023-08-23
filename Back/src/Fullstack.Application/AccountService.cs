
using Fullstack.Application.Dtos;
using Fullstack.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Fullstack.Persistence.Contratos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Application
{
    public class AccountService : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;
        private IMapper mapper;

        
        public AccountService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper,
        IUserPersist userPersist)
        {
            _userPersist = userPersist;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());
                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.password);
                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDto>(user);
                    return userToReturn;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar criar Usuario. Erro {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _userPersist.GetUserByUsernameAsync(username);
                if (user == null) return null;
                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar Pegar Usuario por Username Erro {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.GetUserByUsernameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                _mapper.Map(userUpdateDto, user);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _userPersist.Update<User>(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.GetUserByUsernameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }
                return null;


            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário Erro {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Erro ao verificar se o usúario ja existe. Erro {ex.Message}");
            }
        }
    }
}