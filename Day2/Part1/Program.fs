open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day2\Part1\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

(*
// IMPERATIVE APPROACH

let mutable result = 0

for i in 0 .. list.Length - 1 do
    let line = list[i]
    printfn $"{line}"

    let j = line.IndexOf(':')

    let gameNo = int ((line[ .. (j - 1) ].Split ' ')[1])
    printfn $"{gameNo}"
    let gameSets = line[ (j + 2) .. ].Split ';'
    let mutable gameFailed = false

    for set in gameSets do
        if gameFailed = false then
            let setTrimmed = set |> String.filter (fun c -> c <> ' ')
            printfn $"{setTrimmed}"
            let individualCubes = setTrimmed.Split ','

            for cube in individualCubes do
                if gameFailed = false then
                    //printfn $"{cube}"
                    let mutable breakLoop = false
                    let mutable color = ""
                    let mutable quantity = "-1"

                    for k in 0 .. cube.Length - 1 do
                        if breakLoop = false then
                            let isLetter = (System.Char.IsNumber(cube[k])) = false

                            if isLetter then
                                color <- cube[k..]
                                quantity <- cube[.. (k - 1)]
                                breakLoop <- true

                    let quantityInt = int quantity

                    if color = "red" && quantityInt > 12 then
                        printfn "red failed"
                        gameFailed <- true

                    elif color = "green" && quantityInt > 13 then
                        printfn "green failed"
                        gameFailed <- true

                    elif color = "blue" && quantityInt > 14 then
                        printfn "blue failed"
                        gameFailed <- true

    if gameFailed = false then
        result <- (result + gameNo)

printfn $"{result}"
 *)

// FUNCTIONAL APPROACH

let testCube cube =
    let color =
        cube
        |> String.filter (fun c -> not (System.Char.IsNumber(c)))

    let quantity =
        cube
        |> String.filter (fun c -> System.Char.IsNumber(c))
        |> int

    if color = "red" && quantity > 12 then
        printfn "red failed"
        true

    elif color = "green" && quantity > 13 then
        printfn "green failed"
        true

    elif color = "blue" && quantity > 14 then
        printfn "blue failed"
        true

    else
        false

let testSet set =
    let setTrimmed = set |> String.filter (fun c -> c <> ' ')
    printfn $"{setTrimmed}"
    let individualCubes = setTrimmed.Split ','

    individualCubes
    |> Array.exists (fun cube -> testCube cube)

let mainFunction (accumulation: int) (line: string) =
    printfn $"{line}"

    let j = line.IndexOf(':')

    let gameNo = int ((line[ .. (j - 1) ].Split ' ')[1])
    printfn $"{gameNo}"
    let gameSets = line[ (j + 2) .. ].Split ';'

    let gameFailed = gameSets |> Array.exists (fun set -> testSet set)

    if not gameFailed then
        accumulation + gameNo
    else
        accumulation

let answer = list |> List.fold (mainFunction) 0

printfn $"{answer}"

// ANSWER: 2716