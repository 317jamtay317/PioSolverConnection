// See https://aka.ms/new-console-template for more information

using Client.Plugins;
using Client.Util;
using PioConnection.Commands;
using PioConnection.Commands.Abstractions;

var rangeMetadata = new RangeMetadata(
    @"C:\PioSOLVER\PioSOLVER2-pro.exe",
    @$"""F:\Sims\Chip Ev\40 BB\SRP\CO vs BB\As2s2h.cfr""");
var loadTreeCommand = new LoadTreeCommand(rangeMetadata);
loadTreeCommand.Execute();
var rangeCommand = new RangeCommand(loadTreeCommand.SolverConnection);
var result = rangeCommand.Execute();

Console.WriteLine(result.First());