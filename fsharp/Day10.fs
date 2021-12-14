open System

let lines =
    IO.File.ReadLines @"/home/sam/repositories/advent-of-code-2021/fsharp/input.txt"

let mismatched =
    [ "{]"
      "{)"
      "{>"
      "<]"
      "<)"
      "<}"
      "(]"
      "(}"
      "(>"
      "[)"
      "[>"
      "[}" ]

let replaceChars (remaining: string) =
    remaining
        .Replace("<>", "")
        .Replace("()", "")
        .Replace("{}", "")
        .Replace("[]", "")


let getCorruptedScore line =
    let rec score (remaining: string) =
        let currentLength = remaining.Length

        let replaced = replaceChars remaining

        let length = replaced.Length

        let anyMismatched =
            mismatched
            |> Seq.map (fun m -> replaced.IndexOf(m))
            |> Seq.filter (fun m -> m >= 0)
            |> Seq.sort

        if (length = 0
            || length = currentLength
            || Seq.length anyMismatched > 0) then
            let ch =
                anyMismatched
                |> Seq.tryHead
                |> Option.map (fun o -> replaced.Substring(o + 1, 1))
                |> Option.defaultValue ""

            match ch with
            | ")" -> 3
            | "]" -> 57
            | "}" -> 1197
            | ">" -> 25137
            | _ -> 0
        else
            score replaced

    score line

let corruptedScore =
    lines |> Seq.map getCorruptedScore |> Seq.sum

let closingScore =
    lines
    |> Seq.map
        (fun line ->
            let rec removeComplete (remaining: string) =
                let initialSize = Seq.length remaining
                let newRemaining = replaceChars remaining

                if initialSize <> Seq.length newRemaining then
                    removeComplete newRemaining
                else
                    newRemaining

            removeComplete line)
    |> Seq.filter
        (fun line ->
            mismatched
            |> Seq.map line.IndexOf
            |> Seq.filter (fun m -> m >= 0)
            |> Seq.length = 0)
    |> Seq.map
        (fun line ->
            line.ToCharArray()
            |> Array.rev
            |> Seq.fold
                (fun score ch ->
                    let value: uint64 =
                        match ch with
                        | '(' -> 1UL
                        | '[' -> 2UL
                        | '{' -> 3UL
                        | '<' -> 4UL
                        | _ -> 0UL

                    (score * 5UL) + value)
                0UL)
    |> Seq.sort
    |> Seq.toList
let middleValue = closingScore.Item(closingScore.Length / 2)
Console.WriteLine("Part one score {0}", corruptedScore)
Console.WriteLine("Part two score {0}", middleValue)
