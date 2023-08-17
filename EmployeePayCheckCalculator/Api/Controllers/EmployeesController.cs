
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Helper.Response;

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
    [ProducesResponseType(typeof(ServiceResponse<GetEmployeeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Getv2(int id)
    {
        try
        {
            if (id == 0)
            {
                return CustomServiceResponse_V2.Failure("Invalid employee id.",StatusCodes.Status400BadRequest);
            }

            var employee = await _employeeBusinessLayer.GetEmployeeById(id);
            if (employee == null)
            {
                return CustomServiceResponse_V2.Failure("Employee id does not exist.", StatusCodes.Status404NotFound);
            }

            return CustomServiceResponse_V2.Success<GetEmployeeDto>(employee);

        }

        catch (Exception ex)
        { 
            return CustomServiceResponse_V2.Failure(ex);
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ServiceResponse<List<GetEmployeeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _employeeBusinessLayer.GetEmployees();
            return CustomServiceResponse_V2.Success<List<GetEmployeeDto?>>(result);

        }
        catch (Exception ex)
        {
            return CustomServiceResponse_V2.Failure(ex);
        }

    }

    [SwaggerOperation(Summary = "Get Paycheck by employeeId")]
    [HttpGet("Paycheck/{employeeId}")]
    [ProducesResponseType(typeof(ServiceResponse<GetEmployeePaycheckDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPayCheck(int employeeId)
    {
        try
        {
            if (employeeId == 0)
            {
                return CustomServiceResponse_V2.Failure("Invalid employee id.", StatusCodes.Status400BadRequest);
            }
            var employeePayCheck = await _employeeBusinessLayer.GetPayCheckByEmployeeId(employeeId);
            if (employeePayCheck == null)
            {

                return CustomServiceResponse_V2.Failure("Employee id does not exist.", StatusCodes.Status404NotFound);
            }

            return CustomServiceResponse_V2.Success<GetEmployeePaycheckDto>(employeePayCheck);
        }

        catch (Exception ex)
        {
            return CustomServiceResponse_V2.Failure(ex);
        }
    }
}
