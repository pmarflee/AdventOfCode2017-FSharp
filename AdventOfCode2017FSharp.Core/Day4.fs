namespace AdventOfCode2017FSharp.Core

module Day4 =
    open System.Collections.Generic
    open System

    let private isAValidPasscode getKey =
        let dict = new Dictionary<string, string>()
        fun word -> 
            let key = getKey word
            match dict.ContainsKey(key) with
                | true -> false
                | false -> dict.Add(key, word); true

    let calculate part input =
        let getKey = 
            match part with
                | 1 -> id
                | 2 -> fun word -> word |> Array.ofSeq |> Array.sort |> String
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")

        input 
            |> Array.filter (fun line -> line |> Array.forall (isAValidPasscode getKey))
            |> Array.length
