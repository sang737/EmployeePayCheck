
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Employee endpoints
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{

    private readonly IEmployeeBusinessLayer _employeeBusinessLayer;

    public EmployeesController(IEmployeeBusinessLayer employeeBusinessLayer)
    {
        _employeeBusinessLayer = employeeBusinessLayer;
    }


    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetEmployeeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest("Invalid employee id.");
            }

            var employee = await _employeeBusinessLayer.GetEmployeeById(id);
            if (employee == null)
            {

                return NotFound("Employee id does not exist.");
            }

            return new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
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

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ApiResponse<List<GetEmployeeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            return new ApiResponse<List<GetEmployeeDto>>
            {
                Data = await _employeeBusinessLayer.GetEmployees(),
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

    [SwaggerOperation(Summary = "Get Paycheck by employeeId")]
    [HttpGet("Paycheck/{employeeId}")]
    [ProducesResponseType(typeof(ApiResponse<GetEmployeePaycheckDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<GetEmployeePaycheckDto>>> GetPayCheck(int employeeId) 
    {
        try
        {
            if (employeeId == 0)
            {
                return BadRequest("Invalid employee id.");
            }


            var employeePayCheck = await _employeeBusinessLayer.GetPayCheckByEmployeeId(employeeId);
            if (employeePayCheck == null)
            {

                return NotFound("Employee id does not exist.");
            }

            return new ApiResponse<GetEmployeePaycheckDto>
            {
                Data = employeePayCheck,
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
