namespace AdventOfCode2017FSharp.Core

module Day5 =
    open System

    let parse input = input |> Parser.parseNumbers |> Array.map (fun line -> line.[0])

    let calculate part input =
        
        let nextOffset = 
            match part with
                | 1 -> fun offset -> offset + 1
                | 2 -> fun offset -> if offset >= 3 then offset - 1 else offset + 1
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")

        let steps =
            let input' = Array.copy input            
            let rec steps' steps position =
                if position >= input.Length then steps
                else
                    let offset = input'.[position]
                    let next = position + offset
                    Array.set input' position (nextOffset offset)
                    steps' (steps + 1) next
            steps' 0 0
        
        steps
