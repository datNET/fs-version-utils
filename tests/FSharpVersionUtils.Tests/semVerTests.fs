module SemVerTests

open FsUnit
open NUnit.Framework
open datNET

//[<Test>]
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

let ``Should parse '1.'`` () =
  let (expected: SemVer.SemVer) =
    {
      Major = 1
      Minor = 0
      Patch = 0
      Pre   = None
      Meta  = None
    }
  SemVer.parse "1." |> should equal expected