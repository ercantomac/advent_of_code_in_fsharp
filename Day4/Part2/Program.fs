open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day4\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

let mutable resultSum = 0
let mutable associativeMap: Map<int, int list> = Map []

for lineIndex in 0 .. list.Length - 1 do
    let line = list[lineIndex]
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

    associativeMap <- (Map.add (lineIndex + 1) [ 1; numberOfMatches ] associativeMap)

for key in associativeMap.Keys do
    let cardQuantity = associativeMap[key][0]
    let numberOfMatches = associativeMap[key][1]

    for i in 1..numberOfMatches do
        if Map.containsKey (key + i) associativeMap then
            let tempValue = associativeMap[(key + i)]

            associativeMap <-
                (Map.add
                    (key + i)
                    [ (tempValue[0] + cardQuantity)
                      tempValue[1] ]
                    associativeMap)

for mapEntry in associativeMap do
    resultSum <- (resultSum + mapEntry.Value[0])

printfn $"{resultSum}"
