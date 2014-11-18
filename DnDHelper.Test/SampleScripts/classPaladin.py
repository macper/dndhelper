import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "[Klasa/Paladyn] Wykrycie zła"
    character.Controller.AddBonusOriginal(at)
    
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "[Klasa/Paladyn] Ugodzenie zła"
    at.AdditionalInfo = "{0}/dzień".format(1 + level / 5)
    character.Controller.AddBonusOriginal(at)

    if (level >= 2):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Paladyn] Nakładanie rąk"
        character.Controller.AddBonusOriginal(at)

        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Paladyn] Boska łaska"
        character.Controller.AddBonusOriginal(at)

    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Paladyn] Aura odwagi"
        character.Controller.AddBonusOriginal(at)
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Paladyn] Boskie zdrowie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 4):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Odpędzanie nieumarłych"
        character.Controller.AddBonusOriginal(at)
    if (level >= 6):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Paladyn] Przełamanie choroby"
        at.AdditionalInfo = "{0}/tydzień".format(level-5 / 2)
        character.Controller.AddBonusOriginal(at)
    return