import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    atutRage = BonusFactory.GetByName("AtutBonus", source)
    atutRage.AtutName = "[Klasa/Bard] Muzyka barda"
    atutRage.AdditionalInfo = "{0}/dzień".format(level)
    character.Controller.AddBonusOriginal(atutRage)
    at = BonusFactory.GetByName("AtutBonus")
    at.AtutName = "[Klasa/Bard] Kontrpieśń"
    character.Controller.AddBonusOriginal(at)
    at = BonusFactory.GetByName("AtutBonus")
    at.AtutName = "[Klasa/Bard] Fascynacja"
    character.Controller.AddBonusOriginal(at)
    at = BonusFactory.GetByName("AtutBonus")
    at.AtutName = "[Klasa/Bard] Inspirowanie odwagi"
    at.AdditionalInfo = "+1"
    if (level >= 8):
        at.AdditionalInfo = "+2"
    if (level >= 14):
        at.AdditionalInfo = "+3"
    if (level >= 20):
        at.AdditionalInfo = "+4"
    character.Controller.AddBonusOriginal(at)
    
    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Bard] Inspirowanie biegłości"
        character.Controller.AddBonusOriginal(at)
    if (level >= 6):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Bard] Sugestia"
        character.Controller.AddBonusOriginal(at)
    if (level >= 12):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Bard] Pieśn wolności"
        character.Controller.AddBonusOriginal(at)
    if (level >= 15):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Bard] Inspirowanie heroizmu"
        character.Controller.AddBonusOriginal(at)
    return