namespace Day10
    module Day10 =

        open Xunit
        open FsUnit.Xunit
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

            static member ReverseLengthTestData
                with get() =
                    let theoryData = new TheoryData<int[], int, int, int, (int[] * int * int)>()
                    theoryData.Add([|0;1;2;3;4|], 3, 0, 0, ([|2;1;0;3;4|], 3, 1))
                    theoryData.Add([|2;1;0;3;4|], 4, 3, 1, ([|4;3;0;1;2|], 3, 2))
                    theoryData.Add([|4;3;0;1;2|], 1, 3, 2, ([|4;3;0;1;2|], 1, 3))
                    theoryData.Add([|4;3;0;1;2|], 5, 1, 3, ([|3;4;2;1;0|], 4, 4))
                    theoryData

            [<Fact>]
            member verify. ``Should calculate correct result for knot hash`` () =
                Day10.calculate [|0;1;2;3;4|] [3;4;1;5] |> should equal 12