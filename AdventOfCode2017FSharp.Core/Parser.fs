namespace AdventOfCode2017FSharp.Core

module Parser =

   let splitLines (input : string) = input.Split "\r\n"

   let parse (input : string) wordParser =  
        input
            |> splitLines
            |> Array.map (fun line -> line.Split [|' ';'\t'|] |> Array.map wordParser)

   let parseWords input = parse input id
   
   let parseNumbers input = parse input int

   let asSingleColumn (input : 'a [][]) = input |> Array.map (fun line -> line.[0])
