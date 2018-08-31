namespace AdventOfCode2017FSharp.Core

module Day8 =

    open System.Text.RegularExpressions
    open System

    type Register = string

    type AdjustmentType = Increase | Decrease with
        static member Parse = function
            | "inc" -> Increase
            | "dec" -> Decrease
            | _ -> failwith "Invalid adjustment type. Should be 'inc' or 'dec'." 

    type Operator =
        | GreaterThan
        | LessThan
        | GreaterThanOrEqual
        | LessThanOrEqual
        | Equals
        | NotEquals with
            static member Parse = function
                | ">" -> GreaterThan
                | ">=" -> GreaterThanOrEqual
                | "<" -> LessThan
                | "<=" -> LessThanOrEqual
                | "==" -> Equals
                | "!=" -> NotEquals
                | _ -> failwith "Invalid operator. Should be '<', '<=', '>', '>=', '==', or '!='."

    type Condition = { Register : Register;
                       Operator : Operator;
                       Amount : int }
    type Instruction = { Register : Register;
                         AdjustmentType : AdjustmentType;
                         AdjustmentAmount : int;
                         Condition : Condition }

    let regex = new Regex("(?<reg>[a-z]+)\s(?<adj_type>inc|dec)\s(?<adj_amt>-?\d+)\sif\s(?<cond_reg>[a-z]+)\s(?<cond_op>\>|\<|\>=|\<=|==|!=)\s(?<cond_amt>-?\d+)")

    let parseInstruction input =
        let m = regex.Match(input)
        { Register = m.Groups.["reg"].Value;
          AdjustmentType = AdjustmentType.Parse m.Groups.["adj_type"].Value;
          AdjustmentAmount = int m.Groups.["adj_amt"].Value;
          Condition = { Register = m.Groups.["cond_reg"].Value;
                        Operator = Operator.Parse m.Groups.["cond_op"].Value;
                        Amount = int m.Groups.["cond_amt"].Value } }

    let parse input = Parser.splitLines input
                        |> Array.map parseInstruction

    let calculatePart1 instructions = 
       raise (NotImplementedException "Not implemented")