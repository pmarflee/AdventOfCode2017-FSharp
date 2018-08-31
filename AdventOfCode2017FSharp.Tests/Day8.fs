namespace Day8
    module Day8 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2017FSharp.Core.Day8

        type Tests () =

            let instructionsAreEqual expected actual =
                actual.Register |> should equal expected.Register
                actual.AdjustmentAmount |> should equal expected.AdjustmentAmount
                actual.Condition.Register |> should equal expected.Condition.Register
                actual.Condition.Amount |> should equal expected.Condition.Amount

            static member InstructionData
                with get() =
                    [ [| "b inc 5 if a > 1" :> obj; 
                         { Register = "b"; 
                           AdjustmentType = (+); 
                           AdjustmentAmount = 5; 
                           Condition = { Register = "a"; 
                                         Operator = (>); 
                                         Amount = 1 } } :> obj |];
                      [| "a inc 1 if b < 5";
                         { Register = "a";
                           AdjustmentType = (+);
                           AdjustmentAmount = 1;
                           Condition = { Register = "b";
                                         Operator = (<);
                                         Amount = 5 } } |];
                      [| "c dec -10 if a >= 1";
                         { Register = "c";
                           AdjustmentType = (-);
                           AdjustmentAmount = -10;
                           Condition = { Register = "a";
                                         Operator = (>=);
                                         Amount = 1 } } |];
                      [| "c inc -20 if c == 10";
                         { Register = "c";
                           AdjustmentType = (+);
                           AdjustmentAmount = -20;
                           Condition = { Register = "c";
                                         Operator = (=);
                                         Amount = 10 } } |] ]

            static member InstructionInput
                with get() =
                    Tests.InstructionData
                    |> List.map (fun line -> string (Array.head line))
                    |> String.concat "\r\n"

            static member Instructions
                with get() =
                    Tests.InstructionData
                    |> List.map (fun line -> line.[1] :?> Instruction)
                    |> Array.ofList

            [<Theory>]
            [<MemberData("InstructionData")>]
            member verify.``Instruction can be parsed`` (input : string, expected : Instruction) =
                instructionsAreEqual expected (parseInstruction input)

            [<Fact>]
            member verify.``Input can be parsed`` () =
                parse Tests.InstructionInput 
                    |> Array.zip Tests.Instructions
                    |> Array.iter (fun (actual, expected) -> instructionsAreEqual expected actual)

            [<Fact>]
            member verify.``Calculate largest value in any register`` () =
                Tests.Instructions |> calculate 1 |> should equal 1

            [<Fact>]
            member verify.``Calculate largest value in any register during the process`` () =
                Tests.Instructions |> calculate 2 |> should equal 10