import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    at = BonusFactory.GetByName("AtutBonus")
    at.AtutName = "[Klasa/Druid] Wieź z dziczą"
    character.Controller.AddBonusOriginal(at)
    if (level >= 2):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Leśny krok"
        character.Controller.AddBonusOriginal(at)
    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Wędrówka bez śladów"
        character.Controller.AddBonusOriginal(at)
    if (level >= 5):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Kształty natury"
        count = level-5
        if (count > 6):
            count = 6
        at.AdditionalInfo = "{0}/dzień".format(count)
        character.Controller.AddBonusOriginal(at)
    if (level >= 9):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Niepodatność na jad"
        character.Controller.AddBonusOriginal(at)
    if (level >= 16):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Druid] Przemiana w żywiołaka"
        at.AdditionalInfo = "{0}/dzień".format(level-15)
        character.Controller.AddBonusOriginal(at)
    return