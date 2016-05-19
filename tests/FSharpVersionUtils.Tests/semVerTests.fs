namespace datNET

module SemVerTests =

  open FsUnit
  open datNET
  open datNET.SemVer
  open NUnit.Framework

  let private _emptySemVer =
    {
      Major = 0
      Minor = 0
      Patch = 0
      Pre   = None
      Meta  = None
    }

  (*
    Was hoping something like this would work, and the internet seems to think
    it does, but I haven't been able to get it to work so far.

    NUnit just keeps saying "Wrong number of arguments provided". Think it might
    be a bug in the version we're using.
  *)
  //let _ValidSemVers =
  //  [|
  //    [| "1", { _emptySemVer with Major = 1 } |]
  //    [| "1.", { _emptySemVer with Major = 1 } |]
  //  |]
  //
  //[<Test>]
  //[<TestCaseSource("_ValidSemVers")>]
  //let ``parse handles correctly-formatted input`` (input, semVer) =
  //  parse input |> should equal semVer

  let private _ParseSuccess input semver = parse input |> should equal semver

  [<Test>]
  let ``Parse 1`` () =
    _ParseSuccess "1" { _emptySemVer with Major = 1 }
  [<Test>]
  let ``Parse 1-pre`` () =
    _ParseSuccess "1-pre"
      { _emptySemVer with
          Major = 1
          Pre   = Some "pre"
      }
  [<Test>]
  let ``Parse 1+meta`` () =
    _ParseSuccess "1+meta"
      {
        _emptySemVer with
          Major = 1
          Meta  = Some "meta"
      }
  [<Test>]
  let ``Parse 1-pre+meta`` () =
    _ParseSuccess "1-pre+meta"
      { _emptySemVer with
          Major = 1
          Pre   = Some "pre"
          Meta  = Some "meta"
      }

  [<Test>]
  let ``parse 1.`` () =
    _ParseSuccess "1." { _emptySemVer with Major = 1 }
  [<Test>]
  let ``Parse 1.-pre`` () =
    _ParseSuccess "1.-pre"
      { _emptySemVer with
          Major = 1
          Pre   = Some "pre"
      }
  [<Test>]
  let ``Parse 1.+meta`` () =
    _ParseSuccess "1.+meta"
      {
        _emptySemVer with
          Major = 1
          Meta  = Some "meta"
      }
  [<Test>]
  let ``Parse 1.-pre+meta`` () =
    _ParseSuccess "1.-pre+meta"
      { _emptySemVer with
          Major = 1
          Pre   = Some "pre"
          Meta  = Some "meta"
      }


  [<Test>]
  let ``Parse 1.2`` () =
    _ParseSuccess "1.2"
      { _emptySemVer with
          Major = 1
          Minor = 2
      }
  [<Test>]
  let ``Parse 1.2-pre`` () =
    _ParseSuccess "1.2-pre"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Pre   = Some "pre"
      }
  [<Test>]
  let ``Parse 1.2+meta`` () =
    _ParseSuccess "1.2+meta"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Meta  = Some "meta"
      }
  [<Test>]
  let ``Parse 1.2-pre+meta`` () =
    _ParseSuccess "1.2-pre+meta"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Pre   = Some "pre"
          Meta  = Some "meta"
      }


  [<Test>]
  let ``Parse 1.2.`` () =
    _ParseSuccess "1.2."
      { _emptySemVer with
          Major = 1
          Minor = 2
      }
  [<Test>]
  let ``Parse 1.2.-pre`` () =
    _ParseSuccess "1.2.-pre"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Pre   = Some "pre"
      }
  [<Test>]
  let ``Parse 1.2.+meta`` () =
    _ParseSuccess "1.2.+meta"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Meta  = Some "meta"
      }
  [<Test>]
  let ``Parse 1.2.-pre+meta`` () =
    _ParseSuccess "1.2.-pre+meta"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Pre   = Some "pre"
          Meta  = Some "meta"
      }


  [<Test>]
  let ``Parse 1.2.3`` () =
    _ParseSuccess "1.2.3"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Patch = 3
      }
  [<Test>]
  let ``Parse 1.2.3-pre`` () =
    _ParseSuccess "1.2.3-pre"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Patch = 3
          Pre = Some "pre"
      }
  [<Test>]
  let ``Parse 1.2.3+meta`` () =
    _ParseSuccess "1.2.3+meta"
      { _emptySemVer with
          Major = 1
          Minor = 2
          Patch = 3
          Meta  = Some "meta"
      }

  let _InvalidSemVerStrings =
    [|
      (* TODO: Fix these cases *)
      // null
      // ""
      // "."
      // "garbage"
      "..."
      "1.2.3-pre1-pre2"
      "1.2.3+meta1+meta2"
      "1.2.3-pre+meta1+meta2"
      "1.2.3-pre1-pre2+meta"
      "1.2.3.-pre"
      "1.2.3.+meta"
      "1.2.3.-pre+meta"
      "1.2.3.4"
      "1.2.3.4-pre"
      "1.2.3.4+meta"
      "1.2.3.4-pre+meta"
    |]

  [<Test>]
  [<TestCaseSource("_InvalidSemVerStrings")>]
  let ``parse throws an exception for badly formatted input`` input =
    (fun () -> parse input |> ignore)
    |> should throw typeof<System.FormatException>

  [<Test>]
  [<TestCaseSource("_InvalidSemVerStrings")>]
  let ``tryParse returns None for badly formatted input`` input =
    tryParse input |> should equal None