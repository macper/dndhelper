import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "[Klasa/Mnich] Grad ciosów"
    character.Controller.AddBonusOriginal(at)
    
    at = BonusFactory.GetByName("AtutBonus", source)
    at.AtutName = "[Klasa/Mnich] Premia do KP"
    character.Controller.AddBonusOriginal(at)

    if (level >= 2):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Uchylanie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 3):
        bonus = BonusFactory.GetByName("ThrowBonus", source)
        bonus.BonusType.WillThrow = 2
        character.Controller.AddBonusOriginal(bonus)
        bonus = BonusFactory.GetByName("SpeedBonus", source)
        bonus.Value = 3;
        if (level >= 6):
            bonus.Value = 6
        if (level >= 9):
            bonus.Value = 9
        if (level >= 12):
            bonus.Value = 12
        if (level >= 15):
            bonus.Value = 15
        if (level >= 18):
            bonus.Value = 18
        character.Controller.AddBonusOriginal(bonus)
    if (level >= 4):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Nieświadomy unik"
        character.Controller.AddBonusOriginal(at)
    if (level >= 5):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Czystość ciała"
        character.Controller.AddBonusOriginal(at)
    if (level >= 7):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Jedność ciała"
        character.Controller.AddBonusOriginal(at)
    if (level >= 9):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "Doskonalsze uchylanie"
        character.Controller.AddBonusOriginal(at)
    if (level >= 11):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Diamentowe ciało"
        character.Controller.AddBonusOriginal(at)
    if (level >= 12):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich[ Daleki krok"
        character.Controller.AddBonusOriginal(at)
    if (level >= 13):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Diamentowa dusza"
        character.Controller.AddBonusOriginal(at)
    if (level >= 15):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Drżąca pięść"
        character.Controller.AddBonusOriginal(at)
    if (level >= 19):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Puste ciało"
        character.Controller.AddBonusOriginal(at)
    if (level >= 20):
        at = BonusFactory.GetByName("AtutBonus", source)
        at.AtutName = "[Klasa/Mnich] Idealne ja"
        character.Controller.AddBonusOriginal(at)
    return