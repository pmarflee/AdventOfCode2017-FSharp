module Day3

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData(1, 1, 0)>]
[<InlineData(12, 1, 3)>]
[<InlineData(23, 1, 2)>]
[<InlineData(1024, 1, 31)>]
[<InlineData(1, 2, 2)>]
[<InlineData(2, 2, 4)>]
[<InlineData(4, 2, 5)>]
[<InlineData(10, 2, 11)>]
[<InlineData(747, 2, 806)>]
let ``Calculate Spiral Memory`` input part expected =
    Day3.calculate part input |> should equal expected
