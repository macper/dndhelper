   M S B A M L   `   `   `  ����K  GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain  5   	   ]@clr-namespace:DnDHelper.Domain.Bonuses;assembly=DnDHelper.DomainDnDHelper.Domain.Bonuses  5   	   L HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls 5   	   5clr-namespace:DnDHelper.GUI.WPFDnDHelper.GUI.WPF 6=   +   %DnDHelper.GUI.WPF.PopUps.AddEditBonus   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Hdnd8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     5   	   Obn@clr-namespace:DnDHelper.Domain.Bonuses;assembly=DnDHelper.Domain     5   	   7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   	   /WPFclr-namespace:DnDHelper.GUI.WPF     6=   -   5        $��bonusWindow��5   i     � Title$  Edycja bonusa��5   	   $	��430��6   $	��450��6,    � WindowStartupLocation$ CenterScreen=�68    � 	Resources 5       �� DataType-   'DnDHelper.Domain.Bonuses.MainSkillBonus �� DisplayMemberPath �� SelectedValuePath �� SelectedValue&    DnDHelper.Domain.EnumsDictionary   
MainSkills �� Source0  *DnDHelper.GUI.WPF.Controls.AdditiveControl	   Value
 �� Mode&    DnDHelper.Domain.Bonuses.ACBonus   ACTypes0   *DnDHelper.Domain.Bonuses.AppendEffectBonus �� Path �� RelativeSource �� AncestorType*   $DnDHelper.Domain.Bonuses.AttackBonus d� 	IsChecked(   "DnDHelper.Domain.Bonuses.AtutBonus*   $DnDHelper.Domain.Bonuses.DamageBonus-	  'DnDHelper.GUI.WPF.Controls.DamageEditor 	 Name � DataContext   DamageToModelConverter �� 	Converter&
    DnDHelper.Domain.Bonuses.HPBonus.   (DnDHelper.Domain.Bonuses.InitiativeBonus0   *DnDHelper.Domain.Bonuses.MaxDexterityBonus+   %DnDHelper.Domain.Bonuses.PanaltyBonus)   #DnDHelper.Domain.Bonuses.RangeBonus.   (DnDHelper.Domain.Bonuses.ResistanceBonus   DamageTypes2   ,DnDHelper.Domain.Bonuses.SecondarySkillBonus)   #DnDHelper.Domain.Bonuses.SpeedBonus)   #DnDHelper.Domain.Bonuses.ThrowBonus+   %DnDHelper.Domain.Bonuses.OverallBonus3   -DnDHelper.Domain.Bonuses.NumberOfAttacksBonus��%�"  (��       5	      *, +)(�� V    5      *, +)(�� �    5      *, +)(�� f    5      *, +)(�� �    5'      *, +)(�� _    5/      *, +)7   (�� -    54      *,
 +)(�� �    5:      *, +)(�� �    5@      *, +)(�� U    5F      *, +)(�� �    5L      *, +)(��     5R      *, +)(�� �    5\      *, +)(�� J    5d      *, +)(�� �    5j      *, +)(��     5t      *, +)(�� t    5z      *, +)�� 5	   
   "  6   .R�5
      � $��Stretch=�6   $��Stretch=�61   .+�5      �� $��23��6   $
��Left=�6(   $��10,8,0,0q�6C   $��Cecha:��6V   $	��Top=�6d   �� 5      $��23��6   $
��Left=�6'   $��67,6,0,0q�6B   $	��Top=�6U   $	��152��6m   $
 Name��6�   $ Value��6�    6y   �� *	Attribute+��6�   �� # Z �� 5      $��23��6   $
��Left=�6(   $��	10,37,0,0q�6C   $��
Wartość:��6W   $	��Top=�6g     5      $
��Left=�6%   $��	67,33,0,0q�6@   $	��Top=�6x   $	��152��6�   	 6S   �� *Value+$
 TwoWay=�6�   5      5      �� 5   
   "  6   .R�5      � $��Stretch=�6   $��Stretch=�62   .+�5      �� $��Typ pancerza:��6   $��28��60   $
��Left=�6<   $��6,6,0,0q�6W   $	��Top=�6h   �� 5      $��23��6   $
��Left=�6'   $��95,8,0,0q�6B   $
 Name��6�   $ Value��6�   $	��Top=�6�   $	��153��6  ��6T   �� # Z  6�   �� *ACType+6  �� 5      $��
Wartość:��6   $��28��6+   $
��Left=�67   $��6,40,0,0q�6R   $	��Top=�6d     5      $
��Left=�6%   $��	95,37,0,0q�6@   $	��Top=�6w   $	��153��6�   	 6S   �� *Value+$
 TwoWay=�6�   5      5      �� 5   
   "  6   .R�5      � $��Stretch=�6   $��Stretch=�61   .+�5      �� $��Efekt:��6   $��28��6)   $
��Left=�65   $��6,6,0,0q�6P   $	��Top=�6b   �� 5      $��23��6   $
��Left=�6'   $��53,7,0,0q�6B   $
 Name��6�   $
 Name��6  $	��Top=�6  $	��195��67  ��6T   �� $ DataContext.Effects� �� *FindAncestor+$ WindowL� 6�   �� *
EffectName+67  5      5      �� 5   
   "  6   .R�5       � $��Stretch=�6   $��Stretch=�61   .+�5!      �� $��Premia do ataku:��6   $��28��63   $
��Left=�6?   $��6,6,0,0q�6Z   $	��Top=�6l     5"      $
��Left=�6%   $��	111,6,0,0q�6@   $	��Top=�6y   $	��145��6�   	 6S   �� *Value+$
 TwoWay=�6�   �� 5#      $��label2��6g   $��Atak wręcz:��6   $��28��6.   $
��Left=�6:   $��6,40,0,0q�6U   $	��Top=�6u   �� 5$      $	��Tak��6   $��16��6)   $
��Left=�65   $��
111,46,0,0q�6P   $	��Top=�6�   $	��145��6�    6d   �� *Melee+$
 TwoWay=�6�   5%      5&      �� 5'   
   "  6   .R�5(      � $��Stretch=�6   $��Stretch=�61   .+�5)      �� $��Atut:��6   $��28��6(   $
��Left=�64   $��6,6,0,0q�6O   $	��Top=�6a   �� 5*      $��23��6   $��Right=�6&   $��0,38,6,0q�6B   $	��Top=�6t   $	��143��6�   ��6T   �� *AdditionalInfo+6�   �� 5+      $��23��6   $��Right=�6'   $��0,7,6,0q�6C   $	��Top=�6T   $	��143��6l   $
 Name��6  $
 Name��6'   6x   �� *
AtutName+��6�   �� $ DataContext.Atutes� �� *FindAncestor+$ WindowL�6'  �� 5,      $��Dodatkowe info:��6   $��28��62   $
��Left=�6>   $��6,38,0,0q�6Y   $	��Top=�6k   5-      5.      �� 5/   
   "  6   .R�50      � $��Stretch=�6   $��Stretch=�61   .+�51      	  $ damageEditor��6"   $
��Left=�68   $	��Top=�6�    6S   �� $ Amount�8   6�   52      53      �� 54   
   " 
 6   .R�55      � $��Stretch=�6   $��Stretch=�61   .+�56      �� $��label2��6k   $��Premia do życia:��6   $��28��63   $
��Left=�6?   $��5,5,0,0q�6Z   $	��Top=�6y     57      $
��Left=�6O   $��	102,7,0,0q�6j   $	��Top=�6}   $	��154��6�   	 6%   �� $ Value�$
 TwoWay=�6�   58      59      �� 5:   
   "  6   .R�5;      � $��Stretch=�6   $��Stretch=�61   .+�5<      �� $��label2��6p   $��Premia do inicjatywy:��6   $��28��68   $
��Left=�6D   $��5,5,0,0q�6_   $	��Top=�6~     5=      $
��Left=�6O   $��	132,6,0,0q�6j   $	��124��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5>      5?      �� 5@   
   "  6   .R�5A      � $��Stretch=�6   $��Stretch=�61   .+�5B      �� $��label2��6t   $"��Max. premia za zręczność:��6   $��28��6<   $
��Left=�6H   $��5,5,0,0q�6c   $	��Top=�6�     5C      $
��Left=�6O   $��	158,7,0,0q�6j   $��98��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5D      5E      �� 5F   
   "  6   .R�5G      � $��Stretch=�6   $��Stretch=�61   .+�5H      �� $��label2��6j   $��Kara do testów:��6   $��28��62   $
��Left=�6>   $��5,5,0,0q�6Y   $	��Top=�6x     5I      $
��Left=�6O   $��	102,7,0,0q�6j   $	��154��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5J      5K      �� 5L   
   "  6   .R�5M      � $��Stretch=�6   $��Stretch=�61   .+�5N      �� $��label2��6f   $��Zasięg (m):��6   $��28��6.   $
��Left=�6:   $��5,5,0,0q�6U   $	��Top=�6t     5O      $
��Left=�6O   $��	102,7,0,0q�6j   $	��154��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5P      5Q      �� 5R   
   "  6   .R�5S      � $��Stretch=�6   $��Stretch=�61   .+�5T      �� $��label2��6g   $��Typ obrażeń:��6   $��28��6/   $
��Left=�6;   $��5,5,0,0q�6V   $	��Top=�6u   �� 5U      $��23��6   $
��Left=�6'   $��95,8,0,0q�6B   $
 Name��6�   $ Value��6�   $	��Top=�6�   $	��153��6  ��6T   �� # Z  6�   �� *
DamageType+6  �� 5V      $��label3��6e   $��	Redukcja:��6   $��28��6,   $
��Left=�68   $��6,40,0,0q�6S   $	��Top=�6s   �� 5W      $��23��6   $
��Left=�6&   $��	95,40,0,0q�6A   $	��Top=�6k   $	��153��6�   ��6T   �� *Value+6�   �� 5X      $��label4��6h   $��Przełamanie:��6   $��28��6/   $
��Left=�6;   $��5,74,0,0q�6V   $	��Top=�6v   �� 5Y      $��23��6   $
��Left=�6&   $��	95,74,0,0q�6A   $	��Top=�6s   $	��153��6�   ��6T   �� *OverrideValue+6�   5Z      5[      �� 5\   
   "  6   .R�5]      � $��Stretch=�6   $��Stretch=�61   .+�5^      �� $��label2��6g   $��Umiejętność:��6   $��28��6/   $
��Left=�6;   $��5,5,0,0q�6V   $	��Top=�6u   �� 5_      $��23��6   $
��Left=�6'   $��95,8,0,0q�6B   $
 Name��6�   $
 Name��6  $	��Top=�6  $	��153��65  ��6T   �� $ DataContext.Skills� �� *FindAncestor+$ WindowL� 6�   �� *	SkillName+65  �� 5`      $��label3��6b   $��Bonus:��6   $��28��6)   $
