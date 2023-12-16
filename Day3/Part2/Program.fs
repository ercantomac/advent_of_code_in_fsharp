open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day3\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines
let mutable resultSum = 0

let coordinatesIfGear (column: int) (row: int) : (int * int) =
    if column < 0
       || row < 0
       || row > (list.Length - 1)
       || column > (list[row].Length - 1)
       || list[row][column] <> '*' then
        (-1, -1)
    else
        (row, column)

let getLeft (column: int) (row: int) : (int * int) = coordinatesIfGear (column - 1) row

let getRight (column: int) (row: int) : (int * int) = coordinatesIfGear (column + 1) row

let getTop (column: int) (row: int) : (int * int) = coordinatesIfGear column (row - 1)

let getBottom (column: int) (row: int) : (int * int) = coordinatesIfGear column (row + 1)

let getTopLeftDiagonal (column: int) (row: int) : (int * int) =
    coordinatesIfGear (column - 1) (row - 1)

let getTopRightDiagonal (column: int) (row: int) : (int * int) =
    coordinatesIfGear (column + 1) (row - 1)

let getBottomLeftDiagonal (column: int) (row: int) : (int * int) =
    coordinatesIfGear (column - 1) (row + 1)

let getBottomRightDiagonal (column: int) (row: int) : (int * int) =
    coordinatesIfGear (column + 1) (row + 1)

let neighbourGearCoordinates (column: int) (row: int) : (int * int) =
    let adjacentChars =
        [ getLeft column row
          getRight column row
          getTop column row
          getBottom column row
          getTopLeftDiagonal column row
          getTopRightDiagonal column row
          getBottomLeftDiagonal column row
          getBottomRightDiagonal column row ]

    let mutable result = (-1, -1)

    for i in adjacentChars do
        if i <> (-1, -1) then result <- i

    result


let mutable associativeMap: Map<(int * int), int list> = Map []

for lineIndex in 0 .. list.Length - 1 do
    let line = list[lineIndex]
    printfn $"{line}"
    let mutable index = 0

    while index < line.Length do
        let mutable theNumber = -1
        let mutable indicesOfDigits = []
        let mutable isNumber = System.Char.IsNumber(line[index])

        if isNumber = false then
            index <- (index + 1)
        else
            theNumber <- 0

        while index < line.Length && isNumber do
            isNumber <- System.Char.IsNumber(line[index])

            if isNumber then
                theNumber <- (theNumber * 10)
                let digit = System.Char.GetNumericValue(line[index]) |> int
                theNumber <- (theNumber + digit)
                indicesOfDigits <- (indicesOfDigits |> List.append [ index ])

            index <- (index + 1)

        if theNumber <> -1 then
            // CHECK IF THE NUMBER IS ADJACENT TO A GEAR SYMBOL
            let mutable adjacentSymbol = (-1, -1)

            for i in indicesOfDigits do
                if adjacentSymbol = (-1, -1) then
                    let a = neighbourGearCoordinates i lineIndex

                    if a <> (-1, -1) then
                        adjacentSymbol <- a

            printf $"{theNumber} = {adjacentSymbol} ,  "

            // IF THE NUMBER IS ADJACENT TO A GEAR SYMBOL, ADD IT TO THE MAP
            if adjacentSymbol <> (-1, -1) then
                let mutable tempList = []

                if Map.containsKey adjacentSymbol associativeMap then
                    tempList <- associativeMap[adjacentSymbol]

                tempList <- (tempList |> List.append [ theNumber ])

                associativeMap <- (Map.add adjacentSymbol tempList associativeMap)

    printfn ""

for mapEntry in associativeMap do
    printfn $"{mapEntry.Key}: {mapEntry.Value}"

    if mapEntry.Value.Length = 2 then
        resultSum <-
            (resultSum
             + (mapEntry.Value[0] * mapEntry.Value[1]))

printfn $"{resultSum}"
