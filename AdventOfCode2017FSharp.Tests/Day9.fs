module Day9

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("{}", 1)>]
[<InlineData("{{{}}}", 6)>]
[<InlineData("{{},{}}", 5)>]
[<InlineData("{{{},{},{{}}}}", 16)>]
[<InlineData("{<{},{},{{}}>}", 1)>]
[<InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)>]
[<InlineData("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)>]
[<InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)>]
let ``Calculate total score for all groups in input`` input expected =
    input |> Day9.parse |> Day9.calculate |> should equal expected