using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Purchasing.Exceptions;

internal class SupplyNotFoundException : UseCaseException
{
    public SupplyNotFoundException(Guid supplyId)
        : base("Supply not found")
    {
        Data["SupplyId"] = supplyId;
    }
}
