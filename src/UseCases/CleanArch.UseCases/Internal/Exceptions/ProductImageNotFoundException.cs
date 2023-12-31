﻿using CleanArch.UseCases.Common.Exceptions;

namespace CleanArch.UseCases.Internal.Exceptions;

internal class ProductImageNotFoundException : UseCaseException
{
    public ProductImageNotFoundException(Guid imageId)
        : base("Image not found")
    {
        Data["ImageId"] = imageId;
    }
}
