open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day3\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

(*
// IMPERATIVE APPROACH

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

let mutable resultSum = 0

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
            let mutable coordinatesOfGear = (-1, -1)

            for i in indicesOfDigits do
                if coordinatesOfGear = (-1, -1) then
                    let a = neighbourGearCoordinates i lineIndex

                    if a <> (-1, -1) then
                        coordinatesOfGear <- a

            printf $"{theNumber} = {coordinatesOfGear} ,  "

            // IF THE NUMBER IS ADJACENT TO A GEAR SYMBOL, ADD IT TO THE MAP
            if coordinatesOfGear <> (-1, -1) then
                let mutable tempList = []

                if Map.containsKey coordinatesOfGear associativeMap then
                    tempList <- associativeMap[coordinatesOfGear]

                tempList <- (tempList |> List.append [ theNumber ])

                associativeMap <- (Map.add coordinatesOfGear tempList associativeMap)

    printfn ""

for mapEntry in associativeMap do
    if mapEntry.Value.Length = 2 then
        printfn $"{mapEntry.Key}: {mapEntry.Value}"
        resultSum <-
            (resultSum
             + (mapEntry.Value[0] * mapEntry.Value[1]))

printfn $"{resultSum}"
 *)

// FUNCTIONAL APPROACH

let myFunction (exclude: string list) (s: string) (baseList: int list) =
    let removeFirstIndices =
        exclude
        |> List.fold (fun acc elem -> acc + elem.Length) 0

    let newList = baseList |> List.removeManyAt 0 removeFirstIndices
    newList.[0 .. (s.Length - 1)]

let getNumbersAndTheirIndices (lineIndex: int, line: string) =
    let charList = line.ToCharArray() |> Seq.toList |> List.indexed

    let charListIndices =
        charList
        |> List.filter (fun (index, c) -> System.Char.IsNumber(c))
        |> List.map (fun (index, c) -> index)

    let theNumbers =
        line.Split(
            [| "."
               "*"
               "+"
               "="
               "%"
               "-"
               "/"
               "_"
               "$"
               "@"
               "&"
               "#" |],
            System.StringSplitOptions.RemoveEmptyEntries
        )
        |> Seq.toList

    let numbersAndTheirIndices =
        theNumbers
        |> List.indexed
        |> List.map (fun (index, elem) -> (elem, myFunction theNumbers[0 .. (index - 1)] elem charListIndices))

    numbersAndTheirIndices

let getGearsAndTheirIndices (lineIndex: int, line: string) =
    let charList = line.ToCharArray() |> Seq.toList |> List.indexed

    let charListIndices =
        charList
        |> List.filter (fun (index, c) -> c = '*')
        |> List.map (fun (index, c) -> (lineIndex, index))

    charListIndices

let numbersAndTheirIndices =
    list
    |> List.indexed
    |> List.map getNumbersAndTheirIndices

let gearsAndTheirIndices =
    list
    |> List.indexed
    |> List.map getGearsAndTheirIndices

let isNumber (column: int) (row: int) =
    if not (row >= 0 && row < numbersAndTheirIndices.Length) then
        -1
    else
        let tempList =
            numbersAndTheirIndices[row]
            |> List.filter (fun (number, indices) -> indices |> List.contains column)

        if not tempList.IsEmpty then
            let (number, indices) = tempList |> List.head
            number |> int
        else
            -1

let getLeft (column: int) (row: int) : int = isNumber (column - 1) row

let getRight (column: int) (row: int) : int = isNumber (column + 1) row

let getTop (column: int) (row: int) : int = isNumber column (row - 1)

let getBottom (column: int) (row: int) : int = isNumber column (row + 1)

let getTopLeftDiagonal (column: int) (row: int) : int = isNumber (column - 1) (row - 1)

let getTopRightDiagonal (column: int) (row: int) : int = isNumber (column + 1) (row - 1)

let getBottomLeftDiagonal (column: int) (row: int) : int = isNumber (column - 1) (row + 1)

let getBottomRightDiagonal (column: int) (row: int) : int = isNumber (column + 1) (row + 1)

let neighbourNumbers (column: int) (row: int) : int =
    let adjacentChars =
        [ getLeft column row
          getRight column row
          getTop column row
          getBottom column row
          getTopLeftDiagonal column row
          getTopRightDiagonal column row
          getBottomLeftDiagonal column row
          getBottomRightDiagonal column row ]

    let resultList =
        adjacentChars
        |> List.filter (fun c -> c <> -1)
        |> List.distinct

    if resultList.Length = 2 then
        printfn $"{(row, column)}: {resultList}"
        resultList[0] * resultList[1]
    else
        0

let answer =
    gearsAndTheirIndices
    |> List.map (fun gears ->
        (gears
         |> List.map (fun (row, column) -> neighbourNumbers column row)
         |> List.sum))
    |> List.sum

printfn $"{answer}"

// ANSWER: 67779080