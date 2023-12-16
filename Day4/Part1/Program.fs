open System.IO

let lines = File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day4\Part1\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

let pow (number: int) (power: int) : int =
    if power < 0 then
        0
    elif power = 0 then
        1
    else
        let mutable result = 1

        for i in 1..power do
            result <- (result * number)

        result

let mutable resultSum = 0

for line in list do
    let lineSplitted = line.Split '|'
    let winningNumbersString = (lineSplitted[ 0 ].Split ':')[1]
    let myNumbersString = lineSplitted[1]

    let winningNumbers =
        winningNumbersString.Split ' '
        |> Array.filter (fun a -> a.Length > 0)

    let myNumbers =
        myNumbersString.Split ' '
        |> Array.filter (fun a -> a.Length > 0)

    let mutable numberOfMatches = 0

    for winningNumber in winningNumbers do
        if Array.contains winningNumber myNumbers then
            numberOfMatches <- (numberOfMatches + 1)

    resultSum <- (resultSum + (pow 2 (numberOfMatches - 1)))

printfn $"{resultSum}"
