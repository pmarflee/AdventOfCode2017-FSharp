namespace AdventOfCode2017FSharp.Core

module Day10 =

    let parsePart1 input = Parser.parseNumbers [|','|] input |> Array.head

    let standardLengthSuffixValues = [|17; 31; 73; 47; 23|]

    let parsePart2 (input : string) = input |> Seq.toArray |> Array.map int
    
    let addSecret input = Array.concat [ input; standardLengthSuffixValues ]

    let reverseLength numbers length position skipsize =
        let countOfNumbers = Array.length numbers
        let rec reverse i = 
            let j = position + length - (i - position) - 1
            if i >= j then 
                (numbers, (position + length + skipsize) % countOfNumbers, skipsize + 1)
            else
                let index_i = i % countOfNumbers
                let index_j = j % countOfNumbers
                let tmp = numbers.[index_i]
                numbers.[index_i] <- numbers.[index_j]
                numbers.[index_j] <- tmp
                reverse (i + 1)
        reverse position

    let calculate numbers lengths rounds =
        let calculate' numbers' position' skipsize' =
            lengths |> Array.fold 
                (fun (n, p, s) length -> 
                    reverseLength n length p s) (numbers', position', skipsize')

        let rec calculateRound numbers' position' skipsize' round =
            if round > rounds then numbers'
            else
                let (n, p, s) = calculate' numbers' position' skipsize'
                calculateRound n p s (round + 1)

        calculateRound numbers 0 0 1

    let calculatePart1 numbers lengths =
        let numbers' = calculate numbers lengths 1
        numbers'.[0] * numbers'.[1]

    let toDenseHash sparseHash =
       sparseHash 
           |> Seq.splitInto ((Array.length sparseHash) / 16)
           |> Seq.map (Array.reduce (^^^))
           |> Seq.toArray

    let toHex denseHash =
        denseHash |> Seq.map (sprintf "%02x") |> String.concat ""

    let calculatePart2 numbers lengths = 
        calculate numbers lengths 64 |> toDenseHash |> toHex
        