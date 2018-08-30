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

    let calculateCorrectWeight (lines : Map<string, Line>) bottom =
        let rec getWeight key =
            let program = lines.[key]
            let dependentWeights = program.Dependents |> Seq.sumBy (fun kv -> getWeight kv.Key)
            program.Weight + dependentWeights

        let dependentWeights = bottom.Dependents |> Map.map (fun k _ -> getWeight k) 
        let groups = dependentWeights
                        |> Seq.groupBy (fun x -> x.Value)
                        |> Seq.sortBy (fun x -> snd x |> Seq.length)
                        |> Seq.toArray
        let keyOfProgramToAdjust = ((snd groups.[0]) |> Seq.head).Key
        let program = lines.[keyOfProgramToAdjust]
        let adjustment = fst groups.[1] - fst groups.[0]

        program.Weight + adjustment

    let calculatePart1 lines =
        let found = lines |> Map.findKey 
                                (fun _ line -> (not << Map.exists 
                                                    (fun _ line' -> 
                                                        line'.Dependents.ContainsKey(line.Name) )) lines)
        lines.[found]

    let calculatePart2 lines =
        let bottomProgram = calculatePart1 lines

        calculateCorrectWeight lines bottomProgram