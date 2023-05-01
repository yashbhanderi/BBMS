namespace BBMS.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}