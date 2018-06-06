namespace AdventOfCode2017FSharp.Core

module Day6 =
    open System

    type State = { Cycles : int; History : Map<string, int> }
    type Bank = { Index : int; Number : int }

    let parse input = input |> Parser.parseNumbers |> Array.head

    let calculate part (input : int []) =
        
        let toCsv = fun () -> String.Join(',', input)

        let getResult =
            match part with
                | 1 -> fun state -> state.Cycles
                | 2 -> fun state -> state.Cycles - state.History.[toCsv()]
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")

        let getBankWithMostBlocks =
            fun () ->
                input |> Array.foldi 
                    (fun i bank n -> 
                        if n > bank.Number || n = bank.Number && i < bank.Index then
                            { Index = i; Number = n }
                        else bank)
                    { Index = 0; Number = 0 }

        let getNextIndex index = (index + 1) % input.Length

        let rec calculate' state =
            let key = toCsv()
            let newHistory = state.History |> Map.add key state.Cycles
            let bank = getBankWithMostBlocks()
            Array.set input bank.Index 0
            [0..bank.Number - 1] 
                |> List.fold
                    (fun index _ ->
                        Array.set input index (input.[index] + 1)
                        getNextIndex index) 
                    (getNextIndex bank.Index) |> ignore
            let newState = { Cycles = state.Cycles + 1; History = newHistory }
            if newHistory.ContainsKey (toCsv()) then getResult newState
            else calculate' newState 

        calculate' { Cycles = 0; History = Map.empty }
