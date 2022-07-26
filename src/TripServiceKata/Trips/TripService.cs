using GuardNet;
using System.Collections.Generic;
using TripServiceKata.Exceptions;
using TripServiceKata.Users;

namespace TripServiceKata.Trips
{
    public class TripService
    {
        private readonly IUserSession _userSession;
        private readonly ITripDAO _tripDAO;

        public TripService() :
            this(UserSession.GetInstance(), new TripDAO())
        {
        }

        public TripService(IUserSession userSession, ITripDAO tripDAO)
        {
            _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            _tripDAO = tripDAO ?? throw new ArgumentNullException(nameof(tripDAO));
        }

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
            return _tripDAO.GetTripsBy(user);
        }

        protected virtual User GetLoggedInUser()
        {
            return _userSession.GetLoggedUser();
        }
    }
}
