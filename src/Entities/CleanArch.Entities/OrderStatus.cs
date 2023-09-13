namespace CleanArch.Entities;

public enum OrderStatus
{
    Checkout = 0, 
    Accepted = 1,
    InProgress = 2,
    Sent = 4,
    Delivered = 5,
    Canceled = 6,
}
