using MediatR;
using TaxService.Application.Dtos;

namespace TaxService.Application.Queries;

public record GetEmployeeTaxQuery(int EmployeeId) : IRequest<TaxResultDto?>;