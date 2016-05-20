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
let ``IncrMajor increments the major version`` () =
  let initial = "1.0.0"
  let expected = "2.0.0"

  IncrMajor initial |> should equal expected

[<Test>]
let ``IncrMajor resets the minor and patch versions`` () =
  let initial = "1.2.3"
  let expected = "2.0.0"

  IncrMajor initial |> should equal expected

[<Test>]
let ``IncrMinor increments the minor version`` () =
  let initial = "1.0.0"
  let expected = "1.1.0"

  IncrMinor initial |> should equal expected

[<Test>]
let ``IncrMinor resets the patch version to zero`` () =
  let initial = "1.2.3"
  let expected = "1.3.0"

  IncrMinor initial |> should equal expected

[<Test>]
let ``IncrPatch increments the patch version`` () =
  let initial = "1.0.0"
  let expected = "1.0.1"

  IncrPatch initial |> should equal expected