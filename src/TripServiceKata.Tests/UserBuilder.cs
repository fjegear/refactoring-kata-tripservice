using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripServiceKata.Trips;
using TripServiceKata.Users;

namespace TripServiceKata.Tests
{
    internal class UserBuilder
    {
        private User[] _friends = new User[] { };
        private Trip[] _trips = new Trip[] { };

        public static UserBuilder CreateBuilder()
        {
            return new UserBuilder();
        }

        public UserBuilder WithFriends(params User[] friends)
        {
            _friends = friends;
            return this;
        }

        public UserBuilder WithTrips(params Trip[] trips)
        {
            _trips = trips;
            return this;
        }

        public User Build()
        {
            var user = new User();
            AddFriendsTo(user);
            AddTripsTo(user);
            return user;
        }

        private void AddTripsTo(User user)
        {
            foreach (var trip in _trips)
            {
                user.AddTrip(trip);
            }
        }

        private void AddFriendsTo(User user)
        {
            foreach (var friend in _friends)
            {
                user.AddFriend(friend);
            }
        }
    }
}
