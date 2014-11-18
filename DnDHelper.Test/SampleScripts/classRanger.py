import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "[Klasa/Tropiciel] Preferowany wróg"
    character.Controller.AddBonusOriginal(at)
    
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "Tropienie"
    character.Controller.AddBonusOriginal(at)

    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "Więź z dziczą"
    character.Controller.AddBonusOriginal(at)

    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Krzepa"
        character.Controller.AddBonusOriginal(at)
    if (level >= 7):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Leśny krok"
        character.Controller.AddBonusOriginal(at)
    if (level >= 9):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Uchylanie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 13):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Tropciel] Kamuflaż"
        character.Controller.AddBonusOriginal(at)
    return