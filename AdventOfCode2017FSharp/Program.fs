// Learn more about F# at http://fsharp.org

open AdventOfCode2017FSharp.Core
open AdventOfCode2017FSharp
open System.IO

[<EntryPoint>]
let main _ =
    printfn "Advent of Code 2017 Solutions"
    printfn "============================="
    printfn ""

    let read path = File.ReadAllText path

    Runner.run "Day 1 Part 1:" (read "Day1.txt") (Day1.parse >> Day1.calculate 1)
    Runner.run "Day 1 Part 2:" (read "Day1.txt") (Day1.parse >> Day1.calculate 2)
    Runner.run "Day 2 Part 1:" (read "Day2.txt") (Parser.parseNumbers Parser.splitChars >> Day2.calculate 1)
    Runner.run "Day 2 Part 2:" (read "Day2.txt") (Parser.parseNumbers Parser.splitChars >> Day2.calculate 2)
    Runner.run "Day 3 Part 1:" 361527 (Day3.calculate 1)
    Runner.run "Day 3 Part 2:" 361527 (Day3.calculate 2)
    Runner.run "Day 4 Part 1:" (read "Day4.txt") (Parser.parseWords Parser.splitChars >> Day4.calculate 1)
    Runner.run "Day 4 Part 1:" (read "Day4.txt") (Parser.parseWords Parser.splitChars >> Day4.calculate 2)
    Runner.run "Day 5 Part 1:" (read "Day5.txt") (Day5.parse >> Day5.calculate 1)
    Runner.run "Day 5 Part 2:" (read "Day5.txt") (Day5.parse >> Day5.calculate 2)
    Runner.run "Day 6 Part 1:" (read "Day6.txt") (Day6.parse >> Day6.calculate 1)
    Runner.run "Day 6 Part 2:" (read "Day6.txt") (Day6.parse >> Day6.calculate 2)
    Runner.run "Day 7 Part 1:" (read "Day7.txt") (Day7.parse >> Day7.calculatePart1)
    Runner.run "Day 7 Part 2:" (read "Day7.txt") (Day7.parse >> Day7.calculatePart2)
    Runner.run "Day 8 Part 1:" (read "Day8.txt") (Day8.parse >> Day8.calculate 1)
    Runner.run "Day 8 Part 2:" (read "Day8.txt") (Day8.parse >> Day8.calculate 2)
    Runner.run "Day 9 Part 1:" (read "Day9.txt") (Day9.parse >> Day9.calculate 1)
    Runner.run "Day 9 Part 2:" (read "Day9.txt") (Day9.parse >> Day9.calculate 2)
    Runner.run "Day 10 Part 1:" (read "Day10.txt") (Day10.parsePart1 >> Day10.calculate [|0..255|])

    printfn ""
    printfn "Finished"

    0 // return an integer exit code
