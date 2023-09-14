using MediatR;

namespace CleanArch.UseCases.Purchasing.Supplies.GetSupplyDetails;

public record GetSupplyDetailsCommand(Guid SupplyId) : IRequest<SupplyDetailsDto?>;
