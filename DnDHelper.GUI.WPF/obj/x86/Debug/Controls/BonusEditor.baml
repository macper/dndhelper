   M S B A M L   `   `   `  ����K  GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]@clr-namespace:DnDHelper.Domain.Bonuses;assembly=DnDHelper.DomainDnDHelper.Domain.Bonuses  5       DnDHelper.GUI.WPF,   &DnDHelper.GUI.WPF.Controls.BonusEditor   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Pdnd@clr-namespace:DnDHelper.Domain.Bonuses;assembly=DnDHelper.Domain     5        � 	Resources  5	       �� DataType(   "DnDHelper.Domain.Bonuses.BaseBonus �� Orientation J� Opacity �� Triggers �� Value �� Binding �� 
TargetName �� Property	 �� Value��%�  (��       5
      *, +)�� 6
   "  6   .R�5      �� $��1q�6   ���	#FF293B23._�5      �� $	��row��6   $ 
Horizontal=�6(   $��Stretch=�6A   $��30��6_   $	 0.7Q�6k   .+�5      �� $��14(�66   $
��Bold"�6D   $��Center=�6V   $��10,0,0,0q�6q   ��6    �� *Name+6q   �� 5      $��14(�6=   $
��Bold"�6K   $��Center=�6]   $	��5,0q�6x   ��6    �� *Description+6x   5      5       5      �� 5      $
 True��6=    6   �� *
IsPositive+.a�5      �� $	 row��6    � ��
	 ����6D   5      �� 5      $ False��6=    6   �� *
IsPositive+.a�5      �� $	 row��6    � ��	 �	#FFE2ABAB6D   5      5      5      .��5      � .+�5   
   �� $��Bonusy:��6   $��Stretch=�6%   $��Stretch=�6D   .��5      � 
 � ColumnDefinitions
 5      �� 5      $��3* �6'   �� 5       .+�5"      �� $��Stretch=�6b   ��6   �� *	Bonuses+ �� SelectedValue 6:   �� *SelectedBonus+5#      �� 5$      $��1��6   $��Center=�6-   .+�5%      �� $��Dodaj��6   $��26��6J   $��75��6V   $��5q�6a      
buttonInfo#��[  6l   ��6-   �� *
AddBonus+6l   �� 5&      $��Edytuj��6   $��26��6L   $��75��6X   $��5q�6c   #��[  6n   ��6.   �� *	EditBonus+6n   �� 5'      $��Usuń��6   $��26��6L   $��75��6X   $��5q�6c   #��[  6n   ��6,   �� *RemoveBonus+6n   5(      5)      5*      5+      5,      