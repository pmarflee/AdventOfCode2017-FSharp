namespace AdventOfCode2017FSharp.Core

module Parser =

   let parse (input : string) wordParser =  
        input.Split "\r\n" 
            |> Array.map (fun line -> line.Split [|' ';'\t'|] |> Array.map wordParser)

   let parseWords input = parse input id
   
   let parseNumbers input = parse input int

   let asSingleColumn (input : 'a [][]) = input |> Array.map (fun line -> line.[0])
