using FluentAssertions;
using TripServiceKata.Exceptions;
using TripServiceKata.Trips;
using TripServiceKata.Users;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        private static User? _loggedInUser = null;
        private readonly TestableTripService _tripService;

        public TripServiceTest()
        {
            _tripService = new TestableTripService();
        }

        [Fact]
        public void Should_not_allow_to_get_trips_When_user_is_not_logged_in()
        {
            //Arrange
            _loggedInUser = null;

            //Act
            var action = () => _tripService.GetTripsByUser(new User());

            //Assert
            action.Should().Throw<UserNotLoggedInException>();
        }

        private class TestableTripService : TripService
        {
            protected override User GetLoggedUser()
            {
                return _loggedInUser!;
            }
        }
    }
}
