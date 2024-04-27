using GreenOcean.Business.DTOs;
using GreenOcean.Business.Tokens;

namespace GreenOcean.Business.Interfaces;

public interface IResetingPasswordService
{
    public Task<ResetingPasswordToken?> GenerateCode(EmailDTO emailDTO);

    public Task<bool> ConfirmCode(Guid id, CodeDTO codeDTO);

    public Task<bool> ChangePassword(Guid id, PasswordDTO passwordDTO);
}