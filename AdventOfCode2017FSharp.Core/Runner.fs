module Runner

open System.IO

let run title source func =
    let input = File.ReadAllText source
    printfn "%s %i" title (func input)


