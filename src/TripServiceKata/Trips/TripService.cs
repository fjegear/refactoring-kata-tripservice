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
            User loggedUser = _userSession.GetLoggedUser();
            Guard.NotNull<User, UserNotLoggedInException>(loggedUser);

            return user.IsFriendOf(loggedUser)
                ? _tripDAO.GetTripsBy(user)
                : NoTrips();
        }

        private List<Trip> NoTrips()
        {
            return Enumerable.Empty<Trip>().ToList();
        }
    }
}
