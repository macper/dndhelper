   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:DnDHelper.GUI.WPF.PanelsDnDHelper.GUI.WPF.Panels  5      G(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  6@   *    $DnDHelper.GUI.WPF.Panels.GroupsPanel   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5my&clr-namespace:DnDHelper.GUI.WPF.Panels     5      =Controls(clr-namespace:DnDHelper.GUI.WPF.Controls     6@   
�������.��5	      �   � ColumnDefinitions  5
   
   �� 5      $	��30* �6   �� 5      $	��85* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $��25 �6    �� 5      $	��69* �6    �� 5      $��32 �6    �� 5      $	��108 �6    �� 5      $
��133* �6    �� 5      $��31 �6    .+�5      �� $��0��6   $��3q�6%   �� 61   $�Grupy:��65   6F   �� 5      $��1��6   $��3,3,3,0q�6#    �� DisplayMemberPath$
 Name��6z   ��64   �� *Groups+ �� SelectedValue 6S   �� *CurrentGroup+6z   �� 5      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   .+�5      �� $��Center=�6    d� 	IsChecked 66   �� *FilterByGroup+.��6Z   	Filtruj6c   �� 5      $��2q�6      
buttonInfo#��[  6$   $��Dodaj��6K   ��6[   �� *
AddGroup+�� 5      $��2q�6   #��[  6$   $��Usuń��6K   ��6Z   �� *RemoveGroup+5      � 5      $��3��6   .+�5      �� $��Nazwa:��6   $
��Left=�6)   $��10,0,0,0q�6D   $	��Top=�6V   �� 5       $
��Left=�6   $��23��65   $��63,4,0,0q�6A    �� TextWrapping$
 Wrap=�6S   $	��Top=�6}   $	��136��6�   ��6g   �� *Name+6�   �� 5!      $��Rasa:��6   $
��Left=�6(   $��	10,26,0,0q�6C   $	��Top=�6V   �� 5"      $
��Left=�6X   $��	63,30,0,0q�6s   $	��Top=�6�   $	��136��6�   ��6   �� *Races+ 69   �� *Race+6�   �� 5#      $��Klasa:��6   $
��Left=�6)   $��	10,52,0,0q�6D   $	��Top=�6W   �� 5$      $
��Left=�6[   $��	63,55,0,0q�6v   $	��Top=�6�   $	��136��6�   ��6   �� *	Classes+ 6;   �� *Class+6�   �� 5%      $��Poziom:��6   $
��Left=�6*   $��	10,78,0,0q�6E   $	��Top=�6X   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5&      $
��Left=�6P   $��
108,79,0,0q�6k   $	��Top=�6   $��91��6�      Value 6+   �� *Level+ �� Mode$ TwoWay=�6�   5(      �� 5)      $��4��6   $��5q�6#   $
 Name��6|   ��6.   �� *
Characters+ 6Q   �� *CurrentCharacter+6|   �� 5*      $��5��6   $ 
Horizontal=�6&   $��Right=�6?   $��
-107,0,0,0q�6[   $	��316��6o   .+�5+      �� $��2q�6   #��[  6$   $��Dodaj��6K   ��6[   �� *AddCharacter+�� 5,      $��2q�6   #��[  6$   $��Kopiuj��6K   ��6\   �� *CopyCharacter+�� 5-      $��2q�6   #��[  6$   $��	Przenieś��6K   ��6^   �� *MoveCharacter+�� 5.      $��2q�6   #��[  6$   $��Usuń��6K   ��6Z   �� *RemoveCharacter+5/      50      -   'DnDHelper.GUI.WPF.Panels.CharacterPanel  51   
   -   5        	  Name$	 characterPanel��51   u   $��1��6G   $��Stretch=�6W   $��Stretch=�6�   
 � DataContext
 6   �� *CharacterViewModel+6�   �� 52   
   $��1��6   $
��Left=�6(   $��Stretch=�6C   $��5��6_   53      54      