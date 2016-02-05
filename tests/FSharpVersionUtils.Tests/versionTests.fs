module Tests

open NUnit.Framework
open FsUnit

[<Test>]
let ``Assert that true is equal to True`` () =
    true |> should be True