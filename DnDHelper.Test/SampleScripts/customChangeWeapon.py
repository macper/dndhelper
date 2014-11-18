import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,object):
    set = []
    set.append([ "Miecz krótki + 1", "Miecz krótki" ])
    set.append([ "Kij+1" ])
    
    index = int(object)
    
    chosen = set[index]
    character.Controller.UnEquipItem(ItemPosition.RightHand)
    character.Controller.UnEquipItem(ItemPosition.LeftHand)

    for element in chosen[:]:
        character.Controller.EquipItem(element)
    return