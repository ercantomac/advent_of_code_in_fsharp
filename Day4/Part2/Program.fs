open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day4\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

let mutable associativeMap: int list = []

let getNumberOfMatches (line: string) =
    let lineSplitted = line.Split '|'
    let winningNumbersString = (lineSplitted[ 0 ].Split ':')[1]
    let myNumbersString = lineSplitted[1]

    let winningNumbers =
        winningNumbersString.Split ' '
        |> Array.filter (fun a -> a.Length > 0)

    let myNumbers =
        myNumbersString.Split ' '
        |> Array.filter (fun a -> a.Length > 0)

    let numberOfMatches =
        winningNumbers
        |> Array.filter (fun number -> Array.contains number myNumbers)
        |> Array.length

    numberOfMatches

let myFunction (index: int) : int =
    let cardQuantity =
        [ 0 .. (index - 1) ]
        |> List.fold
            (fun accumulation j ->
                if (getNumberOfMatches list[j]) >= (index - j) then
                    accumulation + associativeMap[j]
                else
                    accumulation)
            0

    associativeMap <-
        (associativeMap
         |> List.insertAt index (cardQuantity + 1))

    (cardQuantity + 1)

let answer =
    list
    |> List.indexed
    |> List.fold (fun acc (index, line) -> acc + myFunction index) 0

printfn $"{answer}"

// ANSWER: 7013204