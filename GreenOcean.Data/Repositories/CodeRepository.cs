﻿using GreenOcean.Data.Interfaces;
using GreenOcean.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class CodeRepository : ICodeRepository
{
    private readonly DataContext _dataContext;

    public CodeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Code?> GetCode(Guid id)
    {
        try
        {
            var code = await _dataContext.Codes.FirstOrDefaultAsync(c => c.UserId == id);
            return code;
        }
        catch (Exception ex)
        {
            throw new Exception($"The code cannot be returned {ex}");
        }
    }

    public async Task<bool> AddCode(Code code)
    {
        try
        {
            await _dataContext.Codes.AddAsync(code);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The code cannot be saved {ex}");
        }
    }

    public async Task<bool> DeleteCode(Code code)
    {
        try
        {
            _dataContext.Codes.Remove(code);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"The code cannot be deleted {ex}");
        }
    }
}