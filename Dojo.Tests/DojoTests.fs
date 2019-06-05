module DojoTests

open NUnit.Framework
open FsUnit
open Dojo

[<Test>]
let ``first test``() =
    add 1 2
    |> should equal 3

