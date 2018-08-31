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

    type Match = | Some of int | None of int

    let getValue = function | Some(v) -> v | None(v) -> v

    let calculateCorrectWeight (lines : Map<string, Line>) bottomProgram =
        let rec calculateCorrectWeight' key =
            let program = lines.[key]
            match program.Dependents.IsEmpty with
                | true -> None(program.Weight)
                | false -> 
                    let dependentWeights = program.Dependents 
                                            |> Map.map (fun k _ -> calculateCorrectWeight' k)
                    match dependentWeights 
                            |> Map.tryFindKey (fun _ v -> match v with
                                                            | Some(_) -> true
                                                            | None(_) -> false) with
                        | Option.Some(key) -> dependentWeights.[key]
                        | Option.None ->
                            let groups = dependentWeights
                                            |> Seq.groupBy (fun x -> getValue x.Value)
                                            |> Seq.map (fun (weight, s) -> (weight, Array.ofSeq s))
                                            |> Seq.sortBy (fun (_, s) -> Array.length s)
                                            |> Seq.toArray
                            match groups.Length with
                                | 1 -> None(program.Weight + (fst groups.[0] * (snd groups.[0] |> Array.length)))
                                | _ ->
                                    let keyOfProgramToAdjust = ((snd groups.[0]) |> Array.head).Key
                                    let programToAdjust = lines.[keyOfProgramToAdjust]
                                    let adjustment = fst groups.[1] - fst groups.[0]

                                    Some(programToAdjust.Weight + adjustment)

        bottomProgram.Name |> calculateCorrectWeight' |> getValue

    let calculatePart1 lines =
        let found = lines |> Map.findKey 
                                (fun _ line -> (not << Map.exists 
                                                    (fun _ line' -> 
                                                        line'.Dependents.ContainsKey(line.Name) )) lines)
        lines.[found]

    let calculatePart2 lines =
        let bottomProgram = calculatePart1 lines

        calculateCorrectWeight lines bottomProgram