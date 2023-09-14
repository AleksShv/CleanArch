using MediatR;

namespace CleanArch.UseCases.Purchasing.Supplies.CompleteSuply;

public record CompleteSupplyCommand(Guid SupplyId) : IRequest;
