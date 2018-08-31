namespace Day8
    module Day8 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2017FSharp.Core.Day8

        type Tests () =

            static member Instructions
                with get() =
                    [ [| "b inc 5 if a > 1" :> obj; 
                         { Register = "b"; 
                           AdjustmentType = Increase; 
                           AdjustmentAmount = 5; 
                           Condition = { Register = "a"; 
                                         Operator = GreaterThan; 
                                         Amount = 1 } } :> obj |];
                      [| "a inc 1 if b < 5";
                         { Register = "a";
                           AdjustmentType = Increase;
                           AdjustmentAmount = 1;
                           Condition = { Register = "b";
                                         Operator = LessThan;
                                         Amount = 5 } } |];
                      [| "c dec -10 if a >= 1";
                         { Register = "c";
                           AdjustmentType = Decrease;
                           AdjustmentAmount = -10;
                           Condition = { Register = "a";
                                         Operator = GreaterThanOrEqual;
                                         Amount = 1 } } |];
                      [| "c inc -20 if c == 10";
                         { Register = "c";
                           AdjustmentType = Increase;
                           AdjustmentAmount = -20;
                           Condition = { Register = "c";
                                         Operator = Equals;
                                         Amount = 10 } } |] ]

            [<Theory>]
            [<MemberData("Instructions")>]
            member verify.``Instruction can be parsed`` (input : string, expected : Instruction) =
                parseInstruction input |> should equal expected

    
