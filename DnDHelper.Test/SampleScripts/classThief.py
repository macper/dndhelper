import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "Podstępny atak"
    at.AdditionalInfo = "+{0}K6".format(1 + level/2)
    character.Controller.AddBonusOriginal(at)
    
    if (level >= 2):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Uchylanie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Wyczucie pułapek"
        at.AdditionalInfo = "+{0}".format(level / 3)
        character.Controller.AddBonusOriginal(at)
    if (level >= 4):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Nieświadomy unik"
        character.Controller.AddBonusOriginal(at)
    if (level >= 10):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Doskonalsze uchylanie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 15):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Mistrzostwo w umiejętnościach"
        character.Controller.AddBonusOriginal(at)
    if (level >= 20):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Wyniszczający atak"
        character.Controller.AddBonusOriginal(at)
    return