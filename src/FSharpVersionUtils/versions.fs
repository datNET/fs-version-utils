namespace datNET

[<System.ObsoleteAttribute("Prefer datNET.SemVer module")>]
module Version =

    open System
    open SemVer

    let private _Coerce n = Math.Max(0, n)

    type VersionType =
      | Major
      | Minor
      | Patch

    // Borrowed from http://stackoverflow.com/a/2226375
    let (|InvariantEqual|_|) (str: string) arg =
      if String.Compare(str, arg, StringComparison.OrdinalIgnoreCase) = 0 then
        Some()
      else
        None

    let versionTypeFromString versionTypeStr =
      match versionTypeStr with
      | InvariantEqual "major" -> Major
      | InvariantEqual "minor" -> Minor
      | _ -> Patch

    let CoerceVersionToFourVersion (version: Version) =
      new Version(
        _Coerce version.Major,
        _Coerce version.Minor,
        _Coerce version.Build,
        _Coerce version.Revision)

    let CoerceStringToFourVersion version =
      CoerceVersionToFourVersion (Version.Parse(version))

    let CoerceVersionToSemVer (version: Version) =
      new Version(
        _Coerce version.Major,
        _Coerce version.Minor,
        _Coerce version.Build)

    let CoerceStringToSemVer version =
      CoerceVersionToSemVer (Version.Parse(version))

    let BuildSemVer major minor patch =
      new System.Version(
        _Coerce major,
        _Coerce minor,
        _Coerce patch)

    let private _map fn versionString =
      versionString
      |> SemVer.parse
      |> fn
      |> SemVer.stringify

    let ApplyMajor fn versionString =
      _map (SemVer.mapMajor fn) versionString

    let ApplyMinor fn versionString =
      _map (SemVer.mapMinor fn) versionString

    let ApplyPatch fn versionString =
      _map (SemVer.mapPatch fn) versionString

    let IncrMajor = _map SemVer.incrMajor
    let IncrMinor = _map SemVer.incrMinor
    let IncrPatch = _map SemVer.incrPatch

    let DecrMajor = _map SemVer.decrMajor
    let DecrMinor = _map SemVer.decrMinor
    let DecrPatch = _map SemVer.decrPatch
