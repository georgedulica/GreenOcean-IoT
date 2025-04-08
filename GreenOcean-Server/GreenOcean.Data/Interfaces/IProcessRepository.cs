using GreenOcean.Data.Entities;

namespace GreenOcean.Data.Interfaces;

public interface IProcessRepository
{
    public Task<Process?> GetProcess(Guid id);

    public Task<IEnumerable<Process>?> GetProcesses(Guid id);

    public Task<bool> AddProcess(Process process);

    public Task<bool> EditProcess(Process processToEdit, Process process);

    public Task<bool> DeleteProcess(Process process);
}