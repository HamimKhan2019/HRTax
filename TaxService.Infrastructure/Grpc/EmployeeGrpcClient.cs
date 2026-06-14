using EmployeeService.Api.Protos;
using Grpc.Net.Client;
using TaxService.Application.Interfaces;

namespace TaxService.Infrastructure.Grpc;

public class EmployeeGrpcClient : IEmployeeGrpcClient
{
    private readonly EmployeeGrpc.EmployeeGrpcClient _client;

    public EmployeeGrpcClient()
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5001");
        _client = new EmployeeGrpc.EmployeeGrpcClient(channel);
    }

    public async Task<EmployeeInfo> GetEmployeeAsync(int id)
    {
        var reply = await _client.GetEmployeeAsync(new EmployeeRequest { Id = id });
        return new EmployeeInfo(reply.Found, reply.Id, reply.Name, reply.Designation, reply.Department,
            (decimal)reply.BasicSalary, (decimal)reply.HouseRent, (decimal)reply.Medical, (decimal)reply.Conveyance, (decimal)reply.PfPercent);
    }
}