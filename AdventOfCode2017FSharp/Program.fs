﻿// Learn more about F# at http://fsharp.org

open AdventOfCode2017FSharp.Core

[<EntryPoint>]
let main argv =
    printfn "Advent of Code 2017 Solutions"
    printfn "============================="
    printfn ""

    Runner.run "Day 1 Part 1:" "Day1.txt" (Day1.parse >> Day1.calculate 1)
    Runner.run "Day 1 Part 2:" "Day1.txt" (Day1.parse >> Day1.calculate 2)
    Runner.run "Day 2 Part 1:" "Day2.txt" (Day2.parse >> Day2.calculate 1)
    Runner.run "Day 2 Part 2:" "Day2.txt" (Day2.parse >> Day2.calculate 2)

    printfn ""
    printfn "Finished"

    0 // return an integer exit code
