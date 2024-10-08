using Client.Util;
using NSubstitute;

namespace PioConnection.Commands.Tests.Unit;

public class LoadTreeCommandTests
{
    [Fact]
    public void Execute_ShouldCallSolverConnection_WhenMetadataExists()
    {
        //arrange
        LoadTreeCommand command = new(_solverConnection);
        //act
        command.Execute();
        //assert
        _solverConnection
            .Received(1)
            .GetResponseFromSolver(Arg.Any<string>(), Arg.Any<object[]>());
    }

    private ISolverConnection _solverConnection = Substitute.For<ISolverConnection>();
}