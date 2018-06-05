module Day2

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("5 1 9 5\r\n7 5 3\r\n2 4 6 8", 1, 18)>]
[<InlineData("5 9 2 8\r\n9 4 7 3\r\n3 8 6 5", 2, 9)>]
let ``Calculate Corruption Checksum`` input part expected =
    input |> Parser.parseNumbers |> Day2.calculate part |> should equal expected