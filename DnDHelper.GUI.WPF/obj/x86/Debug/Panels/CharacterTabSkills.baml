   M S B A M L   `   `   `  ����K  GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain  5   K   L HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls 5   7   K*clr-namespace:DnDHelper.GUI.WPF.ViewModelsDnDHelper.GUI.WPF.ViewModels 6k   1   +DnDHelper.GUI.WPF.Panels.CharacterTabSkills   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      KDomain8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     6K   7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   7   A
ViewModels*clr-namespace:DnDHelper.GUI.WPF.ViewModels     6k     � 	Resources  5       �� DataType1  +DnDHelper.GUI.WPF.ViewModels.SkillViewModel �� 
IsReadOnly �� Mode �� TextWrapping0  *DnDHelper.GUI.WPF.Controls.AdditiveControl   Value,  &DnDHelper.GUI.WPF.Controls.BonusHelper   Bonuses   DnDHelper.Domain.Atut �� AcceptsReturn��%�  (��       5      *, +)(��     5      *, +)�� 5   
   "  6   .R�5	      � $��Stretch=�6   $��Stretch=�61   .+�5
      �� $��Nazwa:��6   $��2��6)   $��28��69   $
��Left=�6E   $��	10,33,0,0q�6`   $	��Top=�6s   �� 5      $��23��6   $
��Left=�6V   $��	60,37,0,0q�6q   $	��Top=�6�   $	��204��6�    . ��6&   �� *CurrentSkill.Name+$ OneWay=�6�   �� 5      $��Opis:��6   $��28��6(   $
��Left=�64   $��	10,67,0,0q�6O   $	��Top=�6b   $��2��6z   �� 5      $	��155��6   $
��Left=�6i   $��	60,67,0,0q�6�   $	��Top=�6�   $	��204��6�   $
 Wrap=�6�    . ��6'   �� *%#CurrentSkill.Definition.Description+$ OneWay=�6�   �� 5      $��Wartość bazowa:��6   $��28��62   $
��Left=�6>   $��
10,228,0,0q�6Y   $	��Top=�6m     5      $
��Left=�6%   $��144,231,0,0q�6@   $	��Top=�6~   $	��120��6�    6U   �� *	BaseValue+$ TwoWay=�6�   �� 5      $��label1��6p   $��Wartość całkowita:��6   $��28��65   $
��Left=�6A   $��
10,262,0,0q�6\   $	��Top=�6~   �� 5      $��label2��6s   $��28��67   $
��Left=�6C   $��144,260,0,0q�6^   $	��Top=�6�   $��16(�6�   $
��Bold"�6�   ��6   �� *
TotalValue+6�     5      $
��Left=�6!   $��239,262,0,0q�6<   $	��Top=�6m    6Q   �� *	Bonuses+6m   5      5      �� 5   
   "  6   .R�5      � $��Stretch=�6   $��Stretch=�61   .+�5      �� $��Nazwa:��6   $��2��6)   $��28��69   $
��Left=�6E   $��	10,33,0,0q�6`   $	��Top=�6s   �� 5      $��23��6   $
��Left=�6I   $��	60,37,0,0q�6d   $	��Top=�6w   $	��204��6�    . ��6&   �� *Name+$ OneWay=�6�   �� 5      $��Opis:��6   $��28��6(   $
��Left=�64   $��	10,67,0,0q�6O   $	��Top=�6b   $��2��6z   �� 5      $	��155��6   $
��Left=�6\   $��	60,67,0,0q�6w   $	��Top=�6�   $	��204��6�   $
 Wrap=�6�    . ��6'   �� *Definition.Description+$ OneWay=�6�   �� 5      $��Dodatkowe info:��6   $��28��62   $
��Left=�6>   $��	6,228,0,0q�6Y   $	��Top=�6l   �� 5      $��81��6   $
��Left=�6&   $��
10,252,0,0q�6A   $	��Top=�6v   $	��254��6�   $
 Wrap=�6�    . ��6U   �� *AdditionalInfo+6�   5      5      5      .��5       � $	��800��6    � ColumnDefinitions 5!   
   �� 5"      $	��300 �6   �� 5#      $	��220 �6   �� 5$      $��* �6   .+�5&   
   �� $��23��6   $��Center=�6    $��16(�6=   $��5q�6K   $��Umiejętności��6W   $	��Top=�6k   �� 5'   
   $��1��6   $��23��6$   $��Center=�60   $��16(�6M   $��5q�6[   $��Atuty��6g   $	��Top=�6t   �� 5(   
   $	��527��6   $��Stretch=�6    $	��Top=�6>   $��5,68,5,0q�6V   ��6h   �� *Skills+	 �� SelectedValue	 6�   �� *SelectedSkill+
 �� View
 5)      �� 5*      .P�5+      �� $��Nazwa��6%    �� Width$	 200��64    �� DisplayMemberBinding 6@   �� *Name+�� 5,      $��	Wartość��6%   $	 100��66    6B   �� *Value+5-      5/      �� 50   
   $��1��6   $	��506��6"   $��Stretch=�6/   $��5,68,5,0q�6M    �� DisplayMemberPath$
 Name��6�   $	��Top=�6�   ��6_   �� *Atutes+	 6~   �� *SelectedAtut+6�   �� 51   
   $��Dodaj��6-   $��1��6=   $��25��6M   $
��Left=�6Y   $��140,585,0,0q�6t   $	��Top=�6�   $��75��6�      
buttonInfo#��[  6�   ��6   �� *	AddAtut+6�   �� 52   
   $��Usuń��60   $��25��6?   $
��Left=�6K   $��
59,585,0,0q�6f   $	��Top=�6{   $��75��6�   $��1��6�   #��[  6�   ��6   �� *
RemoveAtut+6�   �� 53   
   $��2��6:   $	��408��6J   $��Stretch=�6W   $��5,20,5,0q�6u   $	��Top=�6�   ��6   �� *CurrentModel+54      �� 55   
   -   5        $��label1��55   e   $��Pozostałe punkty:��6   $��28��6,   $
��Left=�68   $��5,34,0,0q�6S   $	��Top=�6s     56   
   $
��Left=�6   $��
185,35,0,0q�68   $	��Top=�6{   $	��110��6�    6L   �� *SkillPointsLeft+$ TwoWay=�6�   �� 57   
   -   5        $��label2��57   e   $��Pozostałe punkty:��6   $��28��6,   $
��Left=�68   $��5,32,0,0q�6S   $	��Top=�6s   $��1��6�     58   
   $
��Left=�6   $��
105,34,0,0q�68   $	��Top=�6|   $	��110��6�   $��1��6�    6L   �� *AtutesPointsLeft+$ TwoWay=�6�   59      5:      