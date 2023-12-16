﻿open System.IO

let lines = File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day3\Part1\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines
let mutable resultSum = 0

let getCharSafe (column: int) (row: int) : char =
    if column < 0
       || row < 0
       || row > (list.Length - 1)
       || column > (list[row].Length - 1) then
        '.'
    else
        list[row][column]

let getLeft (column: int) (row: int) : char = getCharSafe (column - 1) row

let getRight (column: int) (row: int) : char = getCharSafe (column + 1) row

let getTop (column: int) (row: int) : char = getCharSafe column (row - 1)

let getBottom (column: int) (row: int) : char = getCharSafe column (row + 1)

let getTopLeftDiagonal (column: int) (row: int) : char = getCharSafe (column - 1) (row - 1)

let getTopRightDiagonal (column: int) (row: int) : char = getCharSafe (column + 1) (row - 1)

let getBottomLeftDiagonal (column: int) (row: int) : char = getCharSafe (column - 1) (row + 1)

let getBottomRightDiagonal (column: int) (row: int) : char = getCharSafe (column + 1) (row + 1)

let hasAdjacentSymbol (column: int) (row: int) : bool =
    let adjacentChars =
        [ getLeft column row
          getRight column row
          getTop column row
          getBottom column row
          getTopLeftDiagonal column row
          getTopRightDiagonal column row
          getBottomLeftDiagonal column row
          getBottomRightDiagonal column row ]

    adjacentChars
    |> List.exists (fun c -> (System.Char.IsNumber c || c = '.') = false)


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
            let mutable adjacentSymbol = false

            for i in indicesOfDigits do
                if adjacentSymbol = false then
                    let a = hasAdjacentSymbol i lineIndex
                    if a then adjacentSymbol <- true

            printf $"{theNumber} = {adjacentSymbol} ,  "

            if adjacentSymbol then
                resultSum <- (resultSum + theNumber)

    printfn ""

printfn $"{resultSum}"
