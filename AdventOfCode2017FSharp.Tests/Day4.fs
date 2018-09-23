module Day4

open Xunit
open FsUnit.Xunit
open AdventOfCode2017FSharp.Core

[<Theory>]
[<InlineData("aa bb cc dd ee", 1, 1)>]
[<InlineData("aa bb cc dd aa", 1, 0)>]
[<InlineData("aa bb cc dd aaa", 1, 1)>]
[<InlineData("abcde fghij", 2, 1)>]
[<InlineData("abcde xyz ecdab", 2, 0)>]
[<InlineData("a ab abc abd abf abj", 2, 1)>]
[<InlineData("iiii oiii ooii oooi oooo", 2, 1)>]
[<InlineData("oiii ioii iioi iiio", 2, 0)>]
let ``Calculate High-Entropy Passphrase`` input part expected =
    input |> Parser.parseWords Parser.splitChars |> Day4.calculate part |> should equal expected