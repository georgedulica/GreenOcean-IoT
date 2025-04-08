using GreenOcean.Business.DTOs;

namespace GreenOcean.Business.Interfaces;

public interface IProcessService
{
    public Task<ProcessDTO?> GetProcess(Guid id);

    public Task<IEnumerable<ProcessDTO>?> GetProcesses(Guid id);

    public Task<bool> AddProcess(ProcessDTO processDTO);

    public Task<bool> EditProcess(Guid id, ProcessDTO processDTO);

    public Task<bool> DeleteProcess(Guid id);
}