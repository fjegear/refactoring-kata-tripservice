using GuardNet;
using System.Collections.Generic;
using TripServiceKata.Exceptions;
using TripServiceKata.Users;

namespace TripServiceKata.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = GetLoggedInUser();
            Guard.NotNull<User, UserNotLoggedInException>(loggedUser);

            return user.IsFriendOf(loggedUser)
                ? GetTripsBy(user)
                : NoTrips();
        }

        private List<Trip> NoTrips()
        {
            return Enumerable.Empty<Trip>().ToList();
        }

        protected virtual List<Trip> GetTripsBy(User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        protected virtual User GetLoggedInUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
