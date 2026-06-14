using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxService.Application.Queries;

namespace TaxService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaxController : ControllerBase
{
    private readonly IMediator _mediator;
    public TaxController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetTax(int employeeId)
    {
        var result = await _mediator.Send(new GetEmployeeTaxQuery(employeeId));
        if (result == null) return NotFound(new { message = "Employee not found" });
        return Ok(result);
    }
}