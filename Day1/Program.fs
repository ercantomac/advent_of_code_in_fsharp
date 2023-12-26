open System.IO

let lines = File.ReadAllLines(@"C:\Users\fb_er\F#\Advent of Code\Day1\input.txt")

// Convert file lines into a list.
let list = Seq.toList lines

(* 
// IMPERATIVE APPROACH

let mutable result = 0

for line in list do
    printfn $"{line}"

    let mutable sum = -1
    let mutable lastNum = 0

    for character in line do
        let isNumber = System.Char.IsNumber(character)

        if isNumber then
            let digit = System.Char.GetNumericValue(character) |> int
            if sum = -1 then sum <- (digit * 10)
            lastNum <- digit

    sum <- (sum + lastNum)
    printfn $"{sum}"
    result <- (result + sum)

printfn $"{result}"
 *)


// FUNCTIONAL APPROACH

let calculateNumber (line: string) =
    printfn $"{line}"

    let charList =
        line.ToCharArray()
        |> Seq.toList
        |> List.filter (fun c -> System.Char.IsNumber(c))

    let firstDigit =
        System.Char.GetNumericValue(List.head charList)
        |> int

    let secondDigit =
        System.Char.GetNumericValue(List.last charList)
        |> int

    let sum = (firstDigit * 10) + secondDigit
    printfn $"{sum}"
    sum

let answer =
    list
    |> List.fold (fun accumulation line -> accumulation + calculateNumber line) 0

printfn $"{answer}"

// ANSWER: 55834
