using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface ICodeRepository
{
    public Task<Code?> GetCode(Guid id);

    public Task<Code> AddCode(Guid userId);

    public Task<bool> DeleteCode(Code code);
}