namespace Day10
    module Day10 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2017FSharp.Core
        open AdventOfCode2017FSharp.Core

        type Tests () =

            [<Theory>]
            [<MemberData("ReverseLengthTestData")>]
            member verify.``Should reverse length in input`` 
                (input, length, position, skipsize, (expected, _, _)) =
                let (actual, _, _) = Day10.reverseLength input length position skipsize
                actual |> should equal expected
            
            [<Theory>]
            [<MemberData("ReverseLengthTestData")>]
            member verify.``Should update position when reversing length`` 
                (input, length, position, skipsize, (_, expected, _)) =
                let (_, actual, _) = Day10.reverseLength input length position skipsize
                actual |> should equal expected
            
            [<Theory>]
            [<MemberData("ReverseLengthTestData")>]
            member verify.``Should increment skip size when reversing length`` 
                (input, length, position, skipsize, (_, _, expected)) =
                let (_, _, actual) = Day10.reverseLength input length position skipsize
                actual |> should equal expected

            [<Theory>]
            [<MemberData("Part1TestData")>]
            member verify. ``Should calculate correct result for Part 1`` (input, numbers, expected) =
                Day10.calculatePart1 numbers (Day10.parsePart1 input) |> should equal expected

            [<Theory>]
            [<MemberData("InputPart1TestData")>]
            member verify.``Should correctly parse Part 1 lengths`` (input, expected) =
                Day10.parsePart1 input |> should equal expected

            [<Theory>]
            [<MemberData("InputPart2TestData")>]
            member verify.``Should correctly parse Part 2 lengths`` (input, expected) =
                Day10.parsePart2 input |> should equal expected

            [<Fact>]
            member verify.``Should calculate dense hash correctly`` () =
                [|65;27;9;1;4;3;40;50;91;7;6;0;2;5;68;22|] |> Day10.toDenseHash |> should equal [|64|]

            [<Theory>]
            [<InlineData("64,7,255", "4007ff")>]
            member verify.``Should calculate hex value for dense hash correctly`` (input, expected) =
                input 
                |> Parser.parseNumbers [|','|]
                |> Array.head 
                |> Day10.toHex 
                |> should equal expected

            static member ReverseLengthTestData
                with get() =
                    let theoryData = new TheoryData<int[], int, int, int, (int[] * int * int)>()
                    theoryData.Add([|0;1;2;3;4|], 3, 0, 0, ([|2;1;0;3;4|], 3, 1))
                    theoryData.Add([|2;1;0;3;4|], 4, 3, 1, ([|4;3;0;1;2|], 3, 2))
                    theoryData.Add([|4;3;0;1;2|], 1, 3, 2, ([|4;3;0;1;2|], 1, 3))
                    theoryData.Add([|4;3;0;1;2|], 5, 1, 3, ([|3;4;2;1;0|], 4, 4))
                    theoryData

            static member Part1TestData
                with get() =
                    let theoryData = new TheoryData<string, int[], int>()
                    theoryData.Add("3,4,1,5", [|0..4|], 12)
                    theoryData.Add("206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3", [|0..255|], 9656)
                    theoryData

            static member InputPart1TestData
                with get() =
                    let theoryData = new TheoryData<string, int[]>()
                    theoryData.Add(
                        "206,63,255,131,65,80,238,157,254,24,133,2,16,0,1,3",
                        [|206;63;255;131;65;80;238;157;254;24;133;2;16;0;1;3|])
                    theoryData

            static member InputPart2TestData
                with get() =
                    let theoryData = new TheoryData<string, int[]>()
                    theoryData.Add("1,2,3", [|49;44;50;44;51|])
                    theoryData