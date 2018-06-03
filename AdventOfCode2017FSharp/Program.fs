// Learn more about F# at http://fsharp.org

open AdventOfCode2017FSharp.Core

[<EntryPoint>]
let main argv =
    Runner.run "Day 1 Part 1:" "Day1.txt" (Day1.parse >> Day1.calculate 1)
    Runner.run "Day 1 Part 2:" "Day1.txt" (Day1.parse >> Day1.calculate 2)
    0 // return an integer exit code
