import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    atutRage = BonusFactory.GetByName("AtutBonus", source)
    atutRage.AtutName = "Szał"
    atutRage.AdditionalInfo = "{0}/dzień".format((level / 4)+1)
    character.Controller.AddBonusOriginal(atutRage)
    at = BonusFactory.GetByName("SpeedBonus",source)
    at.Value = 3;
    character.Controller.AddBonusOriginal(at)
    if (level >= 2):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Nieświadomy unik"
        character.Controller.AddBonusOriginal(at)
    if (level >= 3):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Wyczucie pułapek"
        at.AdditionalInfo = "+{0}".format(level/3)
        character.Controller.AddBonusOriginal(at)
    if (level >= 7):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Redukcja obrażeń"
        at.AdditionalInfo = "Fizyczne({0})".format(1 + (level - 7) / 3)
        character.Controller.AddBonusOriginal(at)
        at = BonusFactory.GetByName("ResistanceBonus", source)
        at.Value = 1 + (level - 7)/3
        at.DamageType = DamageTypes.Physical
        character.Controller.AddBonusOriginal(at)
    if (level >= 11):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Potężniejszy szał"
        character.Controller.AddBonusOriginal(at)
    if (level >= 14):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Niezłomna wola"
        character.Controller.AddBonusOriginal(at)
    if (level >= 20):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Mężny szał"
        character.Controller.AddBonusOriginal(at)
    return