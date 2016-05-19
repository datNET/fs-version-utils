module Tests

open NUnit.Framework
open FsUnit
open datNET
open datNET.Version

let private _version = new System.Version(1, 1, 1)

[<Test>]
let ``Should build correct semantic version using 1.1.1`` () =
    BuildSemVer 1 1 1 |> should equal _version

[<Test>]
let ``Should parse '1'`` () =
  let (expected: SemVer.SemVer) =
    {
      Major = 1
      Minor = 0
      Patch = 0
      Pre   = None
      Meta  = None
    }
  SemVer.parse "1" |> should equal expected

