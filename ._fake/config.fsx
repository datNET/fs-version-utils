namespace FSharpUtils.Fake.Config

#r @"./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.EnvironmentHelper
open System.IO

module Common =
  let RootDir = Directory.GetCurrentDirectory()

module Source =
  open Common

  let SolutionFile = !! (Path.Combine(RootDir, "*.sln"))

module Build =
  let TestAssemblies = !! "tests/**/*.Tests.dll" -- "**/obj/**/*.Tests.dll"
  let DotNetVersion = "4.5"
  let MSBuildArtifacts = !! "**/bin/**.*" ++ "**/obj/**/*.*"

module Nuget =
  let ApiEnvVar = "DAT_NET_NUGET_API_KEY"
  let ApiKey = environVar ApiEnvVar
  let PackageDirName = "nupkgs"

module Release =
  let Items = !! "**/bin/Release/FSharpVersionUtils.dll"
  let Nuspec = "FSharpVersionUtils.nuspec"

  let Version = "0.1.0-alpha"
  let Project = "FSharp.Version.Utils"
  let Authors = [ "Andrew Seward"; "Mathew Glodack" ]
  let Description = "Semantic versioning utilities"
  let WorkingDir = "bin"
  let OutputPath = WorkingDir