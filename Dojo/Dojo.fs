module Dojo

type Trade =
    {
        OrderId: string
        Qty : int
        FromCurrency: string
        ToCurrency: string
        Ratio: decimal
    }


let matchTrades (marketState: Trade list) tradeToMatch =
    let currencyMatch t1 t2 =
        t1.FromCurrency = t2.ToCurrency 
        && t1.ToCurrency = t2.FromCurrency

    match marketState with
    | [] -> []
    | _ ->
        let result =
            marketState
            |> List.filter (currencyMatch tradeToMatch)
            |> List.sortByDescending (fun t -> t.Ratio)
            |> List.head
        if (result.Ratio > 1m/tradeToMatch.Ratio)
        then
            [result]
        else
            []
