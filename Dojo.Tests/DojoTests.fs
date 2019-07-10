module DojoTests

open NUnit.Framework
open FsUnit
open Dojo

[<Test>]
let ``Given no market state findTradingLoop returns no result``() =
    findTradingLoop [] ""
    |> should equal []

[<Test>]
let ``Given market state with no loops findTradingLoop returns no result``() =
    let marketState = 
        [
            {Index=0; StartingQty=10m; StartingCurrency = "USD"; EndQty = 11m; EndingCurrency = "USD"}
        ]
    findTradingLoop marketState ""
    |> should equal []

[<Test>]
let ``Given market state with a loop findTradingLoop returns a result``() =
    let marketState = 
        [
            {Index=1; StartingQty=80m; StartingCurrency = "EUR"; EndQty = 100m; EndingCurrency = "USD"}
            {Index=2; StartingQty=105m; StartingCurrency = "USD"; EndQty = 90m; EndingCurrency = "GBP"}
            {Index=3; StartingQty=90m; StartingCurrency = "GBP"; EndQty = 80m; EndingCurrency = "EUR"}
        ]
    findTradingLoop marketState "USD"
    |> should equal [1; 3; 2]

[<Test>]
let ``Given market state with a loop and a different starting currency findTradingLoop returns a different result``() =
    let marketState = 
        [
            {Index=1; StartingQty=80m; StartingCurrency = "EUR"; EndQty = 100m; EndingCurrency = "USD"}
            {Index=2; StartingQty=105m; StartingCurrency = "USD"; EndQty = 90m; EndingCurrency = "GBP"}
            {Index=3; StartingQty=90m; StartingCurrency = "GBP"; EndQty = 80m; EndingCurrency = "EUR"}
        ]
    findTradingLoop marketState "GBP"
    |> should equal [2; 1; 3]