   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   7   0    *DnDHelper.GUI.WPF.Panels.CharacterTabStats   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   7   .��5      �� $��2q�6   $��1q�6   
��� d �._�5	   
   � $	��620��6   $	��800��6   $	��Top=�6(   $
��Left=�6@     � RowDefinitions  5
      �� 5      $	��100 �6    �� 5      $��30 �6    �� 5      $	��500 �6    .+�5      � $	��100��6   $��1,2,0,0q�6    $	��Top=�61   $
��Left=�6I   -  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Local=�6d   $��2��6�    � ColumnDefinitions 5      �� 5      $
��260* �6'   �� 5      $
��300* �6'   �� 5      $
��300* �6'   .+�5      �� $��0��6    	  label#��[  6(   $	��Top=�6J   $
��Left=�6b   $��10,5,0,0q�6}   .��6�   Nazwa:6�   �� 5        textBox#��[ 6   $��	68,5,4,65q�6[      TabIndex$ 1��6n   $	��150��6�   ��6@   ��  �� Path$
 Name�6�   �� 5      $��1��6   #��[  6(   $	��Top=�6J   $
��Left=�6b   $��2,5,0,0q�6}   .��6�   Rasa:6�   �� 5       	 combo#��[ 6   $��1��6=   $	��150��6M    �� DisplayMemberPath$
 Name��6}   $��
111,5,0,65q�6�   $ 3��6�   ��6Z   �� $ Races� �� SelectedValue 6�   �� $
 Race�6�   �� 5      $��2��6   #��[  6(   $��
