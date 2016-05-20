namespace datNET

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

    let private _Map fn versionString =
      versionString
      |> SemVer.parse
      |> fn
      |> SemVer.stringify

    let ApplyMajor fn versionString =
      _Map (SemVer.mapMajor fn) versionString

    let ApplyMinor fn versionString =
      _Map (SemVer.mapMinor fn) versionString

    let ApplyPatch fn versionString =
      _Map (SemVer.mapPatch fn) versionString

    let IncrMajor = _Map SemVer.IncrMajor
    let IncrMinor = _Map SemVer.IncrMinor
    let IncrPatch = _Map SemVer.IncrPatch

    let DecrMajor = _Map SemVer.DecrMajor
    let DecrMinor = _Map SemVer.DecrMinor
    let DecrPatch = _Map SemVer.DecrPatch
