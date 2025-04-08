using GreenOcean.Business.DTOs;
using GreenOcean.Business.Tokens;

namespace GreenOcean.Business.Interfaces;

public interface IValidatingAccountService
{
    public Task<AccountValidationToken?> CheckCode(Guid id, CodeDTO codeDTO);

    public Task<bool> ValidateAccount(Guid id, AccountValidationDTO validateAccountDTO);
}
