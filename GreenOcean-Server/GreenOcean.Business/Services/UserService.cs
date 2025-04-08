using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Business.Settings;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace GreenOcean.Business.Services;

public class UserService : ICreatingUserService
{
    private readonly IUserRepository _userRepostory;
    private readonly ICodeRepository _codeRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IOptions<EmailPathSettings> _emailPathSettings;
    private readonly IOptions<EmailSubjectSettings> _emailSubjectSettings;

    public UserService(IUserRepository userRepository, ICodeRepository codeRepository, IMapper mapper,
        IEmailService emailService, IOptions<EmailPathSettings> emailPathSettings, IOptions<EmailSubjectSettings> emailSubjectSettings)
    {
        _userRepostory = userRepository;
        _codeRepository = codeRepository;
        _mapper = mapper;
        _emailService = emailService;
        _emailPathSettings = emailPathSettings;
        _emailSubjectSettings = emailSubjectSettings;
    }

    public async Task<bool> CreateUser(UserDTO userDTO)
    {
        try
        {
            var (user, savedUserResponse) = await SaveUser(userDTO);
            if (savedUserResponse == false)
            {
                return savedUserResponse;
            }

            var userId = user.Id;
            var code = await _codeRepository.AddCode(userId);
            var generatedCode = code.GeneratedCode.ToString();
     
            var emailBody = CreateEmail(user, generatedCode);

            var sentEmail = _emailService.SendEmail(user.Email, emailBody, _emailSubjectSettings.Value.RegistrationEmailSubject);
            if (sentEmail == false)
            {
                await DeleteResources(code, user);
                return sentEmail;
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new ArgumentException(ex.ToString());
        }
    }

    private string CreateEmail(User user, string generatedCode)
    {
        var emailTemplate = System.IO.File.ReadAllText(_emailPathSettings.Value.RegistrationEmailPath);
        string emailBody = emailTemplate.Replace("{name}", user.FirstName)
                                .Replace("{code}", generatedCode)
                                .Replace("{id}", user.Id.ToString());

        return emailBody;
    }

    private async Task<(User? user, bool response)> SaveUser(UserDTO userDTO)
    {
        var user = _mapper.Map<UserDTO, User>(userDTO);

        var randomUsername = BitConverter.ToString(GetRandomBlob()).Replace("-", "");
        var randomPassword = GetRandomBlob();
        var randomSalt = GetRandomBlob();

        user.Username = randomUsername;
        user.Password = randomPassword;
        user.Salt = randomSalt;

        var response = await _userRepostory.AddUser(user);
        return (user, response);
    }

    private byte[] GetRandomBlob()
    {
        var buffer = new byte[128];
        new Random().NextBytes(buffer);

        return buffer;
    }

    private async Task DeleteResources(Code code, User user)
    {
        await _userRepostory.DeleteUser(user);
        await _codeRepository.DeleteCode(code);
    }
}