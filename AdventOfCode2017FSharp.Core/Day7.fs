namespace AdventOfCode2017FSharp.Core

module Day7 =

    open System.Text.RegularExpressions

    type Line = { Name : string; 
                  Weight : int; 
                  Dependents : Map<string, string> }
                override x.ToString() = sprintf "%s" x.Name

    let regex = new Regex("(?<name>\w+)\s\((?<weight>\d+)\)(\s->\s((?<dependent>\w+)(,\s)?)*)?")
    let parseLine input = 
        let match' = regex.Match(input)
        { Line.Name = match'.Groups.["name"].Value;
          Weight = int match'.Groups.["weight"].Value;
          Dependents = match'.Groups.["dependent"].Captures 
          |> Seq.cast<Capture> 
          |> Seq.map (fun c -> (c.Value, c.Value)) 
          |> Map.ofSeq } 

    let parse input = Parser.splitLines input 
                        |> Array.map parseLine 
                        |> Array.map (fun line -> (line.Name, line))
                        |> Map.ofArray

    let findBottomProgram (lines : Map<string, Line>) =
        let found = lines |> Map.findKey 
                                (fun _ line -> (not << Map.exists 
                                                    (fun _ line' -> 
                                                        line'.Dependents.ContainsKey(line.Name) )) lines)
        lines.[found]