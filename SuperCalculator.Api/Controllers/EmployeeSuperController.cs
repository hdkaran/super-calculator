using Microsoft.AspNetCore.Mvc;
using SuperCalculator.Api.Models.Request;
using SuperCalculator.Api.Models.Response;
using SuperCalculator.Api.Services;
using SuperCalculator.Application.Services;

namespace SuperCalculator.Api.Controllers;

[Route("/api/employee-super/")]
[ApiController]
public class EmployeeSuperController: ControllerBase
{
    private readonly IRequestConverterService _requestConverterService;
    private readonly IEmployeeSuperService _employeeSuperService;

    public EmployeeSuperController(IRequestConverterService requestConverterService, IEmployeeSuperService employeeSuperService)
    {
        _requestConverterService = requestConverterService;
        _employeeSuperService = employeeSuperService;
    }
    
    [HttpPost("process-quarterly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<EmployeeSuperQuarterlyVarianceResponse> GetQuarterlyVariances([FromForm] EmployeeSuperQuarterlyVarianceRequest request)
    {
        var convertedRequest = await _requestConverterService.GetRawDataProcessingRequest(request);

        var response = new EmployeeSuperQuarterlyVarianceResponse()
        {
            Summary = await _employeeSuperService.ProcessEmployeeSuperQuarterlyVariances(convertedRequest)
        };
        
        return response;
    }
}