module Day6

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("0\t2\t7\t0", 1, 5)>]
[<InlineData("0\t2\t7\t0", 2, 4)>]
let ``Calculate Memory Allocation`` input part expected =
    input |> Day6.parse |> Day6.calculate part |> should equal expected
