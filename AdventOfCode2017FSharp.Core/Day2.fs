namespace AdventOfCode2017FSharp.Core

module Day2 =
    open System

    let calculate part (input : seq<int[]>) =
        let rowValuePart1 row = 
            let min, max = row
                            |> Seq.fold 
                                (fun (min, max) n -> 
                                    let newMin = match min with
                                                    | None -> n
                                                    | Some(x) when x > n -> n
                                                    | Some(x) -> x
                                    let newMax = match max with
                                                    | None -> n
                                                    | Some(x) when x < n -> n
                                                    | Some(x) -> x
                                    Some(newMin), Some(newMax)) (None, None)
            max.Value - min.Value

        let rowValuePart2 (row : int[]) =
            seq {
                for i in 0..row.Length - 1 do
                    for j in i + 1..row.Length - 1 do
                        let larger = if row.[i] > row.[j] then row.[i] else row.[j]
                        let smaller = if row.[i] < row.[j] then row.[i] else row.[j]
                        if larger % smaller = 0 then
                            yield larger / smaller
            } |> Seq.head
        
        let rowValue = match part with
                        | 1 -> rowValuePart1
                        | 2 -> rowValuePart2
                        | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")
        
        input |> Seq.map rowValue |> Seq.sum
