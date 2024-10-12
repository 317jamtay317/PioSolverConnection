using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using PioConnection.Api.Services;
using PioConnection.Dtos;

namespace PioConnection.Api.Tests.Unit.Services;

public class SolverFilePathServiceTests
{
    public SolverFilePathServiceTests()
    {
        _sut = new(_configuration);
    }
    [Fact]
    public void GetFilePath_ShouldReturnCorrectFilePath_WhenMetadataMeetsCriterion()
    {
        //arrange
        var configurationSection = Substitute.For<IConfigurationSection>();
        configurationSection.Value = "F:\\{GameType}\\{StackSize}\\{Positions}";
        _configuration.GetSection("file-path-convention").Returns(configurationSection);
        SolverFilePathMetadata metadata = new SolverFilePathMetadata(
            [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            GameType.Tournaments,
            StackSize._30,
            PlayerPosition.BB,
            PlayerPosition.BTN);
        //act
        var filePath = _sut.GetFilePath(metadata);
        //assert
        filePath.Should().Be(@"F:\Tournaments\30BB\BTN vs BB\AcAdAh.cfr");
    }

    [Fact]
    public void GetFilePath_ShouldThrowArgumentNullException_WhenNamingConventionDoesntExistInConfiguration()
    {
        //arrange
        SolverFilePathMetadata metadata = new SolverFilePathMetadata(
            [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            GameType.Tournaments,
            StackSize._30,
            PlayerPosition.BB,
            PlayerPosition.BTN);
        Action action = () => _sut.GetFilePath(metadata);
        //act

        //assert
        action.Should().Throw<ArgumentNullException>()
            .WithMessage("Please add the file naming convention to the enviromment (Parameter 'filePathNamingConvention')");
    }

    [Fact]
    public void GetFilePath_ShouldThrowArguementException_WhenStackSizeIsNotDefiend()
    {
        //arrange
        var configurationSection = Substitute.For<IConfigurationSection>();
        configurationSection.Value = "F:/{GameType}/{StackSize}/{Positions}";
        _configuration.GetSection("file-path-convention").Returns(configurationSection);
        SolverFilePathMetadata metadata = new SolverFilePathMetadata(
            [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            GameType.Tournaments,
            StackSize.NotDefined,
            PlayerPosition.BB,
            PlayerPosition.BTN);
        Action action = () => _sut.GetFilePath(metadata);
        //act

        //assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("Please define the stack size");

    }

    private IConfiguration _configuration = Substitute.For<IConfiguration>();
    private SolverFileService _sut;
}