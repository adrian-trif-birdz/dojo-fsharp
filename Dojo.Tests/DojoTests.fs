module DojoTests

open NUnit.Framework
open FsUnit
open Dojo

[<Test>]
let ``given no order on the market match returns no match``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    matchTrades [] trade
    |> should equal []

[<Test>]
let ``given one trade with a match the match should be returned``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [{  
            OrderId = "1"
            Qty = 100
            FromCurrency = "USD"
            ToCurrency = "EUR"
            Ratio = 0.84m
        }]
    matchTrades markettrades trade
    |> should equal 
        [{  
            OrderId = "1"
            Qty = 100
            FromCurrency = "USD"
            ToCurrency = "EUR"
            Ratio = 0.84m
        }]

[<Test>]
let ``given some trades  with a match the match should be returned``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.84m
            }
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.80m
            }
        ]
    matchTrades markettrades trade
    |> should equal 
        [{  
            OrderId = "1"
            Qty = 100
            FromCurrency = "USD"
            ToCurrency = "EUR"
            Ratio = 0.84m
        }]

[<Test>]
let ``given some other trades  with a match the match should be returned``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.80m
            }
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.84m
            }
        ]
    matchTrades markettrades trade
    |> should equal 
        [{  
            OrderId = "1"
            Qty = 100
            FromCurrency = "USD"
            ToCurrency = "EUR"
            Ratio = 0.84m
        }]


[<Test>]
let ``given some trades  with NO match the matchTrades returns empty``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.80m
            }
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.83m
            }
        ]
    matchTrades markettrades trade
    |> should equal []

[<Test>]
let ``given trades in different currences with NO match the matchTrades returns empty``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.80m
            }
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "GBP"
                Ratio = 0.88m
            }
        ]
    matchTrades markettrades trade
    |> should equal []

[<Test>]
let ``given trades in different currences with one match the matchTrades returns the match``() =
    let trade =
        {  
            OrderId = "1"
            Qty = 100
            FromCurrency = "EUR"
            ToCurrency = "USD"
            Ratio = 1.2m
        }
    let markettrades =
        [
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "EUR"
                Ratio = 0.80m
            }
            {  
                OrderId = "1"
                Qty = 100
                FromCurrency = "USD"
                ToCurrency = "GBP"
                Ratio = 0.88m
            }
        ]
    matchTrades markettrades trade
    |> should equal []