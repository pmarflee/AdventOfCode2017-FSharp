namespace AdventOfCode2017FSharp.Core

module Day9 = 
    open System

    [<Flags>]
    type Mode = Group = 1 | Garbage = 2 | Cancel = 4

    type State = { Score : int;
                   Depth : int;
                   Mode : Mode }
                   member this.HasFlag flag = this.Mode &&& flag = flag
                   member this.SetFlag flag = this.Mode ||| flag
                   member this.ToggleFlag flag = this.Mode ^^^ flag

    let parse (input : string) = Seq.toList input

    let (|Cancel|_|) ((state : State, _)) = 
        if state.HasFlag Mode.Cancel then Some() else None

    let (|CancelNextCharacter|_|) ((state : State, c : char)) =
        if c = '!' && state.HasFlag Mode.Garbage then Some() else None

    let (|BeginGroup|_|) ((state : State, c : char)) =
        if c = '{' && state.HasFlag Mode.Group then Some() else None

    let (|EndGroup|_|) ((state : State, c : char)) =
        if c = '}' && state.HasFlag Mode.Group then Some() else None

    let (|BeginGarbage|_|) ((state : State, c : char)) =
        if c = '<' && state.HasFlag Mode.Group then Some() else None

    let (|EndGarbage|_|) ((state : State, c : char)) =
        if c = '>' && state.HasFlag Mode.Garbage then Some() else None

    let calculate (input : char list) = 
        let result = input |> List.fold 
                                (fun state c -> 
                                    match (state, c) with
                                    | Cancel ->
                                        { state with Mode = state.ToggleFlag Mode.Cancel }
                                    | CancelNextCharacter ->
                                        { state with Mode = state.SetFlag Mode.Cancel }
                                    | BeginGroup -> 
                                        { state with Depth = state.Depth + 1 }
                                    | EndGroup -> 
                                        { state with Depth = state.Depth - 1; 
                                                     Score = state.Score + state.Depth }
                                    | BeginGarbage ->
                                        { state with Mode = Mode.Garbage }
                                    | EndGarbage ->
                                        { state with Mode = Mode.Group }
                                    | _ -> state) 
                                { Score = 0; Depth = 0; Mode = Mode.Group }
        result.Score