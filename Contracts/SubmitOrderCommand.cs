namespace Contracts
{
    public record SubmitOrderCommand(Guid OrderId, List<LineItem> Items);
}
