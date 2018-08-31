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

    let calculateCorrectWeight (lines : Map<string, Line>) bottomProgram =
        let rec calculateCorrectWeight' key =
            let program = lines.[key]
            match program.Dependents.IsEmpty with
                | true -> program.Weight
                | false -> 
                    let dependentWeights = program.Dependents 
                                            |> Map.map (fun k _ -> calculateCorrectWeight' k)
                    let groups = dependentWeights
                                    |> Seq.groupBy (fun x -> x.Value)
                                    |> Seq.sortBy (fun x -> snd x |> Seq.length)
                                    |> Seq.toArray
                    match groups.Length with
                        | 1 -> program.Weight + (fst groups.[0] * (snd groups.[0] |> Seq.length))
                        | _ ->
                            let keyOfProgramToAdjust = ((snd groups.[0]) |> Seq.head).Key
                            let programToAdjust = lines.[keyOfProgramToAdjust]
                            let adjustment = fst groups.[1] - fst groups.[0]

                            programToAdjust.Weight + adjustment

        calculateCorrectWeight' bottomProgram.Name

    let calculatePart1 lines =
        let found = lines |> Map.findKey 
                                (fun _ line -> (not << Map.exists 
                                                    (fun _ line' -> 
                                                        line'.Dependents.ContainsKey(line.Name) )) lines)
        lines.[found]

    let calculatePart2 lines =
        let bottomProgram = calculatePart1 lines

        calculateCorrectWeight lines bottomProgram