using TripServiceKata.Users;

namespace TripServiceKata.Trips
{
    public interface ITripDAO
    {
        List<Trip> GetTripsBy(User user);
    }
}