using Client.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Plugins;

/// <summary>
/// Load a file and output root strategy
/// </summary>
public class RangesPrinter
{
    private string TreePath { get; set; }

    private SolverConnection _solver { get; set; }


    public RangesPrinter(SolverMetadata metadata)
    {
        TreePath = metadata.TreePath;
        _solver = new SolverConnection(metadata.SolverPath);
    }


    public BothRanges Run()
    {
        TreeUtil.LoadTree(_solver, TreePath);
        var rangeOOP = TreeUtil.ShowRootRange(_solver, "OOP");
        var rangeIP = TreeUtil.ShowRootRange(_solver, "IP");
        return new BothRanges(rangeOOP, rangeIP);
    }
}

public record BothRanges(string OOPRange, string IPRange);

/// <summary>
/// Metadata that helps defind what tree tou want to get.
/// </summary>
/// <param name="SolverPath"></param>
/// <param name="TreePath">The full path of the *.cfr file</param>
public record SolverMetadata(string SolverPath, string TreePath);