   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5      K GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain 6H   ,    &DnDHelper.GUI.WPF.Panels.Repos.Classes   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =Controls(clr-namespace:DnDHelper.GUI.WPF.Controls     5      KDomain8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     6H   
�������$	��660��5   2   $	��800��6?   .��5      �   � ColumnDefinitions  5	   
   �� 5
      $	��300 �6   �� 5      .+�5   
   �� $��Klasy��6   $
��Left=�6"   $��	10,10,0,0q�6=   $	��Top=�6P   $	��468��6h   $	��280��6u   .��5      ��  �� DisplayMemberPath$
 Name��66   $
��Left=�6w   $	��446��6�   $	��Top=�6�   $	��268��6�   ��6   �� *	Classes+ �� SelectedValue 6O   �� *SelectedClass+6�   5      �� 5   
   $��Dodaj��6   $
��Left=�6>   $��215,483,0,0q�6Y   $	��Top=�6n   $��75��6�      
buttonInfo#���   6�   ��6!   �� *
AddClass+6�   �� 5   
   $��Usuń��6   $
��Left=�6@   $��135,483,0,0q�6[   $	��Top=�6p   $��75��6�   #���   6�   ��6    �� *RemoveClass+6�   � 5   
   $��1��6   $��Stretch=�6   $��Stretch=�6=    � DataContext 6Y   �� *SelectedClassViewModel+.+�5      �� $��Nazwa:��6   $��1��6%   $
��Left=�65   $��	10,10,0,0q�6P   $	��Top=�6c   �� 5      $
��Left=�6   $��23��61   $��	77,14,0,0q�6=    �� TextWrapping$
 Wrap=�6P   $	��Top=�6z   $	��155��6�   ��6d   �� *Name+6�   �� 5      $	��PW:��6   $
��Left=�6"   $��
263,11,0,0q�6=   $	��Top=�6Q   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5      $
��Left=�6I   $��
385,14,0,0q�6d   $	��Top=�6x   $��85��6�      Value 6'   �� *PW+ �� Mode$ TwoWay=�6�   �� 5      $��Atak:��6   $
��Left=�6$   $��	10,36,0,0q�6?   $	��Top=�6R   �� 5      $
 Name��6@    �� SelectedValuePath$ Value��6   $
��Left=�6�   $��	77,42,0,0q�6�   $	��Top=�6�   $	��155��6�   ��6   �� *AttackSkillRates+ 6Y   �� *AttackSkill+6�   �� 5      $��Pkt.umiejętności��6   $
��Left=�6/   $��
263,42,0,0q�6J   $	��Top=�6^     5      $
��Left=�6R   $��
385,44,0,0q�6m   $	��Top=�6�   $��85��6�    6'   �� *SkillPoints+$ TwoWay=�6�   �� 5      $��Wysokie rzuty obronne��6   $
��Left=�66   $��	10,69,0,0q�6Q   $	��Top=�6d   $��54��6|   $	��460��6�   .��5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $��10,0,0,0q�6Q   .+�5      �� $
��Wola��6   $��0,0,10,0q�6.   	 d� 	IsChecked	 6@   �� *HighWillThrow+�� 5      $��Refleks��6   $��0,0,10,0q�61   	 6C   �� *HighReflexThrow+�� 5       $��Wytrwałość��6   	 64   �� *HighEnduranceThrow+5!      5"      �� 5#      $��Skrypt:��6   $
��Left=�6&   $��
10,128,0,0q�6A   $	��Top=�6U   �� 5$      $
 Name��6X   $
 Name��6q   $
��Left=�6�   $��115,128,0,0q�6�   $	��Top=�6�   $	��158��6�   ��6   �� *	Scripts+ 67   �� *Script+6�   �� 5%      $��Edytuj��6   $
��Left=�6E   $��278,128,0,0q�6`   $	��Top=�6u   $��75��6�   #���   6�   ��6&   �� *
EditScript+6�   �� 5&      $��Dostępne czary��6   $
��Left=�6/   $��
10,155,0,0q�6J   $	��Top=�6^   $	��227��6v   $	��460��6�   .��5'      � .+�5(      �� $��Poziom postaci:��6   $
��Left=�66   $	��Top=�6Q   
 J� RenderTransformOrigin$
 -0.211,-0.038&�6i   �� 5)      $
��Left=�6~   $��98,4,0,0q�6�   $	��Top=�6�   $	��159��6�    6   �� *SelectedSpellCastingLevel+��6S   �� *SpellCastingLevels+6�   �� 5*      $
��Left=�6E   $	��135��6`   $��	10,32,0,0q�6m   $	��Top=�6�   $	��428��6�   ��6   �� *SpellCastings+��5+      �� 5,      .R�5-   "   �� $ 
Horizontal=�6-   $��Center=�6F   $��5q�6a   .+�5.   &   �� $��Center=�60   Typ:6Q   �� 5/   &   $��23��6/   $
 Name��6�   $ Value��6�   $��85��6�   $��2,0,0,0q�6�   ��6;   �� &   DnDHelper.Domain.EnumsDictionary   
SpellTypes �� Source# Z  6�   �� *Type+6�   �� 50   &   $��Center=�60   $��2,0,0,0q�6K   	Poziom:6e     51   &   $��53��6?    6J   �� *Level+$ TwoWay=��� 52   &   $��Center=�60   $��2,0,0,0q�6K   Ile:6b     53   &   $��53��6?    6J   �� *Count+$ TwoWay=�54   #   55      57      �� 58      $��Dodaj��6   $
��Left=�6Q   $��362,173,0,0q�6l   $	��Top=�6�   $��75��6�   #���   6�   ��6-   �� *AddSpellCasting+6�   �� 59      $��Dodaj��6   $
��Left=�6V   $��	262,2,0,0q�6q   $	��Top=�6�   $��75��6�   #���   6�   ��6-   �� *AddSpellCastingLevel+6�   5;      5<      �� 5=      $��Zapisz��6   $
��Left=�6A   $��395,624,0,0q�6\   $	��Top=�6q   $��75��6�   #���   6�   ��6&   �� *Commit+6�   �� 5>      $ ��Preferencje dla zdolności��6   $
��Left=�6:   $��
10,387,0,0q�6U   $	��Top=�6i   $	��232��6�   $	��460��6�   .��5?      � .+�5@      �� $
��Left=�6H   $	��180��6c   $	��Top=�6p   $	��448��6�   ��6   �� *SkillsPreference+��5A      �� 5B      .R�5C   "   �� $ 
Horizontal=�6-   $��Center=�6F   $��1q�6a   .+�5D   &   �� $	��200��60   $��Center=�6<   ��6W   �� *Name+6o     5E   &   $��90��6?    6J   �� *Wage+$ TwoWay=�5F   #   5G      5I      �� 5J      $
��Bold"�6O   $
��Left=�6a   $��400,183,0,0q�6|   $	��Top=�6�   ��6   �� * SkillPreferenceCountAlreadySet+6�   �� 5K      $��Odswież��6   $
��Left=�6Z   $��303,182,0,0q�6u   $	��Top=�6�   $��75��6�   #���   6�   ��6/   �� *RefreshSkillPreference+6�   5L      5N      5P      5R      5S      