namespace Informedica.PimPrism.Lib


module Dto =


    open Types
    open Utils

    module PIM =

        let diagnoses =
            [
                Asthma, "Asthma"
                BoneMarrowTransplant, "Bone Marrow Transplant"
                Bronchiolitis, "Bronchiolitis"
                CardiacArrestInHospital, "Cardiac Arrest InHospital"
                CardiacArrestPreHospital, "Cardiac Arrest PreHospital"
                CardiomyopathyOrMyocarditis, "Cardiomyopathy Or Myocarditis"
                CerebralHemorrhage, "Cerebral Hemorrhage"
                Croup, "Croup"
                DiabeticKetoacidosis, "Diabetic Ketoacidosis"
                HIVPositive, "HIV Positive"
                HypoplasticLeftHeartSyndrome, "Hypoplastic Left Heart Syndrome"
                LeukemiaorLymphoma, "Leukemia Or Lymphoma"
                LiverFailure, "Liver Failure"
                NecrotizingEnterocolitis, "Necrotizing Enterocolitis"
                NeurodegenerativeDisorder, "Neurodegenerative Disorder"
                ObstructiveSleepApnea, "Obstructive Sleep Apnea"
                SeizureDisorder, "Seizure Disorder"
                SevereCombinedImmuneDeficiency, "Severe Combined Immune Deficiency"
            ]

        let diagnosesFromString s = 
            let eqs s1 s2 =
                let s1 = 
                    s1 
                    |> String.toLower
                    |> String.replace " " ""
                let s2 = 
                    s2 
                    |> String.toLower
                    |> String.replace " " ""
                s1 = s2

            diagnoses
            |> List.filter (snd >> (eqs s))
            |> List.map fst


        type Dto () =
            member val Elective = false  with get, set
            member val ElectiveUnknown = true  with get, set
            member val Recovery = false with get, set
            member val ByPass = false with get, set
            member val Cardiac = false with get, set
            member val RiskDiagnosis : string list = [] with get, set
            member val Ventilated = false with get, set
            member val AdmissionPupils = false with get, set
            member val PaO2kP : float option = None with get, set
            member val PaO2mmHg : float option = None with get, set
            member val FiO2 : float option = None with get, set
            member val BaseExcess : float option = None with get, set
            member val SystolicBloodPressure : float option = None with get, set
            member val Scores : (string * float) list = [] with get, set
            member val PIM2Score : float option = None with get, set
            member val PIM2Mortality : float option  = None with get, set
            member val PIM3Score : float option = None with get, set
            member val PIM3Mortality : float option = None with get, set


        let fromDto (dto: Dto) =
            {
                Urgency = 
                    match dto.Elective, dto.ElectiveUnknown with
                    | true,  _     -> Elective
                    | false, true  -> UnknownUrgency
                    | false, false -> NotElective
                Recovery =  
                    match dto.Recovery, dto.ByPass, dto.Cardiac with
                    | _, true, _         -> PostCardiacByPass
                    | _, false, true     -> PostCardiacNonByPass
                    | true, false, false -> PostNonCardiacProcedure
                    | false, false, _    -> NoRecovery
                RiskDiagnosis  =  dto.RiskDiagnosis |> List.collect diagnosesFromString
                Ventilated  =  dto.Ventilated
                AdmissionPupils  =  
                    if dto.AdmissionPupils = true then PIM.FixedDilated 
                    else PIM.UnknownPupils
                PaO2  =  
                    match dto.PaO2kP, dto.PaO2mmHg with
                    | Some _, _ -> dto.PaO2kP
                    | None, _   -> dto.PaO2mmHg |> Option.map calcmmHgToKiloPascal
                FiO2  =  dto.FiO2
                BaseExcess  =  dto.BaseExcess
                SystolicBloodPressure  =  dto.SystolicBloodPressure
                PIM2Scores   =  []
                PIM2Score   =  None
                PIM2Mortality   =  None
                PIM3Scores = []
                PIM3Score   =  None
                PIM3Mortality   =  None
            }
 