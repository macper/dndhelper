   M S B A M L   `   `   `  ����K  GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain  5      L HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls 5   7   *   $DnDHelper.GUI.WPF.Panels.Repos.Items   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hdnd8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     5      7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   7   
�������.��5	      �   � ColumnDefinitions  5
   
   �� 5      �� 5      $��2* �6   .+�5   
   �  � RowDefinitions 5      �� 5      �� 5      $��2* �6    �� 5      $��80 �6    .+�5      �� $��22��6   $
��Left=�6$   $��10,8,0,0q�6?   $��Nazwa:��6Q   $	��Top=�6_   �� 5      $��24��6   $
��Left=�6"   $��62,6,0,0q�6=   $	��Top=�6P   $	��172��6h   ��6u   ��  �� Path$ 
SearchName� �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��24��6   $
��Left=�6$   $��	10,34,0,0q�6?   $
��Typ:��6R   $	��Top=�6^   �� 5      $��22��6   $
��Left=�6l   $��	62,34,0,0q�6�    �� DisplayMemberPath$
 Name��6�    �� SelectedValuePath$ Value��6�   $	��Top=�6�   $	��172��6  ��6#   �� &    DnDHelper.Domain.EnumsDictionary   	ItemTypes �� Source# Z  �� SelectedValue 6�   �� $ 
SearchType�6  �� 5      $��Prototyp��6   $
��Left=�6'   $��6,56,0,0q�6B   $	��Top=�6T   	 J� RenderTransformOrigin$	 0.342,0.462&�6l   �� 5      $��	Cena (do)��6   $
��Left=�6(   $��6,80,0,0q�6C   $	��Top=�6U   �� 5      -   5        $��listBox1��5   R   $��2,0,2,2q�6   $��1��6'   $��Stretch=�64   $��Stretch=�6b   ��6~   �� $ Items� 6�   �� $ SelectedItem��� 5      $��2��6   $��Stretch=�6&   $��Center=�6B   
 �� Orientation$
 
Horizontal=�6_   .+�5      �� $��Dodaj��6   $��2��6J   $��25��6W   $��75��6c      
buttonInfo#��[  6n   $��2q�6�   ��6)   �� $ AddItem�6�   �� 5       $��Kopiuj��6   $��2��6G   $��25��6T   $��75��6`   #��[  6k   $��2q�6�   ��6*   �� *
CopyItem+6�   �� 5!      $��Usuń��6   $��2��6G   $��25��6T   $��75��6`   #��[  6k   $��2q�6�   ��6(   �� *
RemoveItem+6�   �� 5"      $��Wybierz��6�   $��26��6�   $��Right=�6�   $��5q�6�   $��Center=�6�   $��75��6�   #��[  6  y�6   �� $ 
SelectMode�   BooleanToVisibilityConverter �� 	Converter# [ ��6y   �� *
SelectItem+6  5#      �� 5$      $
��Left=�6   $��	62,60,0,0q�62   $	��Top=�6�   $	��172��6�   ��6E   �� *
Prototypes+ 6h   �� *	Prototype+6�   0  *DnDHelper.GUI.WPF.Controls.AdditiveControl  5%      $
��Left=�6I   $��
112,86,0,0q�6d   $	��Top=�6x   $	��122��6�      Value 6!   �� *
PriceMax+ �� Mode$ TwoWay=�6�   �� 5&      $��Losuj��6   $
��Left=�6D   $��159,116,0,0q�6_   $	��Top=�6t   $��75��6�   #���   6�   ��6%   �� *
RandomItem+6�   5(      �� 5)   
   $��2q�6   $��1q�6   $��1��60   
���   �$	��500��6T   ._�5*      �  � DataContext 6   �� $ ItemDetails� 5+      �� 5,      $��50 �6$   62   �� 5-      $��50 �6$   62   �� 5.      $��50 �6$   62   �� 5/      $��50 �6$   62   �� 50      $��50 �6$   62   �� 51      $	��100 �6$   63   �� 52      $	��200 �6$   63   �� 53      $��50 �6$   62   .+�55      �� $��23��6   $
��Left=�6(   $��10q�6C   $��Nazwa:��6O   $��Center=�6]   $��16(�6x   �� 56      $��24��6   $��Right=�6<   $��10q�6X   $��Center=�6d   $	��289��6     textBox#��[ 6�   ��6&   �� *Name+6�   �� 57      $��1��6   $��16(�6)   $��23��67   $
��Left=�6C   $��10q�6^   $
��Typ:��6j   $��Center=�6v   �� 58      $
 Name��6�   $ Value��6�   $��1��6�   $��23��6�   $��Right=�6�   $��10q�6�   $��Center=�6�   $	��289��6  ��6   �� # Z  6c   �� $
 Type�6  �� 59      $��2��6   $��16(�6)   $��23��67   $
��Left=�6C   $��10q�6^   $��Typ bazowy:��6j   $��Center=�6}   �� 5:      $��Jest typem bazowym��6=   $��2��6Z   $��16��6g   $
��Left=�6s   $��Center=�6�   $��168,17,0,17q�6�    d� 	IsChecked 6   �� *IsPrototype+6�   �� 5;      -   5        $��	comboBox1��5;     $��2��6�   $��23��6�   $��Right=�6�   $��10q�6�   $��Center=�6  $	��170��67   6   �� *	Prototype+��6?   �� *
Prototypes+y�6b   �� $ IsPrototype� ' #BooleanToInverseVisibilityConverter# [ 67  �� 5<      $��16(�6   $��23��6*   $
��Left=�66   $��10q�6Q   $��Cena:��6]   $��Center=�6j   $��3��6�   �� 5=      $��24��61   $��Right=�6=   $��10q�6Y   #��[ 6e   $��Center=�6�   $	��140��6�   $��3��6�   ��6   �� *Price+6�   �� 5>      $��16(�6   $��23��6*   $
��Left=�66   $��10q�6Q   $��Liczba ładunków:��6]   $��Center=�6u   $��4��6�   �� 5?      $��24��63   $��Right=�6?   $��10q�6[   #��[ 6g   $��Center=�6�   $	��140��6�   $��4��6�   ��6   �� *	Charges+6�   ,  &DnDHelper.GUI.WPF.Controls.BonusEditor  5@      $��6��6J   $��Stretch=�6X   $��Stretch=�6t    6!   �� *BonusEditorModel+6t   �� 5A      $��16(�6   $��23��6*   $
��Left=�66   $��
10,43,0,34q�6Q   $��
Specjalne:��6e   $��Center=�6w   $��5��6�   �� 5B      -   5        $��textBox1��5B   |   $��5��64   $��89��6A   $
��Left=�6M   $��
202,11,0,0q�6h   $	��Top=�6�   $	��289��6�    �� TextWrapping$
 Wrap=�6�    �� 
AcceptsTab .  �� AcceptsReturn . ��6   �� *
Specials+6�   �� 5C      $��7��6   $
 
Horizontal=�6*   $��Center=�6C   $��Right=�6^   .+�5D      �� $��Zapisz��66   $��26��6G   $��Right=�6S   $��5q�6o   $��Center=�6{   $��75��6�   #��[  6�   ��6   �� *Save+6�   5F      5H      5I      5K      5L      