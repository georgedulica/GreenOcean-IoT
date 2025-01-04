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
            var existingProcess = await CheckProcess(process);
            if (existingProcess == true)
            {
                return false;
            }

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
            var existingProcess = await CheckProcess(process);
            if (existingProcess == true)
            {
                return false;
            }

            processToEdit.ProcessName = process.ProcessName;
            processToEdit.Description = process.Description;
            processToEdit.StartDate = process.StartDate;
            processToEdit.DueDate = process.DueDate;
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

    private async Task<bool> CheckProcess(Process process)
    {
        var existingProcess = await _dataContext.Processes.AnyAsync(p => string.Equals(p.ProcessName, process.ProcessName) &&
            string.Equals(p.Description, process.Description) && p.GreenhouseId == process.GreenhouseId && p.StartDate == process.StartDate && process.DueDate == process.DueDate);
        return existingProcess;
    }
}