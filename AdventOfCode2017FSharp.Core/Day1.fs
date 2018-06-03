namespace AdventOfCode2017FSharp.Core

module Day1 =
    open System

    let parse (input : string) = 
        input 
            |> Seq.toList 
            |> List.map (string >> int)
    let calculate part input =
        let length = List.length input
        let offset = 
            match part with
                | 1 -> 1
                | 2 -> length / 2
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")
        [0..length - 1] 
            |> List.fold (fun acc i -> 
                if input.[i] = input.[(i + offset) % length] then 
                    acc + input.[i]
                else
                    acc) 0