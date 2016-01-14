#r @"./._fake/packages/FAKE/tools/FakeLib.dll"

open Fake

Target "Hello" (fun _ -> 
  trace "Hello!"
)

Run "Hello"
