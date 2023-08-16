using Api.BusinessLayer;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
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
    [ProducesResponseType(typeof(ApiResponse<GetDependentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest("Invalid dependent id.");
            }

            var dependent = await _dependentsBusinessLayer.GetDependentById(id);
            if (dependent == null)
            {
                return NotFound("dependent id does not exist.");
            }

            return new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };
        }

        catch (Exception ex)
        {
            //Log the actual error message for troubleshooting. It is not a good idea to show the actual error to the end user. 
            return new ObjectResult(new string("Unable to process the request."))
                { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ApiResponse<List<GetDependentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            return new ApiResponse<List<GetDependentDto>>
            {
                Data = await _dependentsBusinessLayer.GetDependents(),
                Success = true
            };
        }
        catch (Exception e)
        {
            //Log the actual error message for troubleshooting. It is not a good idea to show the actual error to the end user. 
            return new ObjectResult(new string("Unable to process the request."))
                { StatusCode = StatusCodes.Status500InternalServerError };
        }
      
    }

    [SwaggerOperation(Summary = "Get all dependents by employee")]
    [HttpGet("ByEmployee/{employeeId}")]
    [ProducesResponseType(typeof(ApiResponse<List<GetDependentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll(int employeeId)
    {
        try
        {
            if (employeeId == 0)
            {
                return BadRequest("Invalid employee id.");
            }

            var dependents = await _dependentsBusinessLayer.GetDependentsByEmployeeId(employeeId);
          
            return new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Success = true
            };
        }

        catch (Exception ex)
        {
            //Log the actual error message for troubleshooting. It is not a good idea to show the actual error to the end user. 
            return new ObjectResult(new string("Unable to process the request."))
                { StatusCode = StatusCodes.Status500InternalServerError };
        }


       
    }
}
