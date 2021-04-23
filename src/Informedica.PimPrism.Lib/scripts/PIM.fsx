
#load "../Utils.fs"
#load "../Types.fs"
#load "../Pim.fs"
#load "../Prism.fs"
#load "../Dto.fs"

open Informedica.PimPrism.Lib

// results checked with
// PIM2: https://qxmd.com/calculate/calculator_368/pim2
// PIM3: https://espnic-online.org/Education/Professional-Resources/Paediatric-Index-of-Mortality-3


// Base line
// PIM2 mort = 0.8%
// PIM3 mort = 1.2%
Dto.PIM.Dto()
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case pupils 
// PIM 2 factor = 3.0791
// PIM 3 factor = 3,8233
// PIM2 mort = 14.1%
// PIM3 mort = 36%
Dto.PIM.Dto()
|> fun dto ->
    dto.AdmissionPupils <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3



// Case Elective
// PIM2 factor = -0,9282
// PIM3 factor = -0,5378
// PIM2 mort = 0.3%
// PIM3 mort = 0.7%
Dto.PIM.Dto()
|> fun dto ->
    dto.Elective <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case Ventilated
// PIM2 factor = 1,3352
// PIM3 factor = 0,9763
// PIM2 mort = 2.8%
// PIM3 mort = 3.2%
Dto.PIM.Dto()
|> fun dto ->
    dto.Ventilated <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case BE = 1
// PIM2 factor = 0,104
// PIM3 factor = 0,0671
// PIM2 mort = 0.8%
// PIM3 mort = 1.3%
Dto.PIM.Dto()
|> fun dto ->
    dto.BaseExcess <- Some 1.
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case SBP = 100
// PIM2 factor = 0,279
// PIM3 factor = -2.594
// PIM2 mort = 1.0%
// PIM3 mort = 1.4%
Dto.PIM.Dto()
|> fun dto ->
    dto.SystolicBloodPressure <- Some 100.
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case fiO2/paO2 = 1
// PIM2 factor = 0,2888
// PIM3 factor = 0,4214
// PIM2 mort = 1.0%
// PIM3 mort = 1.7%
Dto.PIM.Dto()
|> fun dto ->
    dto.FiO2 <- Some 1.
    dto.PaO2mmHg <- Some 100.
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case cardiac bypass
// PIM2 factor = 0,7507
// PIM3 factor = -1,2246
// PIM2 mort = 1.6%
// PIM3 mort = 0.4%
Dto.PIM.Dto()
|> fun dto ->
    dto.ByPass <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case cardiac non bypass
// PIM3 factor = -0.8762
// PIM3 mort = 0.5%
Dto.PIM.Dto()
|> fun dto ->
    dto.Cardiac <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case cardiac bypass
// PIM2 factor = -1,0244
// PIM3 factor = -1,5164
// PIM2 mort = 0.3%
// PIM3 mort = 0.3%
Dto.PIM.Dto()
|> fun dto ->
    dto.Recovery <- true
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case low risk
// PIM2 factor = -1,577
// PIM3 factor = -2,1766
// PIM2 mort = 0.2%
// PIM3 mort = 0.1%
Dto.PIM.Dto()
|> fun dto ->
    dto.RiskDiagnosis <- ["asthma"] 
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case high risk
// PIM2 factor = 1,6829
// PIM3 factor = 1,0725
// PIM2 mort = 3.9%
// PIM3 mort = 3.5%
Dto.PIM.Dto()
|> fun dto ->
    dto.RiskDiagnosis <- ["hypoplastic left heart syndrome"] 
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case high and low risk
// PIM2 factor = 1,6829 high
// PIM2 factor = -1,577 low
// PIM3 factor = 1,0725
// PIM2 mort = 0.8%
// PIM3 mort = 3.5%
Dto.PIM.Dto()
|> fun dto ->
    dto.RiskDiagnosis <- ["hypoplastic left heart syndrome"; "asthma"] 
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3


// Case very high risk (only PIM3)
// PIM2 factor = 1,6829
// PIM3 factor = 1,6225
// PIM2 mort = 3.9%
// PIM3 mort = 5.9%
Dto.PIM.Dto()
|> fun dto ->
    dto.RiskDiagnosis <- ["Leukemia Or Lymphoma"] 
    dto
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3

