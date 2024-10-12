namespace WebApiMessage_Chat.Services.Blocked;

public class BlockedServices : IBlockedInterface
{
    private readonly IBlockedInterface _blockedInterface;

    public BlockedServices(IBlockedInterface blockedInterface)
    {
        _blockedInterface = blockedInterface;
    }
}