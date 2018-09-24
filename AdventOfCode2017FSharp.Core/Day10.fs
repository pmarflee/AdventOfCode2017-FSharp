namespace AdventOfCode2017FSharp.Core

module Day10 =
    open Day3

    let parsePart1 input = Parser.parseNumbers [|','|] input |> Array.head

    let standardLengthSuffixValues = [|17; 31; 73; 47; 23|]

    let parsePart2 (input : string) = input |> Seq.toArray |> Array.map int
    
    let addSecret input = Array.concat [ input; standardLengthSuffixValues ]

    let reverseLength (numbers : int[]) length position skipsize =
        let rec reverse i = 
            let j = position + length - (i - position) - 1
            if i >= j then 
                (numbers, (position + length + skipsize) % numbers.Length, skipsize + 1)
            else
                let index_i = i % numbers.Length
                let index_j = j % numbers.Length
                let tmp = numbers.[index_i]
                numbers.[index_i] <- numbers.[index_j]
                numbers.[index_j] <- tmp
                reverse (i + 1)
        reverse position

    let calculate numbers lengths position skipsize rounds =
        let calculate' numbers' position' skipsize' =
            lengths |> Array.fold 
                (fun (n, p, s) length -> 
                    reverseLength n length p s) (numbers', position', skipsize')

        let rec calculateRound numbers' position' skipsize' round =
            if round > rounds then numbers'
            else
                let (n, p, s) = calculate' numbers' position' skipsize'
                calculateRound n p s (round + 1)

        calculateRound numbers position skipsize 1

    let calculatePart1 numbers lengths =
        let numbers' = calculate numbers lengths 0 0 1
        numbers'.[0] * numbers'.[1]

    let toDenseHash (sparseHash : int[]) =
       sparseHash 
           |> Seq.splitInto (sparseHash.Length / 16)
           |> Seq.map (fun chunk -> chunk |> Array.reduce (fun acc n -> acc ^^^ n))
           |> Seq.toArray

    let toHex (denseHash : int[]) =
        denseHash |> Seq.map (sprintf "%02x") |> String.concat ""

    let calculatePart2 numbers lengths = 
        calculate numbers lengths 0 0 64 |> toDenseHash |> toHex
        