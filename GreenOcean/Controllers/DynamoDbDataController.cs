using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using GreenOcean.DTOs;
using AutoMapper;
using DynamoDBData = GreenOcean.Data.Data;

namespace GreenOcean.Controllers;

[ApiController]
[Route("dynamoDbData")]
public class DynamoDbDataController : ControllerBase
{
    private readonly IDynamoDBContext dynamoDBContext;
    private readonly IMapper mapper;

    public DynamoDbDataController(IDynamoDBContext dynamoDBContext, IMapper mapper)
    {
        this.dynamoDBContext = dynamoDBContext;
        this.mapper = mapper;
    }

    [HttpGet("getData")]
    public async Task<IEnumerable<DataDTO>> GetAsync(string id)
    {
        
        var data = await dynamoDBContext.QueryAsync<Data.Data>(id).GetRemainingAsync();
        var dataDTO = mapper.Map<IEnumerable<DynamoDBData>, IEnumerable<DataDTO>>(data);

        return dataDTO;
    }
}
