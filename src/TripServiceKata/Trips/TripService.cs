using System.Collections.Generic;
using TripServiceKata.Exceptions;
using TripServiceKata.Users;

namespace TripServiceKata.Trips
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User user)
        {
            User loggedUser = GetLoggedUser();
            if (loggedUser != null)
            {
                return user.IsFriendOf(loggedUser)
                    ? GetTripsBy(user)
                    : NoTrips();
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }

        private List<Trip> NoTrips()
        {
            return Enumerable.Empty<Trip>().ToList();
        }

        protected virtual List<Trip> GetTripsBy(User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        protected virtual User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
