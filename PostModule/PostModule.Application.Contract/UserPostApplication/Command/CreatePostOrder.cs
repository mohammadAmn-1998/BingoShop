namespace PostModule.Application.Contract.UserPostApplication.Command;

public class CreatePostOrder
{
    public CreatePostOrder(long userId, int packageId,int price)
    {
        UserId = userId;
        PackageId = packageId;
        Price = price;
    }

    public long UserId { get; private set; }
    public int PackageId { get; private set; }
    public int Price { get; private set; }
}