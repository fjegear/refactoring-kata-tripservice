using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripServiceKata.Users;

namespace TripServiceKata.Tests
{
    public class UserTests
    {
        private readonly User _user1 = new User();
        private readonly User _user2 = new User();

        [Fact]
        public void Should_inform_When_user_is_not_friend()
        {
            var user = UserBuilder.CreateBuilder()
                .WithFriends(_user1)
                .Build();

            user.IsFriendOf(_user2).Should().BeFalse();
        }

        [Fact]
        public void Should_inform_When_user_is_friend()
        {
            var user = UserBuilder.CreateBuilder()
                .WithFriends(_user1, _user2)
                .Build();

            user.IsFriendOf(_user1).Should().BeTrue();
        }
    }
}
