namespace Informedica.PimPrism.Tests
open Expecto

module Main =

    [<EntryPoint>]
    let main argv =
        Tests.runTestsInAssembly defaultConfig argv
