using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    private static readonly UserService _userService = new UserService();
    [Fact]
    public void CheckNameAndSurname_ValidNames_ReturnsTrue()
    {
        string firstName = "John";
        string lastName = "Doe";

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.True(result);
    }

    [Fact]
    public void CheckNameAndSurname_NullFirstName_ReturnsFalse()
    {
        string firstName = null;
        string lastName = "Doe";

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.False(result);
    }

    [Fact]
    public void CheckNameAndSurname_NullLastName_ReturnsFalse()
    {
        string firstName = "John";
        string lastName = null;

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.False(result);
    }

    [Fact]
    public void CheckNameAndSurname_EmptyFirstName_ReturnsFalse()
    {
        string firstName = "";
        string lastName = "Doe";

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.False(result);
    }

    [Fact]
    public void CheckNameAndSurname_EmptyLastName_ReturnsFalse()
    {
        string firstName = "John";
        string lastName = "";

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.False(result);
    }

    [Fact]
    public void CheckNameAndSurname_EmptyNames_ReturnsFalse()
    {
        string firstName = "";
        string lastName = "";

        bool result = _userService.CheckNameAndSurname(firstName, lastName);

        Assert.False(result);
    }

    [Fact]
    public void CheckEmail_ValidEmail_ReturnsTrue()
    {
        string email = "example@example.com";
        
        bool result = _userService.CheckEmail(email);
        
        Assert.True(result);
    }

    [Fact]
    public void CheckEmail_InvalidEmail_NoAtSymbol_ReturnsFalse()
    {
        string email = "exampleexample.com";
        
        bool result = _userService.CheckEmail(email);
        
        Assert.False(result);
    }

    [Fact]
    public void CheckEmail_InvalidEmail_NoDotSymbol_ReturnsFalse()
    {
        string email = "example@examplecom";
        
        bool result = _userService.CheckEmail(email);
        
        Assert.False(result);
    }

    [Fact]
    public void CheckEmail_InvalidEmail_NoAtAndDotSymbols_ReturnsFalse()
    {
        string email = "exampleexamplecom";
        
        bool result = _userService.CheckEmail(email);
        
        Assert.False(result);
    }

    [Fact]
    public void CalculateAge_BirthDateToday_ReturnsZero()
    {
        DateTime birthDate = DateTime.Now.Date;
        
        int age = _userService.CalculateAge(birthDate);
        
        Assert.Equal(0, age);
    }

    [Fact]
    public void CalculateAge_BirthDateFuture_ReturnsNegative()
    {
        DateTime birthDate = DateTime.Now.Date.AddYears(1);
        
        int age = _userService.CalculateAge(birthDate);
        
        Assert.True(age < 0);
    }

    [Fact]
    public void CalculateAge_BirthDatePast_ReturnsPositive()
    {
        DateTime birthDate = DateTime.Now.Date.AddYears(-20);

        int age = _userService.CalculateAge(birthDate);

        Assert.True(age >= 0);
    }

    [Fact]
    public void CalculateAge_BirthDateFutureBeforeNow_ReturnsNegative()
    {
        DateTime birthDate = DateTime.Now.Date.AddYears(1).AddDays(-1);
        
        int age = _userService.CalculateAge(birthDate);

        Assert.True(age < 0);
    }

    [Fact]
    public void CalculateAge_BirthDatePastBeforeNow_ReturnsPositive()
    {
        DateTime birthDate = DateTime.Now.Date.AddYears(-20).AddDays(-1);
        
        int age = _userService.CalculateAge(birthDate);
        
        Assert.True(age >= 0);
    }

    [Fact]
    public void ValidateAge_AgeBelow21_ReturnsFalse()
    {
        int age = 20;

        bool result = _userService.ValidateAge(age);

        Assert.False(result);
    }

    [Fact]
    public void ValidateAge_AgeEqual21_ReturnsTrue()
    {
        int age = 21;

        bool result = _userService.ValidateAge(age);

        Assert.True(result);
    }

    [Fact]
    public void ValidateAge_AgeAbove21_ReturnsTrue()
    {
        int age = 25;

        bool result = _userService.ValidateAge(age);

        Assert.True(result);
    }
}