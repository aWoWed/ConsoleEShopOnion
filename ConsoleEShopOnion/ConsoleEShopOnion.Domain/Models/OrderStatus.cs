namespace ConsoleEShopOnion.Domain.Models
{
    /// <summary>
    /// Order statuses
    /// </summary>
    public enum OrderStatus
    {
        New,
        CanceledByAdmin,
        CanceledByUser,
        ReceivedPayment,
        Sent,
        Received,
        Done
    }
}
