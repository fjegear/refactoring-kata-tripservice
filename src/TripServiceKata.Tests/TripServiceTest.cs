using FluentAssertions;
using NSubstitute;
using TripServiceKata.Exceptions;
using TripServiceKata.Trips;
using TripServiceKata.Users;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        private readonly int ZeroTrips = 0;
        private static User? _loggedInUser;
        private static User? _guest = null;
        private static User _registeredUser = new User();
        private static User _user = new User();
        private static Trip _trip1 = new Trip();
        private static Trip _trip2 = new Trip();

        private readonly TripService _tripService;
        private readonly IUserSession _userSession;
        private readonly ITripDAO _tripDAO;

        public TripServiceTest()
        {
            _userSession = Substitute.For<IUserSession>();
            _tripDAO = Substitute.For<ITripDAO>();

            _tripService = new TripService(_userSession, _tripDAO);
        }

        [Fact]
        public void Should_not_allow_to_get_trips_When_user_is_not_logged_in()
        {
            //Arrange
            _loggedInUser = _guest;
            _userSession.GetLoggedUser().Returns(_loggedInUser);
            _tripDAO.GetTripsBy(_user).Returns(_user.Trips());

            //Act
            var action = () => _tripService.GetTripsByUser(_user);

            //Assert
            action.Should().Throw<UserNotLoggedInException>();
        }

        [Fact]
        public void Should_not_get_trips_When_users_are_not_friends()
        {
            //Arrange
            _loggedInUser = _registeredUser;

            var notFriend = UserBuilder.CreateBuilder()
                .WithFriends(_user)
                .WithTrips(_trip1, _trip2)
                .Build();

            _userSession.GetLoggedUser().Returns(_loggedInUser);
            _tripDAO.GetTripsBy(notFriend).Returns(notFriend.Trips());

            //Act
            var trips = _tripService.GetTripsByUser(notFriend);

            //Assert
            trips.Should().HaveCount(ZeroTrips);
        }

        [Fact]
        public void Should_get_trips_When_users_are_friends()
        {
            //Arrange
            _loggedInUser = _registeredUser;

            var friend = UserBuilder.CreateBuilder()
                .WithFriends(_user, _loggedInUser)
                .WithTrips(_trip1, _trip2)
                .Build();

            _userSession.GetLoggedUser().Returns(_loggedInUser);
            _tripDAO.GetTripsBy(friend).Returns(friend.Trips());

            //Act
            var trips = _tripService.GetTripsByUser(friend);

            //Assert
            trips.Should().HaveCount(friend.Trips().Count);
        }
    }
}
