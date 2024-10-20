namespace PioConnection.Commands;

public struct CommandRequest
{
    /// <summary>
    /// Loads the tree from the content of the save file into memory.
    /// State.root points to this tree
    /// </summary>
    public static CommandRequest LoadTree() => new(SolverCommands.LoadTree);
    /// <summary>
    /// deletes current tree and frees the memory.
    /// </summary>
    public static CommandRequest FreeTree() => new(SolverCommands.LoadTree);
    /// <summary>
    /// Without loading a tree it prints information ontains information from following commands:
    ///      show_node r
    ///      show_range OOP r
    ///      show_range IP r
    ///      show_tree_info
    /// </summary>
    public static CommandRequest ShowMetadata() => new(SolverCommands.ShowMetadata);

    public static CommandRequest ShowNode() => new(SolverCommands.ShowNode);
    public static CommandRequest ShowHumanReadableStratigy() => new(SolverCommands.ShowHumanReadableStratigy);
    public static CommandRequest BetSizing() => new(SolverCommands.BetSizing);
    internal CommandRequest(string requestName)
    {
        RequestName = requestName;
    }

    /// <summary>
    /// The name of the command the solver runs
    /// </summary>
    public string RequestName { get; }

    public override string ToString() =>
        RequestName;
}