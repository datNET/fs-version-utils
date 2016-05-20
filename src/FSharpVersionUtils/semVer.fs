namespace datNET

module SemVer =

  type SemVer =
    {
      Major : int
      Minor : int
      Patch : int
      Pre   : string option
      Meta  : string option
    }

  let stringify semVer =
    let baseStr =
      [| semVer.Major ; semVer.Minor ; semVer.Patch |]
      |> Seq.map (sprintf "%i")
      |> String.concat "."

    let pre =
      match semVer.Pre with
      | Some v -> sprintf "-%s" v
      | None   -> ""

    let meta =
      match semVer.Meta with
      | Some v -> sprintf "+%s" v
      | None   -> ""

    String.concat "" [| baseStr ; pre ; meta |]

  let private _parseFail str =
    let message = sprintf "Invalid semantic version string: %s" str
    raise (new System.FormatException(message))

  (* The following might be better done as regexes *)
  let private _headAndMeta (str: string) =
    match str.Split [| '+' |] with
    | [| head; meta |] -> head, Some(meta)
    | [| head; |]      -> head, None
    | _                -> _parseFail str

  let private _versionPreAndMeta (head: string, meta: string option) =
    match head.Split [| '-' |] with
    | [| ver; pre |] -> ver, Some(pre), meta
    | [| ver |]      -> ver, None,      meta
    | _              -> _parseFail head

  let private _majorMinorPatch (str: string) =
    let toInt i =
      match System.Int32.TryParse i with
      | (true, v) -> v
      | (false, _) -> 0

    let split =
      str.Split [| '.' |]
      |> Array.map toInt

    match split with
    | [| maj; min; pat; _|] -> (maj, min, pat)
    | [| maj; min; pat |]   -> (maj, min, pat)
    | [| maj; min; |]       -> (maj, min, 0)
    | [| maj |]             -> (maj, 0,   0)
    | _                     -> _parseFail str

  let private _splitParts (str: string) =
    if str = null then raise (new System.ArgumentNullException())
    else
      str
      |> _headAndMeta
      |> _versionPreAndMeta

  let parseMajorMinorPatch str =
    let baseStr, _, _ = _splitParts str
    _majorMinorPatch baseStr

  let parseMajor str =
    let major, _, _ = parseMajorMinorPatch str
    major

  let parseMinor str =
    let _, minor, _ = parseMajorMinorPatch str
    minor

  let parsePatch str =
    let _, _, patch = parseMajorMinorPatch str
    patch

  let parsePre str =
    let _, pre, _ = _splitParts str
    pre

  let parseMeta str =
    let _, _, meta = _splitParts str
    meta

  let parse str =
    let baseStr, pre, meta = _splitParts str
    let major, minor, patch = _majorMinorPatch baseStr

    {
      Major = major
      Minor = minor
      Patch = patch
      Pre = pre
      Meta = meta
    }

  let tryParse str =
    try
      Some (parse str)
    with
    | _ -> None

  let toSystemVersion revision semVer =
    let
      {
        Major = major
        Minor = minor
        Patch = patch
      } = semVer
    let rev =
      match revision with
      | Some n -> n
      | None   -> 0

    (new System.Version(major, minor, patch, rev))

  let mapMajor fn semVer =
    { semVer with
        Major = fn(semVer.Major)
        Minor = 0
        Patch = 0
    }

  let mapMinor fn semVer =
    { semVer with
        Minor = fn(semVer.Minor)
        Patch = 0
    }

  let mapPatch fn semVer =
    { semVer with Patch = fn(semVer.Patch) }

  let mapPre fn semVer =
    { semVer with Pre = fn(semVer.Pre) }

  let mapMeta fn semVer =
    { semVer with Meta = fn(semVer.Meta) }

  let private _incr = (+) 1
  let private _decr = (-) 1

  let incrMajor = mapMajor _incr
  let incrMinor = mapMinor _incr
  let incrPatch = mapPatch _incr

  let decrMajor = mapMajor _decr
  let decrMinor = mapMajor _decr
  let decrPatch = mapMajor _decr