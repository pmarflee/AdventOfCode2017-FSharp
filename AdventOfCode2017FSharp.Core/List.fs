namespace AdventOfCode2017FSharp.Core

module Array =

    // http://www.fssnip.net/2Z/title/Extensions-to-the-Fold-function

    //Executes a fold operation within a list returning a two-dimension tuple with
    //the first element being the result of the fold and the second being the count of the
    //processed elements.
    let public foldc fold first source  =
       source 
       |> Array.fold(fun (prev,count) c -> (fold prev c,count + 1)) (first,0)

    //Executes a fold operation within a list passing as parameter of the folder function 
    //the zero based index of each element.
    let public foldi fold first source  =
       source 
       |> Array.fold(fun (prev,i) c -> (fold i prev c,i + 1)) (first,0)
       |> fst

    //Executes a fold operation within a list passing as parameter of the folder function 
    //the zero based index of each element and returning a two-dimension tuple with
    //the first element being the result of the fold and the second being the count of the
    //processed elements.
    let public foldic fold first source  =
        source 
        |> Array.fold(fun (prev,i) c -> (fold i prev c, i + 1)) (first,0)