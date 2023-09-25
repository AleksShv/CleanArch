using MediatR;

namespace CleanArch.UseCases.Internal.Notifications;

internal record ProductUpdatedNotification(Guid ProductId) : INotification;
