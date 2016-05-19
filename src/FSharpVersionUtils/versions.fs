namespace datNET

module Version =

    open System

    let private _Coerce n = Math.Max(0, n)
    let private _Incr = (+) 1
    let private _Decr = (-) 1

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

    let ApplyMajor fn versionString =
      let current = Version.Parse(versionString)
      let newMajor = fn (_Coerce current.Major)

      (BuildSemVer newMajor 0 0).ToString()

    let ApplyMinor fn versionString =
      let current = Version.Parse(versionString)
      let newMinor = fn (_Coerce current.Minor)

      (BuildSemVer current.Major newMinor 0).ToString()

    let ApplyPatch fn versionString =
      let current = Version.Parse(versionString)
      let newPatch = fn (_Coerce current.Build)

      (BuildSemVer current.Major current.Minor newPatch).ToString()


    let IncrMajor = ApplyMajor _Incr
    let IncrMinor = ApplyMinor _Incr
    let IncrPatch = ApplyPatch _Incr

    let DecrMajor = ApplyMajor _Decr
    let DecrMinor = ApplyMinor _Decr
    let DecrPatch = ApplyPatch _Decr
