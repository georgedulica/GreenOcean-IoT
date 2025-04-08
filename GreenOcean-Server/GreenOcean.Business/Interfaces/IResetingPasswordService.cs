using GreenOcean.Business.DTOs;
using GreenOcean.Business.Tokens;

namespace GreenOcean.Business.Interfaces;

public interface IResetingPasswordService
{
    public Task<Guid?> GenerateCode(EmailDTO emailDTO);

    public Task<ResetingPasswordToken?> ConfirmCode(Guid id, CodeDTO codeDTO);

    public Task<bool> ChangePassword(Guid id, PasswordDTO passwordDTO);
}