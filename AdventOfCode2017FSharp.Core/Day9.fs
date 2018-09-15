namespace AdventOfCode2017FSharp.Core

module Day9 = 
    open System

    [<Flags>]
    type Mode = Group = 1 | Garbage = 2 | Cancel = 4

    type State = { Score : int;
                   Depth : int;
                   Mode : Mode }

    let parse (input : string) = Seq.toList input

    let hasFlag state flag = state.Mode &&& flag = flag
    let setFlag mode flag = mode ||| flag
    let toggleFlag mode flag = mode ^^^ flag

    let calculate (input : char list) = 
        let result = input |> List.fold 
                                (fun state c -> 
                                    match c with
                                    | _ when hasFlag state Mode.Cancel ->
                                        { state with Mode = toggleFlag state.Mode Mode.Cancel }
                                    | '!' when hasFlag state Mode.Garbage ->
                                        { state with Mode = setFlag state.Mode Mode.Cancel }
                                    | '{' when hasFlag state Mode.Group -> 
                                        { state with Depth = state.Depth + 1 }
                                    | '}' when hasFlag state Mode.Group -> 
                                        { state with Depth = state.Depth - 1; 
                                                     Score = state.Score + state.Depth }
                                    | '<' when hasFlag state Mode.Group ->
                                        { state with Mode = Mode.Garbage }
                                    | '>' when hasFlag state Mode.Garbage ->
                                        { state with Mode = Mode.Group }
                                    | _ -> state) 
                                { Score = 0; Depth = 0; Mode = Mode.Group }
        result.Score