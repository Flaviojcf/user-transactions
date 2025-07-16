using FluentAssertions;
using UserTransactions.Tests.Shared.Builders.Entities;
using UserEntity = UserTransactions.Domain.Entities.User;

namespace UserTransactions.Tests.Domain.Entities
{
    public class UserEntityTest
    {
        [Fact]
        public void Given_ValidUser_When_CalledConstructor_ShouldCreateUser()
        {
            //Arrange
            var user = UserEntityBuilder.Build();

            //Act
            var userEntity = new UserEntity(user.FullName, user.Email, user.CPF, user.Password, user.UserType);

            //Assert
            userEntity.Should().NotBeNull();

            userEntity.Id.Should().NotBeEmpty();
            userEntity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
            userEntity.UpdatedAt.Should().HaveHour(0);
            userEntity.IsActive.Should().BeTrue();

            userEntity.FullName.Should().Be(user.FullName);
            userEntity.Email.Should().Be(user.Email);
            userEntity.CPF.Should().Be(user.CPF);
            userEntity.Password.Should().Be(user.Password);
            userEntity.UserType.Should().Be(user.UserType);
        }

        [Fact]
        public void Given_User_When_Activate_ShouldSetIsActiveToTrueAndUpdateUpdatedAt()
        {
            // Arrange
            var user = UserEntityBuilder.Build();
            var userEntity = new UserEntity(user.FullName, user.Email, user.CPF, user.Password, user.UserType);

            // Act
            userEntity.Activate();

            // Assert
            userEntity.IsActive.Should().BeTrue();
            userEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Given_User_When_DeActivate_ShouldSetIsActiveToFalseAndUpdateUpdatedAt()
        {
            // Arrange
            var user = UserEntityBuilder.Build();
            var userEntity = new UserEntity(user.FullName, user.Email, user.CPF, user.Password, user.UserType);

            // Act
            userEntity.Deactivate();

            // Assert
            userEntity.IsActive.Should().BeFalse();
            userEntity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        }
    }
}