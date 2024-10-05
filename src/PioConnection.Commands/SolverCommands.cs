namespace PioConnection.Commands;

internal class SolverCommands
{
    /// <summary>
    /// Loads the tree from the content of the save file into memory.
    /// State.root points to this tree
    /// </summary>
    internal const string LoadTree = "load_tree";

    /// <summary>
    /// deletes current tree and frees the memory.
    /// </summary>
    internal const string FreeTree = "free_tree";

    /// <summary>
    /// Without loading a tree it prints information ontains information from following commands:
    ///      show_node r
    ///      show_range OOP r
    ///      show_range IP r
    ///      show_tree_info
    /// </summary>
    internal const string ShowMetadata = "show_metadata";

    /// <summary>
    /// human readable sorted (by equity vs ALL) output
    /// </summary>
    internal const string ShowHumanReadableStratigy = "show_strategy_pp";

    /// <summary>
    /// 
    /// </summary>
    internal const string ShowNode = "show_node";
}