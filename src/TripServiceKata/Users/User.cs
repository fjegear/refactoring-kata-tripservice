using System.Collections.Generic;
using TripServiceKata.Trips;

namespace TripServiceKata.Users
{
    public class User
    {
        private List<Trip> trips = new List<Trip>();
        private List<User> friends = new List<User>();

        public List<User> GetFriends()
        {
            return friends;
        }

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public void AddTrip(Trip trip)
        {
            trips.Add(trip);
        }

        public List<Trip> Trips()
        {
            return trips;
        }

        public bool IsFriendOf(User user)
        {
            return friends.Contains(user);
        }
    }
}
