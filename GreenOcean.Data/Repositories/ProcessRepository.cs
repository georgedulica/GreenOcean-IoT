using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class ProcessRepository : IProcessRepository
{
    private readonly DataContext _dataContext;

    public ProcessRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Process?> GetProcess(Guid id)
    {
        try
        {
            var process = await _dataContext.Processes.FirstOrDefaultAsync(p => p.Id == id);
            return process;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<Process>?> GetProcesses(Guid id)
    {
        try
        {
            var processes = await _dataContext.Processes.Where(p => p.GreenhouseId == id).ToListAsync();
            return processes;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddProcess(Process process)
    {
        try
        {
            await _dataContext.Processes.AddAsync(process);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
    public async Task<bool> EditProcess(Process processToEdit, Process process)
    {
        try
        {
            processToEdit.ProcessName = process.ProcessName;
            processToEdit.Description = process.Description;
            processToEdit.Timestamp = process.Timestamp;
            processToEdit.GreenhouseId = process.GreenhouseId;

            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeleteProcess(Process process)
    {
        try
        {
            _dataContext.Processes.Remove(process);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
}