��Left=�65   $��6,40,0,0q�6P   $	��Top=�6p   �� 5a      $��23��6   $
��Left=�6&   $��	95,40,0,0q�6A   $	��Top=�6k   $	��153��6�   ��6T   �� *Value+6�   5b      5c      �� 5d   
   "  6   .R�5e      � $��Stretch=�6   $��Stretch=�61   .+�5f      �� $��label2��6m   $��Szybkość (m/turę):��6   $��28��65   $
��Left=�6A   $��5,5,0,0q�6\   $	��Top=�6{     5g      $
��Left=�6O   $��	119,7,0,0q�6j   $	��137��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5h      5i      �� 5j   
   "  6   .R�5k      � $��Stretch=�6   $��Stretch=�61   .+�5l      �� $��label2��6c   $��Refleks:��6   $��28��6+   $
��Left=�67   $��5,5,0,0q�6R   $	��Top=�6q     5m      $
��Left=�6_   $��98,7,0,0q�6z   $	��Top=�6�   $��80��6�   	 6%   �� $ BonusType.ReflexThrow�$
 TwoWay=�6�   �� 5n      $��label3��6a   $��Wola:��6   $��28��6(   $
��Left=�64   $��6,37,0,0q�6O   $	��Top=�6o     5o      $
��Left=�6]   $��	98,38,0,0q�6x   $	��Top=�6�   $��80��6�   	 6%   �� $ BonusType.WillThrow�$
 TwoWay=�6�   �� 5p      $��label4��6g   $��Wytrwałość:��6   $��28��6.   $
��Left=�6:   $��6,68,0,0q�6U   $	��Top=�6u     5q      $
��Left=�6a   $��	98,69,0,0q�6|   $	��Top=�6�   $��80��6�   	 6%   �� $ BonusType.EnduranceThrow�$
 TwoWay=�6�   5r      5s      �� 5t   
   "  6   .R�5u      � $��Stretch=�6   $��Stretch=�61   .+�5v      �� $��label2��6l   $��Premia do rzutów:��6   $��28��64   $
��Left=�6@   $��5,5,0,0q�6[   $	��Top=�6z     5w      $
��Left=�6O   $��	119,6,0,0q�6j   $	��154��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5x      5y      �� 5z   
   "  6   .R�5{      � $��Stretch=�6   $��Stretch=�61   .+�5|      �� $��label2��6s   $��Premia do liczby ataków:��6   $��28��6;   $
��Left=�6G   $��5,5,0,0q�6b   $	��Top=�6�     5}      $
��Left=�6O   $��	152,6,0,0q�6j   $	��104��6}   $	��Top=�6�   	 6%   �� $ Value�$
 TwoWay=�6�   5~      5      5�      .��5�      � .+�5�   
   �� -   5        $��label1��5�   Y   $
��Typ:��6   $��28��6   $
��Left=�6+   $��	12,12,0,0q�6F   $	��Top=�6g   �� 5�   
   $��23��6   $
��Left=�6   $��	65,15,0,0q�6:   $
 Name��6�   $ Value��6�   $	��Top=�6�   $	��201��6   6M   �� *	BonusName+��6q   ��    
BonusTypes# Z 6  �� 5�   
   -   5        $��Content��5�      $	��300��6B   $
��Left=�6O   $��	10,46,0,0q�6j   $	��Top=�6}   $	��400��6�   $��5q�6�   ��6(   �� *Bonus+5�      �� 5�   
   $��OK��6   $��25��69   $
��Left=�6E   $��335,354,0,0q�6`   $	��Top=�6v   $��75��6�     
buttonInfo#��[ 6�   ��6   �� *Commit+6�   5�      5�      