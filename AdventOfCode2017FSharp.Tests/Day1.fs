module Day1

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("1122", 1, 3)>]
[<InlineData("1111", 1, 4)>]
[<InlineData("1234", 1, 0)>]
[<InlineData("91212129", 1, 9)>]
[<InlineData("1212", 2, 6)>]
[<InlineData("1221", 2, 0)>]
[<InlineData("123425", 2, 4)>]
[<InlineData("123123", 2, 12)>]
[<InlineData("12131415", 2, 4)>]
let ``Calculate Inverse Captcha`` input part expected =
    input |> Day1.parse |> Day1.calculate part |> should equal expected