5,5,140,70q�6J   .��6^   Klasa:6f   �� 5      -   5        $��
MultiClass��5   Q   $��2��6   $��14(�6+   
��� d �$��Wiele��6c   $��18��6s   $
��Left=�6�   $��
55,12,0,70q�6�    d� 	IsChecked 6�   �� $ IsMultiClass� �� Mode$ OneWay=�6�   �� 5      #��[ 6   $��2��6=   $	��150��6M   $
 Name��6�   $��
111,5,0,65q�6<  y�6Z   �� $ 	IsChecked�	 �� ElementName$	 
MultiClass�� ' #BooleanToInverseVisibilityConverter
 �� 	Converter#
 [ ��6�   �� $ Classes� 6  �� $ Class�6<  �� 5      #��[  6   $	��Top=�6:   $
��Left=�6R   $��	10,45,0,0q�6m   .��6�   	Poziom:6�   �� 5      $��1��6   #��[  6(   $	��Top=�6J   $
��Left=�6b   $��2,45,0,0q�6}   .��6�   Doświadczenie:6�   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5      -   5          Name$ additiveControl1��5   �   $
��Left=�6�   $��	67,46,0,0q�6�   $	��Top=�6  $��31��6.  $	��160��6:  y�6%   �� $ 	IsChecked�$	 
MultiClass��#
 [    Value 6�   �� $ Level�$ TwoWay=�6:  �� 5      #��[ 6   $��75,46,10,26q�6�    �� 
IsReadOnly . y�6>   �� $ 	IsChecked�$	 
MultiClass��   BooleanToVisibilityConverter#
 [ ��6�   �� $ Level�6�     5       -   5        $ additiveControl2��5    �   $��1��6T   $
��Left=�6d   $��
111,49,0,0q�6   $	��Top=�6�   $	��148��6�    6%   �� $ 
Experience�$ TwoWay=�6�   �� 5!      $��2��6�   $��64��6�   $
��Left=�6�   $��
111,36,0,0q�6�   $	��Top=�66  $	��150��6N  y�6   �� $ 	IsChecked�$	 
MultiClass��#
 [ ��6�   �� $ CharacterClasses� 6
  �� $ CurrentClass�6N  �� 5"      -   5        $��button1��5"     $��Dodaj��6�   $��2��6�   $
��Left=�6�   $��	22,39,0,0q�6�   $	��Top=�6  $��75��6(    
buttonInfo#��[ 63  y�6   �� $ 	IsChecked�$	 
MultiClass��#
 [ ��6�   �� $ AddClass�63  �� 5#      -   5        $��button2��5#     $��Usuń��6�   $��2��6�   $
��Left=�6�   $��	22,68,0,0q�6�   $	��Top=�6  $��75��6*  #��[ 65  y�6   �� $ 	IsChecked�$	 
MultiClass��#
 [ ��6�   �� $ RemoveClass�65  5$      �� 5%      $��Policz wg, rasy i klasy:��6   $��1��67   $��28��6D   $
��Left=�6P   $��	532,2,0,0q�6k   $��Center=�6   �� 5&      -   5        $��button3��5&   �   $��Policz��6?   $��1��6P   $
��Left=�6^   $��	664,3,0,1q�6y   $��Center=�6�   $��75��6�   #��[ 6�   ��6   �� $ CalculateInitial�6�   � 5(      $��2��6    5)      �� 5*      �� 5+        5-      �� 5.      $	��30* �6$   �� 5/      $	��12* �6$   .+�51      �� $��5q�6   $��Stretch=�6$   $��Stretch=�6B   $��1q�6^   
���   �._�52      � $��Stretch=�6   $��Stretch=�69   ���	#FFE8FFE8  53      �� 54      $��60 �6,   �� 55      $��40 �6,   �� 56      $��40 �6,   �� 57      $��40 �6,   �� 58      $��40 �6,   �� 59      $��40 �6,   �� 5:      $��40 �6,   �� 5;      $	��22* �6,   .+�5=      �� $��Center=�6%   $��CECHY GŁÓWNE��6B   $	��Top=�6V   $��16(�6n   $
��Bold"�6|   $	��0,5q�6�   �� 5?      -   5        $��
textBlock1��5?   S   $
��Left=�6$   $��
150,43,0,0q�6?   $��Bazowe��6e   $	��Top=�6s   $��Italic$�6�   $��11(�6�   �� 5@      -   5        $��
textBlock2��5@   t   $��11(�6$   $��Italic$�62   $
��Left=�6E   $��
234,43,0,0q�6`   $��	Bieżące��6�   $	��Top=�6�   �� 5C      $��1��6!   $��Siła:��6.   $
��Left=�6?   $��Center=�6Z   $��18(�6u   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5D      $��1��6"   $��23��6R   $
��Left=�6^   $��Center=�6y   $��87��6�    �� TextAlignment$ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ BaseStrength�6�   �� 5E      $��1��6    $
��Left=�6W   $	��Top=�6r   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentStrength�6  �� 5F      $��1��6    $
��Left=�6U   $	��Top=�6p   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ StrengthBonus�6  ,   &DnDHelper.GUI.WPF.Controls.BonusHelper  5G      $��1��6`   $
��Left=�6m   $��Center=�6�   $��	345,0,0,0q�6�      Bonuses 6)   �� $ StrengthBonuses�$ OneWay=�6�   �� 5J      $��2��6!   $��Zręczność:��6.   $
��Left=�6D   $��Center=�6_   $��18(�6z   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5K      $��2��6    $
��Left=�6V   $	��Top=�6q   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ DexterityBonus�6  �� 5L      $��2��6"   $��23��6S   $
��Left=�6_   $��Center=�6z   $��87��6�   $ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ BaseDexterity�6�   �� 5M      $��2��6    $
��Left=�6X   $	��Top=�6s   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentDexterity�6    5N      $��2��6S   $
��Left=�6`   $��Center=�6|   $��	345,0,0,0q�6�    6)   �� $ DexterityBonuses�6�   �� 5Q      $��3��6!   $��	Kondycja:��6.   $
��Left=�6C   $��Center=�6^   $��18(�6y   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5R      $��3��6"   $��23��6V   $
��Left=�6b   $��Center=�6}   $��87��6�   $ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ BaseConstitution�6�   �� 5S      $��3��6    $
��Left=�6[   $	��Top=�6v   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentConstitution�6  �� 5T      $��3��6    $
��Left=�6Y   $	��Top=�6t   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ ConstitutionBonus�6    5U      $��3��6V   $
��Left=�6c   $��Center=�6   $��	345,0,0,0q�6�    6)   �� $ ConstitutionBonuses�6�   �� 5X      $��4��6!   $��Inteligencja:��6.   $
��Left=�6G   $��Center=�6b   $��18(�6}   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5Y      $��4��6"   $��23��6V   $
��Left=�6b   $��Center=�6}   $��87��6�   $ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ BaseIntelligence�6�   �� 5Z      $��4��6    $
��Left=�6[   $	��Top=�6v   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentIntelligence�6  �� 5[      $��4��6    $
��Left=�6Y   $	��Top=�6t   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ IntelligenceBonus�6    5\      $��4��6V   $
��Left=�6c   $��Center=�6   $��	345,0,0,0q�6�    6)   �� $ IntelligenceBonuses�6�   �� 5_      $��5��6!   $��Mądrość:��6.   $
��Left=�6B   $��Center=�6]   $��18(�6x   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5`      $��5��6"   $��23��6P   $
��Left=�6\   $��Center=�6w   $��87��6�   $ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ 
BaseWisdom�6�   �� 5a      $��5��6    $
��Left=�6U   $	��Top=�6p   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentWisdom�6  �� 5b      $��5��6    $
��Left=�6S   $	��Top=�6n   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ WisdomBonus�6    5c      $��5��6P   $
��Left=�6]   $��Center=�6y   $��	345,0,0,0q�6�    6)   �� $ WisdomBonuses�6�   �� 5f      $��6��6!   $��	Charyzma:��6.   $
��Left=�6C   $��Center=�6^   $��18(�6y   $
��Bold"�6�   $��5,0,0,0q�6�   �� 5g      $��6��6"   $��23��6R   $
��Left=�6^   $��Center=�6y   $��87��6�   $ Center=�6�   $
��Bold"�6�   $��Center=�6�   $��	115,0,0,0q�6�   ��6/   �� $ BaseCharisma�6�   �� 5h      $��6��6    $
��Left=�6W   $	��Top=�6r   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FF232383$��	230,0,0,0q�6  ��6-   �� $ CurrentCharisma�6  �� 5i      $��6��6    $
��Left=�6U   $	��Top=�6p   $��38��6�   $��Center=�6�   $��Center=�6�   $��18(�6�   $
��Bold"�6�   ���	#FFC42B4C$��	290,0,0,0q�6  ��6-   �� $ CharismaBonus�6    5j      $��6��6R   $
��Left=�6_   $��Center=�6{   $��	345,0,0,0q�6�    6)   �� $ CharismaBonuses�6�   �� 5l      -	   5        $��
textBlock3��5l   t   $��11(�6$   $��Italic$�62   $
��Left=�6E   $��
295,42,0,0q�6`   $��Bonus��6�   $	��Top=�6�   �� 5m      $��Pozostałe punkty:��6    $��18(�6<   $
��Bold"�6J   $
��Left=�6\   $��65,0,0,2q�6w   $��Center=�6�   $��36��6�   $��7��6�     5n      -
   5        $ additiveControl3��5n   �   $��7��6j   $
��Left=�6w   $��	233,8,0,0q�6�   $	��Top=�6�   $	��138��6�    6-   �� $ AvailableMainSkillPoints�$ TwoWay=�6�   5o      5p      �� 5s      $��5q�6   $��1��6$   $	��Top=�62   $��1q�6J   
���   �._�5t      � $��Stretch=�6   $	��Top=�69   ���	#FFF2E3CA$	��120��6h   .+�5u      �� $
��Bold"�6    $
��Left=�62   $��16(�6M   $��5,0,0,64q�6[   $��68��6m   .��6x   RZUTY6   �� 5v      -   5        $��
textBlock4��5v   t   $��11(�6$   $��Italic$�62   $
��Left=�6E   $��
139,10,0,0q�6`   $��Bazowe��6�   $	��Top=�6�   �� 5w      -   5        $��
textBlock5��5w   t   $��11(�6$   $��Italic$�62   $
��Left=�6E   $��
223,10,0,0q�6`   $��	Bieżące��6�   $	��Top=�6�   �� 5y      $��Wola:��6    $��16(�60   $
��Bold"�6>   $
��Left=�6P   $��	61,0,0,57q�6k   $��Bottom=�6~   $��35��6�   $��53��6�   �� 5z      $
��Bold"�6"   $��22��64   $
��Left=�6@   $��115,28,0,64q�6[   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��87��6�   ��6p   �� $ BaseWillThrow�6�   �� 5{      $��18(�6J   $
��Bold"�6X   ���	#FF232383$
��Left=�6�   $��Center=�6�   $��
219,23,0,0q�6�   $	��Top=�6�   $��Center=�6�   $��38��6  ��6    �� $ CurrentWillThrow�6    5|      $��Right=�6S   $��
0,24,74,61q�6o   $��Center=�6�   $��29��6�   $��29��6�    6)   �� $ WillThrowBonuses�6�   �� 5~      $��Refleks:��6    $��16(�63   $
��Bold"�6A   $��35��6S   $
��Left=�6_   $��
46,50,0,29q�6z   $��Center=�6�   $��68��6�   �� 5      $
��Bold"�6"   $��22��64   $
��Left=�6@   $��115,56,0,36q�6[   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��87��6�   ��6p   �� $ BaseReflexThrow�6�   �� 5�      $��18(�6L   $
��Bold"�6Z   ���	#FF232383$
��Left=�6�   $��Center=�6�   $��
219,51,0,0q�6�   $	��Top=�6�   $��Center=�6�   $��38��6  ��6    �� $ CurrentReflexThrow�6    5�      $��29��6U   $��Right=�6a   $��
0,52,74,33q�6}   $��Center=�6�   $��29��6�    6)   �� $ ReflexThrowBonuses�6�   �� 5�      $��Wytrwałość:��6    $��16(�66   $
��Bold"�6D   $��35��6V   $
��Left=�6b   $��	11,78,0,1q�6}   $��Center=�6�   $	��103��6�   �� 5�      $
��Bold"�6"   $��22��64   $
��Left=�6@   $��
115,84,0,8q�6[   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��87��6�   ��6o   �� $ BaseEnduranceThrow�6�   �� 5�      $��18(�6O   $
��Bold"�6]   ���	#FF232383$
��Left=�6�   $��Center=�6�   $��
219,79,0,0q�6�   $	��Top=�6�   $��Center=�6�   $��38��6  ��6    �� $ CurrentEnduranceThrow�6    5�      $��29��6X   $��Right=�6d   $��	0,80,74,5q�6�   $��Center=�6�   $��29��6�    6)   �� $ EnduranceThrowBonuses�6�   5�      5�      �� 5�      $��1��6   
���   �$��1q�6=   $��0,5,5,5q�6Q   $��Stretch=�6b   $��Stretch=�6~   ._�5�      � $��Stretch=�6   $��Stretch=�67   ���	#FFDEF5FF  5�      �� 5�      $
��1.2* �6,   6<   �� 5�      $
��1.4* �6,   6<   �� 5�      6.   �� 5�      6.   �� 5�      6.   �� 5�      6.   �� 5�      6.   �� 5�      6.    5�      �� 5�      $��4* �6/   �� 5�      .+�5�      �� $��16(�6$   $
��Bold"�62   $��Center=�6D   $��
WYLICZALNE��6a   $	��Top=�6s   $	��106��6�   $��22��6�   $��5q�6�   �� 5�      $��11(�6$   $��Italic$�62   $��Center=�6E   $��0q�6b   $��Bazowe��6m   $��Bottom=�6{   �� 5�      $��11(�6$   $��Italic$�62   $��Right=�6E   $
��70,0q�6a   $��	Bieżące��6o   $��Bottom=�6~   �� 5�      $��1��6    $��Życie:��6-   $��18(�6>   $
��Bold"�6L   $
��Left=�6^   $	��5,0q�6y   $	��Top=�6�   $��32��6�   �� 5�      $
��Bold"�6"   $��25��64   $
��Left=�6@   $��130,2q�6[   $ Center=�6�   $	��Top=�6�   $��48��6�   #��[ 6�   $��1��6�   ��6j   �� $ BaseHP�6�   �� 5�      $��18(�6    $��32��6.   $
��Bold"�6:   ���	#FF232383$
��Left=�6c   $��200,45,0,27q�6~   $��Center=�6�   $��2��6�   ��6�   �� $ 	CurrentHP�6�   �� 5�      $��/��6    $��18(�6,   $
��Bold"�6:   $��32��6L   $
��Left=�6X   $��
181,45,0,0q�6s   $	��Top=�6�   $��19��6�   $��2��6�     5�      $��1��6-   $��Right=�6:   $��0,0,10,2q�6V   $��Bottom=�6�   $	��174��6�    6h   �� $
 Life�$ TwoWay=�6�   �� 5�      $	��>HP��6#   $��16��61   $
��Left=�6=   $��
221,33,0,0q�6X   $	��Top=�6l   $��48��6�   $��1��6�    6�   �� $ AllowLifeOverflow�$ TwoWay=�6�     5�      $��1��6)   $��1��66   $��Center=�6v   $��Center=�6�    6F   �� $ 	HPBonuses�$ OneWay=�6�   �� 5�      $��2��6    $	��KP:��6-   $��18(�6;   $
��Bold"�6I   $��32��6[   $
��Left=�6g   $��5q�6�   $��Center=�6�   �� 5�      $��2��6    $��18(�6I   $��32��6W   $
��Bold"�6c   ���	#FF232383$
��Left=�6�   $��	190,0,0,0q�6�   $��Center=�6�   ��6-   �� $ AC�6�     5�      $��2��6)   $��1��66   $��Center=�6v   $��Center=�6�    6F   �� $ 	ACBonuses�$ OneWay=�6�   �� 5�      -   5        $��button4��5�   x   $��2��6!   $��Zmień��6.   $��27��6>   $
��Left=�6J   $��	103,6,0,7q�6e   $��Center=�6�   $��75��6�   #��[ 6�   ��6�   �� $ ChangeAC��� 5�      $��3��6    $��Atak (wręcz):��6-   $��18(�6E   $
��Bold"�6S   $��32��6e   $
��Left=�6q   $��5q�6�   $��Center=�6�   �� 5�      $��3��6"   $
��Bold"�6/   $��25��6A   $
��Left=�6M   $��130,0q�6h   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��48��6�   #��[ 6�   ��6w   �� $ BaseAttackMelee�6�   �� 5�      $��3��6    $��18(�6Y   $��32��6g   $
��Bold"�6s   ���	#FF232383$
��Left=�6�   $��	190,0,0,0q�6�   $��Center=�6�   ��6-   �� $ CurrentAttackMelee�6�     5�      $��3��6)   $��1��66   $��Center=�6   $��Center=�6�   $��28��6�    6F   �� $ AttackMeleeBonuses�$ OneWay=�6�   �� 5�      $��4��6    $��Atak (dyst.):��6-   $��18(�6E   $
��Bold"�6S   $��32��6e   $
��Left=�6q   $��5q�6�   $��Center=�6�   �� 5�      $
��Left=�6"   $��48��6=   $��4��6H   $
��Bold"�6U   $��25��6g   $��130,0q�6s   $ Center=�6�   $��Center=�6�   #��[ 6�   ��6�   �� $ BaseAttackRange�6�   �� 5�      $��4��6    $��18(�6Y   $
��Bold"�6g   ���	#FF232383$
��Left=�6�   $��	190,0,0,0q�6�   $��Center=�6�   $��32��6�   ��6-   �� $ CurrentAttackRange�6�     5�      $��4��6)   $��1��66   $��Center=�6   $��Center=�6�    6F   �� $ AttackRangeBonuses�$ OneWay=�6�   �� 5�      $��5��6    $��Ile ataków:��6-   $��18(�6C   $
��Bold"�6Q   $��32��6c   $
��Left=�6o   $��5q�6�   $��Center=�6�   �� 5�      $��5��6"   $
��Bold"�6/   $��25��6A   $
��Left=�6M   $��130,0q�6h   $��Center=�6�   $ Center=�6�   $��Center=�6�   $��48��6�   #��[ 6�   ��6w   �� $ BaseAttacksCount�6�   �� 5�      $��5��6    $��18(�6Z   $
��Bold"�6h   ���	#FF232383$
��Left=�6�   $��	190,0,0,0q�6�   $��32��6�   $��Center=�6�   ��6-   �� $ CurrentAttacksCount�6�     5�      $��5��6)   $��1��66   $��Center=�6�   $��Center=�6�    6F   �� $ AttacksCountBonuses�$ OneWay=�6�   �� 5�      $��6��6    $��Inicjatywa:��6-   $��18(�6C   $
��Bold"�6Q   $��32��6c   $
��Left=�6o   $��5q�6�   $��Center=�6�   �� 5�      $��6��6"   $
��Bold"�6/   $��25��6A   $
��Left=�6M   $��	130,0,0,0q�6h   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��48��6�   #��[ 6�   ��6{   �� $ BaseInitiative�6�   �� 5�      $��6��6    $��18(�6X   $
��Bold"�6f   ���	#FF232383$
��Left=�6�   $��Center=�6�   $��	190,0,0,0q�6�   $��Center=�6�   ��6-   �� $ CurrentInitiative�6�     5�      $��6��6)   $��1��66   $��Center=�6~   $��Center=�6�    6F   �� $ InitiativeBonuses�$ OneWay=�6�   �� 5�      $��7��6    $��Szybkość:��6-   $��18(�6A   $
��Bold"�6O   $��32��6a   $
��Left=�6m   $��5q�6�   $��Center=�6�   �� 5�      $��7��6"   $
��Bold"�6/   $��25��6A   $
��Left=�6M   $��	130,0,0,0q�6h   $ Center=�6�   $��Center=�6�   $��Center=�6�   $��48��6�   #��[ 6�   ��6{   �� $ 	BaseSpeed�6�   �� 5�      $��7��6    $��18(�6S   $
��Bold"�6a   ���	#FF232383$
��Left=�6�   $��Center=�6�   $��	190,0,0,0q�6�   $��Center=�6�   $��Center=�6�   ��6-   �� $ CurrentSpeed�6�     5�      $��7��6)   $��1��66   $��Center=�6y   $��Center=�6�    6F   �� $ SpeedBonuses�$ OneWay=�6�   5�      5�      �� 5�      $��1��6   $��1��6)   $��1q�66   
���   �$��0,5,5,5q�6^   $	��Top=�6o   ._�5�      � ���	#FFE8CACA$	��Top=�62   $	��120��6J   .+�5�      �� $��16(�6$   $
��Bold"�62   $��22��6D   $��Center=�6P   $��ATAKI��6m   $	��Top=�6z   �� 5�      -   5        $��listBox1��5�   �   $��93��6"   $
��Left=�6w   $��6,23,0,0q�6�   $	��Top=�6�   $	��298��6�   ���	#FF003200��6.   �� *	Attacks+ 6N   �� *SelectedAttack+6�   �� 5�      $
��Nowy��6!   $��26��6N   $
��Left=�6Z   $��
311,23,0,0q�6u   $	��Top=�6�   $��75��6�   #��[ 6�   ��60   �� *	AddAttack+6�   �� 5�      $��Usuń��6!   $��26��6Q   $
��Left=�6]   $��
311,55,0,0q�6x   #��[ 6�   $	��Top=�6�   $��75��6�   ��60   �� *RemoveAttack+6�   �� 5�      $��Edytuj��6!   $��26��6Q   $
��Left=�6]   $��
311,87,0,0q�6x   #��[ 6�   $	��Top=�6�   $��75��6�   ��62   �� *
EditAttack+6�   5�      5�      5�      �� 5�       �� Orientation$ 
Horizontal=�6   $��1��62   y�6?   �� *
BattleMode+#
 [ .+�5�      �� $��Inicjatywa:��6   $
��Left=�6.   $��13,1,0,0q�6I   #��[  6[   $	��Top=�6}     5�      $��88��6U    6%   �� *BattleInitiative+$ TwoWay=�6U   �� 5�      $��14(�6   $��Center=�6*   $��5q�6E   Całkowita:6\   �� 5�      $��28��6<   $��14(�6H   $
��Bold"�6W   ��6   �� *TotalInitiative+6W   5�      5�      5�      5�      