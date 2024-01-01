open System.IO

let lines =
    File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day4\Part1\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

// FUNCTIONAL APPROACH

let pow (number: int) (power: int) : int =
    if power < 0 then
        0
    elif power = 0 then
        1
    else
        let result =
            [ 1..power ]
            |> List.fold (fun accumulation n -> accumulation * number) 1

        result

let mainFunction (accumulation: int) (line: string) =
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

    accumulation + (pow 2 (numberOfMatches - 1))

let answer = list |> List.fold (mainFunction) 0

printfn $"{answer}"

// ANSWER: 22488