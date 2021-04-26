
#r "nuget: Expecto, 9.0.2"


#load "../Utils.fs"
#load "../Types.fs"
#load "../Pim.fs"
#load "../Prism.fs"
#load "../Dto.fs"


open System
open Expecto
open Expecto.Flip
open Expecto.Logging


open Informedica.PimPrism.Lib

open Dto

fsi.AddPrinter<DateTime> (fun dt -> dt.ToString("dd-MM-yyyy"))

// checked with: https://www.cpccrn.org/calculators/prismiiicalculator/


let now = DateTime.Now
let newborn = now.AddDays(-1.)      |> Some
let neonate = now.AddDays(-15.)      |> Some
let infant = now.AddDays(-50.)      |> Some
let child = now.AddDays(-3. * 365.) |> Some
let adolescent = now.AddYears(-13)  |> Some




let createTests case mort (f : PRISM.Dto -> unit) =
//    Console.WriteLine($"testing {case}")
        // Baseline newBorn
        // Note admission source unknown = recovery
    PRISM.Dto()
    |> fun dto ->
        dto |> f
        dto
    |> PRISM.fromDto
    |> PRISM.mapPRISMtoInput
    |> PRISM.calculate DateTime.Now
    |> fun prims -> 
        testParam prims [
            $"{case} PRISM IV", 
            fun prism () ->
                prism.PRISM4Mortality
                |> Option.map (fun v -> Math.Round(100. * v, 1))
                |> Expect.equal "should equal" mort

        ]


let tests =
    testList "PRISM" [
        // Baseline newBorn
        // Note admission source unknown = recovery
        yield!
            fun (dto : PRISM.Dto) ->
                dto.Age <- newborn
            |> createTests "baseline newborn" (Some 1.1)
        // Neuro: 0
        // NonNeuro: 0
        // Mort 1.1%

        // Baseline neonate
        // Note admission source unknown = recovery
        yield!
            fun (dto : PRISM.Dto) ->
                dto.Age <- neonate
            |> createTests "baselne neonate" (Some 0.8)
        // Neuro: 0
        // NonNeuro: 0
        // Mort 0.8%

        // // Baseline infant
        // // Note admission source unknown = recovery
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- infant
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 0
        // // NonNeuro: 0
        // // Mort 0.4%


        // // Baseline child
        // // Note admission source unknown = recovery
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- child
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 0
        // // NonNeuro: 0
        // // Mort 0.3%


        // // Baseline adolescent
        // // Note admission source unknown = recovery
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- adolescent
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 0
        // // NonNeuro: 0
        // // Mort 0.3%


        // // NewBorn
        // // pupil reaction one dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- newborn
        //     dto.PupilsFixed <- Some 1
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 7
        // // NonNeuro: 0
        // // Mort 4.3%



        // // Neonate
        // // pupil reaction one dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- neonate
        //     dto.PupilsFixed <- Some 1
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 7
        // // NonNeuro: 0
        // // Mort 3.1%


        // // Infant
        // // pupil reaction one dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- infant
        //     dto.PupilsFixed <- Some 1
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 7
        // // NonNeuro: 0
        // // Mort 1.7%


        // // Child
        // // pupil reaction one dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- child
        //     dto.PupilsFixed <- Some 1
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 7
        // // NonNeuro: 0
        // // Mort 1.2%


        // // NewBorn
        // // pupil reaction two dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- newborn
        //     dto.PupilsFixed <- Some 2
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 11
        // // NonNeuro: 0
        // // Mort 9.1%



        // // Neonate
        // // pupil reaction two dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- neonate
        //     dto.PupilsFixed <- Some 2
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 11
        // // NonNeuro: 0
        // // Mort 6.6%


        // // Infant
        // // pupil reaction two dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- infant
        //     dto.PupilsFixed <- Some 2
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 11
        // // NonNeuro: 0
        // // Mort 3.7%


        // // Adolescent
        // // pupil reaction two dilated
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- adolescent
        //     dto.PupilsFixed <- Some 2
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 11
        // // NonNeuro: 0
        // // Mort 2.6%



        // // Newborn
        // // SBP = 30
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- newborn
        //     dto.SystolicBloodPressureMin <- Some 30.
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 0
        // // NonNeuro: 7
        // // Mort 3.5%


        // // Neonate
        // // SBP = 30
        // Dto.PRISM.Dto()
        // |> fun dto ->
        //     dto.Age <- neonate
        //     dto.SystolicBloodPressureMin <- Some 30.
        //     dto
        // |> Dto.PRISM.fromDto
        // |> PRISM.mapPRISMtoInput
        // |> PRISM.calculate DateTime.Now
        // // Neuro: 0
        // // NonNeuro: 7
        // // Mort 2.5%
    ]

    
tests    
|> runTestsWithCLIArgs [ CLIArguments.Verbosity LogLevel.Verbose ] [||]


