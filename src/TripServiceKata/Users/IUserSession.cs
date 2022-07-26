namespace TripServiceKata.Users
{
    public interface IUserSession
    {
        User GetLoggedUser();
        bool IsUserLoggedIn(User user);
    }
}