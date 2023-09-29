using CleanArch.Entities;
using MediatR;

namespace CleanArch.UseCases.Catalog.Products.UpdateProduct;

public record ProductUpdatedEvent(Product Product) : INotification;
