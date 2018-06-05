namespace AdventOfCode2017FSharp

module Runner =
    let run title input func = printfn "%s %i" title (func input)