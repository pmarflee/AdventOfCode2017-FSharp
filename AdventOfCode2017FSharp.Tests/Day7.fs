namespace Day7
    module Day7 =

        open Xunit
        open FsUnit.Xunit
        open AdventOfCode2017FSharp.Core.Day7

        type Tests () =

            static member LineData
                with get() =
                    let toMap codes = codes |> List.map (fun c -> (c, c)) |> Map.ofList
                    [ [| "pbga (66)" :> obj; { Name = "pbga"; Weight = 66; Dependents = Map.empty } :> obj |];
                      [| "xhth (57)"; { Name = "xhth"; Weight = 57; Dependents = Map.empty } |];
                      [| "ebii (61)"; { Name = "ebii"; Weight = 61; Dependents = Map.empty } |];
                      [| "havc (66)"; { Name = "havc"; Weight = 66; Dependents = Map.empty } |];
                      [| "ktlj (57)"; { Name = "ktlj"; Weight = 57; Dependents = Map.empty } |];
                      [| "fwft (72) -> ktlj, cntj, xhth"; { Name = "fwft"; Weight = 72; Dependents = toMap [ "ktlj"; "cntj"; "xhth" ] } |];
                      [| "qoyq (66)"; { Name = "qoyq"; Weight = 66; Dependents = Map.empty } |];
                      [| "padx (45) -> pbga, havc, qoyq"; { Name = "padx"; Weight = 45; Dependents = toMap [ "pbga"; "havc"; "qoyq" ] } |];
                      [| "tknk (41) -> ugml, padx, fwft"; { Name = "tknk"; Weight = 41; Dependents = toMap [ "ugml"; "padx"; "fwft" ] } |];
                      [| "jptl (61)"; { Name = "jptl"; Weight = 61; Dependents = Map.empty } |];
                      [| "ugml (68) -> gyxo, ebii, jptl"; { Name = "ugml"; Weight = 68; Dependents = toMap [ "gyxo"; "ebii"; "jptl" ] } |];
                      [| "gyxo (61)"; { Name = "gyxo"; Weight = 61; Dependents = Map.empty } |];
                      [| "cntj (57)"; { Name = "cntj"; Weight = 57; Dependents = Map.empty } |] ]

            [<Theory>]
            [<MemberData("LineData")>]
            member verify.``Line can be parsed`` (input : string, expected : Line) =
                parseLine input |> should equal expected

            [<Fact>]
            member verify.``Find bottom program`` () =
                let result = Tests.LineData 
                                |> List.map (fun line -> string (Array.head line))
                                |> String.concat "\r\n"
                                |> parse
                                |> findBottomProgram
                result.Name |> should equal "tknk"
