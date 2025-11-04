using FluentAssertions;
using LeaderboardSystem.Application.Auth.Commands.Register;
using Xunit;

namespace LeaderboardSystem.UnitTests.Application.Validators
{
    public class RegisterCommandValidatorTests
    {
        private readonly RegisterCommandValidator _validator;

        public RegisterCommandValidatorTests()
        {
            _validator = new RegisterCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Username_Is_Empty()
        {
            var command = new RegisterCommand("", "test@example.com", "Test@123");
            var result = _validator.Validate(command);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Username");
        }

        [Fact]
        public void Should_Have_Error_When_Username_Is_Too_Short()
        {
            var command = new RegisterCommand("ab", "test@example.com", "Test@123");
            var result = _validator.Validate(command);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var command = new RegisterCommand("testuser", "invalid-email", "Test@123");
            var result = _validator.Validate(command);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Email");
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Weak()
        {
            var command = new RegisterCommand("testuser", "test@example.com", "weak");
            var result = _validator.Validate(command);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Password");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new RegisterCommand("testuser", "test@example.com", "Test@123!");
            var result = _validator.Validate(command);
            result.IsValid.Should().BeTrue();
        }
    }
}
