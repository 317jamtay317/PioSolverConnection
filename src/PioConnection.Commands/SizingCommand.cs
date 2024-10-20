// ***********************************************************************
// Assembly         : PioConnection.Commands
// Author           : james
// Created          : 10-08-2024
//
// Last Modified By : james
// Last Modified On : 10-18-2024
// ***********************************************************************
// <copyright file="SizingCommand.cs" company="PioConnection.Commands">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Client.Plugins;
using Client.Util;
using PioConnection.Commands.Abstractions;

namespace PioConnection.Commands;

/// <summary>
/// Class SizingCommand.
/// Implements the <see cref="PioConnection.Commands.RangeCommand" />
/// </summary>
/// <seealso cref="PioConnection.Commands.RangeCommand" />
public class SizingCommand : RangeCommand
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="connection">The connection to be passed in</param>
    public SizingCommand(ISolverConnection connection) : base(connection)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="metadata">Metadata to create the connection</param>
    public SizingCommand(SolverMetadata metadata) : base(metadata)
    {
    }

    /// <summary>
    /// Gets the solver command request.
    /// </summary>
    /// <value>The solver command request.</value>
    public override CommandRequest SolverCommandRequest => CommandRequest.BetSizing();
}