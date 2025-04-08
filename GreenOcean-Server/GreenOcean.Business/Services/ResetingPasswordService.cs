using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Settings;
using GreenOcean.Business.Tokens;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace GreenOcean.Business.Services;

public class ResetingPasswordService : IResetingPasswordService
{
    private readonly IUserRepository _userRepository;
    private readonly ICodeRepository _codeRepository;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly ISettingPassword _settingPassword;
    private readonly IOptions<EmailPathSettings> _emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> _emailSubjectSettings;

    public ResetingPasswordService(IUserRepository userRepository, ICodeRepository codeRepository,
        IEmailService emailService, ITokenService tokenService, ISettingPassword settingPassword,
        IOptions<EmailPathSettings> emailPathSettings, IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        _userRepository = userRepository;
        _codeRepository = codeRepository;
        _emailService = emailService;
        _tokenService = tokenService;
        _settingPassword = settingPassword;
        _emailSubjectSettings = emailSubjectSettings;
        _emailPathSettings = emailPathSettings;
    }

    public async Task<Guid?> GenerateCode(EmailDTO emailDTO)
    {
        try
        {
            var email = emailDTO.Email;
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            var userId = user.Id;
            var code = await _codeRepository.AddCode(userId);
            var generatedCode = code.GeneratedCode.ToString();

            string emailBody = CreateEmail(user, generatedCode);

            var sentEmail = _emailService.SendEmail(user.Email, emailBody, _emailSubjectSettings.Value.PasswordResetEmailSubject);
            if (sentEmail == false)
            {
                await _codeRepository.DeleteCode(code);
                throw new Exception("The email cannot be sent");
            }

            return userId;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResetingPasswordToken?> ConfirmCode(Guid id, CodeDTO codeDTO)
    {
        try
        {
            var code = await _codeRepository.GetCode(id);
            if (code == null)
            {
                return null;
            }

            var generatedCode = code.GeneratedCode;
            if (generatedCode != codeDTO.Code)
            {
                return null;
            }

            await _codeRepository.DeleteCode(code);

            var token = _tokenService.CreateConfirmationCodeToken(id.ToString());
            var resetingPasswordToken = new ResetingPasswordToken
            {
                Id = id.ToString(),
                Token = token
            };

            return resetingPasswordToken;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ChangePassword(Guid id, PasswordDTO passwordDTO)
    {
        try 
        { 
            var password = passwordDTO.Password;
            var confirmedPassword = passwordDTO.ConfirmedPassword;
            if (!string.Equals(password, confirmedPassword))
            {
                throw new Exception("Passwords do not match");
            }

            var hash = _settingPassword.EncryptPassword(password, out var salt);

            var resetedPassword = await _userRepository.UpdateUser(id, hash, salt);
            return resetedPassword;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
    }

    private string CreateEmail(User user, string generatedCode)
    {
        var emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.PasswordResetEmailPath);
        string emailBody = emailTemplate.Replace("{name}", user.FirstName)
                                .Replace("{code}", generatedCode)
                                .Replace("{id}", user.Id.ToString());

        return emailBody;
    }
}