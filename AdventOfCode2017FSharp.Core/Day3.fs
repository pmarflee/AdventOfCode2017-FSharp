namespace AdventOfCode2017FSharp.Core

module Day3 =
    open System.Collections.Generic
    open System

    type Direction = Right = 0 | Up = 1 | Left = 2 | Down = 3
    type Position = { X : int; Y : int }
    type Square = { Index : int; Position : Position; Score : int }

    let private scorePart2 = 
        fun () ->
            let scores = new Dictionary<string, int>()
            let offsetPairs = List.allPairs [-1..1] [-1..1]
            let scorePart2' i position =
                let score = 
                    match i with
                       | 1 -> 1
                       | _ -> 
                          offsetPairs    
                             |> List.fold 
                                (fun acc (x, y) ->
                                   let offsetPos = { X = position.X + x; Y = position.Y + y }
                                   match scores.TryGetValue (offsetPos.ToString()) with
                                      | true, score -> acc + score
                                      | _ -> acc) 0
                scores.TryAdd(position.ToString(), score) |> ignore
                score
            scorePart2'

    let calculate part input =
        let scorer = 
            match part with
                | 1 -> fun _ position -> abs position.X + abs position.Y
                | 2 -> scorePart2()
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")
        
        let canScore =
            match part with
                | 1 -> fun square -> square.Index = input
                | 2 -> fun square -> square.Score > input
                | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")

        let rec calculate' i j position direction step =
          let square = { Index = i; Position = position; Score = scorer i position }
          if canScore square then square.Score
          else
              let position' = 
                match direction with
                    | Direction.Right -> { X = position.X + 1; Y = position.Y }
                    | Direction.Up -> { X = position.X; Y = position.Y + 1 }
                    | Direction.Left -> { X = position.X - 1; Y = position.Y }
                    | Direction.Down -> { X = position.X; Y = position.Y - 1 }
                    | _ -> raise <| new ArgumentOutOfRangeException("direction")
              let direction' = 
                if j = step || j = step * 2 then 
                    enum<Direction>((int direction + 1) % 4)
                else
                    direction
              let step' = if j = step * 2 then step + 1 else step
              let j' = if j = step * 2 then 1 else j + 1
              calculate' (i + 1) j' position' direction' step'

        calculate' 1 1 { X = 0; Y = 0 } Direction.Right 1