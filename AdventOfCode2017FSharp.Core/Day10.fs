namespace AdventOfCode2017FSharp.Core

module Day10 =

    let numbers = [206;63;255;131;65;80;238;157;254;24;133;2;16;0;1;3]

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

    let calculate (numbers : int[]) lengths =
        let (numbers', _, _) = 
            lengths |> List.fold 
                (fun (numbers', position, skipsize) length -> 
                    reverseLength numbers' length position skipsize) (numbers, 0, 0)
        numbers'.[0] * numbers'.[1]
