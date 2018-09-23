namespace AdventOfCode2017FSharp.Core

module Day10 =

    let parsePart1 input = Parser.parseNumbers [|','|] input |> Array.head

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

    let calculate numbers lengths =
        let (numbers', _, _) = 
            lengths |> Array.fold 
                (fun (numbers', position, skipsize) length -> 
                    reverseLength numbers' length position skipsize) (numbers, 0, 0)
        numbers'.[0] * numbers'.[1]
