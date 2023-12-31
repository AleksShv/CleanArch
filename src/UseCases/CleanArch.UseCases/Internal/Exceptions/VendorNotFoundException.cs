﻿using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Internal.Exceptions;

internal class VendorNotFoundException : UseCaseException
{
    public VendorNotFoundException(Guid vendorId)
        : base("Vendor not found")
    {
        Data["VendorId"] = vendorId;
    }
}
