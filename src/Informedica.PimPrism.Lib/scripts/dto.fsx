
#load "../Utils.fs"
#load "../Types.fs"
#load "../Pim.fs"
#load "../Prism.fs"
#load "../Dto.fs"

open Informedica.PimPrism.Lib


Dto.PIM.Dto()
|> Dto.PIM.fromDto
|> PIM.calculatePIM2
|> PIM.calculatePIM3



