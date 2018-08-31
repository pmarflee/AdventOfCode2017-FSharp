namespace AdventOfCode2017FSharp.Core

module Day8 =

    open System.Text.RegularExpressions

    type Register = string

    type AdjustmentType = (int -> int -> int)
    type Operator = (int -> int -> bool)

    let private getAdjustmentType = function
        | "inc" -> (+)
        | "dec" -> (-)
        | _ -> failwith "Invalid adjustment type. Should be 'inc' or 'dec'." 

    let private readRegister registers key =
        match registers |> Map.tryFind key with
            | Some(value) -> value
            | None -> 0

    let private getOperator = function
        | ">" -> (>)
        | ">=" -> (>=)
        | "<" -> (<)
        | "<=" -> (<=)
        | "==" -> (=)
        | "!=" -> (<>)
        | _ -> failwith "Invalid operator. Should be '<', '<=', '>', '>=', '==', or '!='."

    type Condition = { Register : Register;
                       Operator : Operator;
                       Amount : int } with
        member this.IsSatisfiedBy registers =
            this.Operator (readRegister registers this.Register) this.Amount

    type Instruction = { Register : Register;
                         AdjustmentType : AdjustmentType;
                         AdjustmentAmount : int;
                         Condition : Condition } with
        member this.Execute registers =
            match this.Condition.IsSatisfiedBy registers with
                | true ->
                    let currentValue = readRegister registers this.Register
                    let newValue = this.AdjustmentType currentValue this.AdjustmentAmount
                    Map.add this.Register newValue registers
                | false -> registers
        static member Exec registers (instruction : Instruction) = instruction.Execute registers

    let private regex = new Regex("(?<reg>[a-z]+)\s(?<adj_type>inc|dec)\s(?<adj_amt>-?\d+)\sif\s(?<cond_reg>[a-z]+)\s(?<cond_op>\>|\<|\>=|\<=|==|!=)\s(?<cond_amt>-?\d+)")

    let parseInstruction input =
        let m = regex.Match(input)
        { Register = m.Groups.["reg"].Value;
          AdjustmentType = getAdjustmentType m.Groups.["adj_type"].Value;
          AdjustmentAmount = int m.Groups.["adj_amt"].Value;
          Condition = { Register = m.Groups.["cond_reg"].Value;
                        Operator = getOperator m.Groups.["cond_op"].Value;
                        Amount = int m.Groups.["cond_amt"].Value } }

    let parse input = Parser.splitLines input
                        |> Array.map parseInstruction

    let calculatePart1 instructions = 
        instructions
            |> Array.fold Instruction.Exec Map.empty<string, int>
            |> Map.toSeq
            |> Seq.sortByDescending (fun (_, value) -> value)
            |> Seq.head
            |> snd