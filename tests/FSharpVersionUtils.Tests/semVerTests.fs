namespace datNET

module SemVerTests =

  open FsUnit
  open datNET
  open NUnit.Framework

  [<Test>]
  let ``Parse 1`` () =
    let (expected: SemVer.SemVer) =
      {
        Major = 1
        Minor = 0
        Patch = 0
        Pre   = None
        Meta  = None
      }
    SemVer.parse "1" |> should equal expected

  [<Test>]
  let ``parse 1.`` () =
    let (expected: SemVer.SemVer) =
      {
        Major = 1
        Minor = 0
        Patch = 0
        Pre   = None
        Meta  = None
      }
    SemVer.parse "1." |> should equal expected

  [<Test>]
  let ``Parse 1.1`` () =
    let (expected: SemVer.SemVer) =
      {
        Major = 1
        Minor = 1
        Patch = 0
        Pre   = None
        Meta  = None
      }

    SemVer.parse "1.1" |> should equal expected