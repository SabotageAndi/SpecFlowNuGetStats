// Learn more about F# at http://fsharp.org

open System
open FSharp.Data


type NugetStats = HtmlProvider<"https://www.nuget.org/packages/SpecFlow">

type VersionDownloads =
    {
        Version: string
        Downloads: decimal
    }


let parseVersion (version : string) = 
    version.Replace("(current)", "").Trim()

[<EntryPoint>]
let main argv =

    let rawStats = NugetStats().Tables.``Version History``

    rawStats.Rows 
        |> Seq.map (fun r -> {VersionDownloads.Version = parseVersion r.Version2; Downloads = r.Downloads})
        |> Seq.iter (fun r -> printfn "Version: %s; Downloads: %f" r.Version r.Downloads)

    0 // return an integer exit code
