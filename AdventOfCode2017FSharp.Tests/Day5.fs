module Day5

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("0\r\n3\r\n0\r\n1\r\n-3", 1, 5)>]
[<InlineData("0\r\n3\r\n0\r\n1\r\n-3", 2, 10)>]
let ``Calculate High-Entropy Passphrase`` input part expected =
    input |> Day5.parse |> Day5.calculate part |> should equal expected