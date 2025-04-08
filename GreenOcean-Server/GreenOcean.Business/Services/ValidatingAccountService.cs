using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Tokens;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class ValidatingAccountService : IValidatingAccountService
{
    private readonly ICodeRepository _codeRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ISettingPassword _settingPassword;

    public ValidatingAccountService(ICodeRepository codeRepository, IUserRepository userRepository,
        ITokenService tokenService, ISettingPassword settingPassword)
    {
        _codeRepository = codeRepository;
        _userRepository = userRepository;
        _tokenService = tokenService;
        _settingPassword = settingPassword;
    }

    public async Task<AccountValidationToken?> CheckCode(Guid id, CodeDTO codeDTO)
    {
        try
        { 
            var code = await _codeRepository.GetCode(id);
            if (code == null || code.GeneratedCode != codeDTO.Code)
            {
                return null;
            }

            var token = _tokenService.CreateConfirmationCodeToken(id.ToString());
            var accountValidationToken = new AccountValidationToken
            {
                Name = "validate",
                Token = token
            };

            await _codeRepository.DeleteCode(code);       
            return accountValidationToken;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"{ex}");
        }
    }

    public async Task<bool> ValidateAccount(Guid id, AccountValidationDTO validateAccountDTO)
    {
        try
        {
            var password = validateAccountDTO.Password;
            var confirmedPassword = validateAccountDTO.ConfirmedPassword;
            if (!string.Equals(password, confirmedPassword))
            {
                throw new Exception("The password do not match");
            }

            var hash = _settingPassword.EncryptPassword(password, out var salt);
            var username = validateAccountDTO.Username;
            
            var updatedAccount = await _userRepository.UpdateUser(id, username, hash, salt);
            return updatedAccount;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new Exception($"{ex}");
        }
    }

}