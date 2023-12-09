open System.IO

let lines = File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day2\Part2\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines
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
