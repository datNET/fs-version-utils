module Tests

open NUnit.Framework
open FsUnit
open datNET.Version

let private _version = new System.Version(1, 1, 1)

[<Test>]
let ``Should build correct semantic version using 1.1.1`` () =
    BuildSemVer 1 1 1 |> should equal _version
