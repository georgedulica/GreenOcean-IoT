using AutoMapper;
using GreenOcean.Business.DTOs;
using GreenOcean.Business.Interfaces;
using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;

namespace GreenOcean.Business.Services;

public class ProcessService : IProcessService
{
    private readonly IProcessRepository _processRepository;
    private readonly IMapper _mapper;

    public ProcessService(IProcessRepository processRepository, IMapper mapper)
    {
        _processRepository = processRepository;
        _mapper = mapper;
    }

    public async Task<ProcessDTO?> GetProcess(Guid id)
    {
        try
        {
            var process = await _processRepository.GetProcess(id);
            if (process == null)
            {
                return null;
            }

            var processDTO = _mapper.Map<Process, ProcessDTO>(process);
            return processDTO;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<IEnumerable<ProcessDTO>?> GetProcesses(Guid id)
    {
        try
        {
            var processes = await _processRepository.GetProcesses(id);
            if (processes == null)
            {
                return null;
            }

            var processDTOs = _mapper.Map<IEnumerable<Process>, IEnumerable<ProcessDTO>>(processes);
            return processDTOs;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> AddProcess(ProcessDTO processDTO)
    {
        try
        {
            var process = _mapper.Map<ProcessDTO, Process>(processDTO);
            var response = await _processRepository.AddProcess(process);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> EditProcess(Guid id, ProcessDTO processDTO)
    {
        try
        {
            var processToEdit = await _processRepository.GetProcess(id);
            if (processToEdit == null)
            {
                return false;
            }

            var process = _mapper.Map<ProcessDTO, Process>(processDTO);

            var response = await _processRepository.EditProcess(processToEdit, process);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }

    public async Task<bool> DeleteProcess(Guid id)
    {
        try
        {
            var process = await _processRepository.GetProcess(id);
            if (process == null)
            {
                return false;
            }

            var response = await _processRepository.DeleteProcess(process);
            return response;
        }
        catch (Exception exception)
        {
            var message = exception.Message;
            Console.WriteLine(message);
            throw new Exception(message);
        }
    }
}