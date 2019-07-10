module Dojo

type Trade = 
    {
        Index: int
        StartingQty: decimal
        StartingCurrency: string
        EndQty: decimal
        EndingCurrency: string
    }

let findTradingLoop marketState startCurrency =
    let rec traverseMarket marketState currentCurrency stopCurrency acc =
        let getFirstMatch marketState currency =
            marketState
            |> Seq.filter (fun trade -> trade.EndingCurrency = currency)
            |> (fun x ->
                if Seq.isEmpty x
                then Option.None
                else Some(Seq.head x))

        let trade = getFirstMatch marketState currentCurrency
        match trade with
        | None -> []
        | Some t -> if t.StartingCurrency = stopCurrency
                    then List.append acc [t.Index] // to add the t.Index 
                    else traverseMarket marketState t.StartingCurrency stopCurrency (List.append acc [t.Index])
        
    traverseMarket marketState startCurrency startCurrency []

    
