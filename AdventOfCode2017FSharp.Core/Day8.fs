namespace AdventOfCode2017FSharp.Core

module Day8 =

    open System.Text.RegularExpressions
    open System

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
        member this.Execute registers largest =
            match this.Condition.IsSatisfiedBy registers with
                | true ->
                    let currentValue = readRegister registers this.Register
                    let newValue = this.AdjustmentType currentValue this.AdjustmentAmount
                    let newRegisters = Map.add this.Register newValue registers
                    let newLargest = 
                        if Option.isNone largest || newValue > largest.Value then newValue 
                        else largest.Value
                    newRegisters, Some(newLargest)
                | false -> registers, largest
        static member Exec (registers, largest : int option) (instruction : Instruction) = instruction.Execute registers largest

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

    let calculate part instructions =
        let result = instructions |> Array.fold Instruction.Exec (Map.empty<string, int>, None)

        match part with
            | 1 -> result 
                    |> fst
                    |> Map.toSeq
                    |> Seq.sortByDescending (fun (_, value) -> value)
                    |> Seq.head
                    |> snd
            | 2 -> (snd result).Value
            | _ -> raise <| new ArgumentOutOfRangeException("part", "Should be '1' or '2'")