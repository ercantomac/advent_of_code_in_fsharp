open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day2\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

(*
// IMPERATIVE APPROACH

let mutable result = 0

for i in 0 .. list.Length - 1 do
    let line = list[i]
    printfn $"{line}"

    let j = line.IndexOf(':')
    let gameSets = line[ (j + 2) .. ].Split ';'
    let mutable maxRed = 0
    let mutable maxGreen = 0
    let mutable maxBlue = 0

    for set in gameSets do
        let setTrimmed = set |> String.filter (fun c -> c <> ' ')
        printfn $"{setTrimmed}"
        let individualCubes = setTrimmed.Split ','

        for cube in individualCubes do
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

            if color = "red" && quantityInt > maxRed then
                maxRed <- quantityInt

            elif color = "green" && quantityInt > maxGreen then
                maxGreen <- quantityInt

            elif color = "blue" && quantityInt > maxBlue then
                maxBlue <- quantityInt

    let power = maxRed * maxGreen * maxBlue
    printfn $"{maxRed}, {maxGreen}, {maxBlue}"
    result <- (result + power)

printfn $"{result}"
 *)

// FUNCTIONAL APPROACH

let getCubeQuantity cube =
    cube
    |> String.filter (fun c -> System.Char.IsNumber(c))
    |> int

let getCubeColor cube =
    cube
    |> String.filter (fun c -> not (System.Char.IsNumber(c)))

let testSet set color =
    let setTrimmed = set |> String.filter (fun c -> c <> ' ')
    printfn $"{setTrimmed}"
    let individualCubes = setTrimmed.Split ','

    let cubesFiltered =
        individualCubes
        |> Array.filter (fun cube -> (getCubeColor cube) = color)

    if not (cubesFiltered |> Array.isEmpty) then
        getCubeQuantity (cubesFiltered |> Array.head)
    else
        0

let mainFunction (accumulation: int) (line: string) =
    printfn $"{line}"

    let j = line.IndexOf(':')

    let gameNo = int ((line[ .. (j - 1) ].Split ' ')[1])
    printfn $"{gameNo}"
    let gameSets = line[ (j + 2) .. ].Split ';'

    let redCubes =
        gameSets
        |> Array.map (fun set -> testSet set "red")

    let greenCubes =
        gameSets
        |> Array.map (fun set -> testSet set "green")

    let blueCubes =
        gameSets
        |> Array.map (fun set -> testSet set "blue")

    let power =
        (redCubes |> Array.max)
        * (greenCubes |> Array.max)
        * (blueCubes |> Array.max)

    accumulation + power

let answer = list |> List.fold (mainFunction) 0

printfn $"{answer}"

// ANSWER: 72227