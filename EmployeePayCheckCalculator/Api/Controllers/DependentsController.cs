using Api.BusinessLayer;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Helper.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Dependent endpoints
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{

    private readonly IDependentsBusinessLayer _dependentsBusinessLayer;

    public DependentsController(IDependentsBusinessLayer dependentsBusinessLayer)
    {
        _dependentsBusinessLayer = dependentsBusinessLayer;
    }


    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ServiceResponse<GetDependentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            if (id == 0)
            {
                return CustomServiceResponse_V2.Failure("Invalid dependent id.", StatusCodes.Status400BadRequest);
            }

            var dependent = await _dependentsBusinessLayer.GetDependentById(id);
            if (dependent == null)
            {
                return CustomServiceResponse_V2.Failure("dependent id does not exist..", StatusCodes.Status404NotFound);
            }

            return CustomServiceResponse_V2.Success<GetDependentDto>(dependent);
        }

        catch (Exception ex)
        {
            return CustomServiceResponse_V2.Failure(ex);
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ServiceResponse<List<GetDependentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _dependentsBusinessLayer.GetDependents();
            return CustomServiceResponse_V2.Success<List<GetDependentDto?>>(result);
        }
        catch (Exception ex)
        {
            return CustomServiceResponse_V2.Failure(ex);
        }
      
    }

    [SwaggerOperation(Summary = "Get all dependents by employee")]
    [HttpGet("ByEmployee/{employeeId}")]
    [ProducesResponseType(typeof(ServiceResponse<List<GetDependentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(int employeeId)
    {
        try
        {
            if (employeeId == 0)
            {
                return CustomServiceResponse_V2.Failure("Invalid employee id.", StatusCodes.Status400BadRequest);
            }

            var dependents = await _dependentsBusinessLayer.GetDependentsByEmployeeId(employeeId);

            return CustomServiceResponse_V2.Success<List<GetDependentDto?>>(dependents);
        }

        catch (Exception ex)
        {
            return CustomServiceResponse_V2.Failure(ex);
        }


       
    }
}
