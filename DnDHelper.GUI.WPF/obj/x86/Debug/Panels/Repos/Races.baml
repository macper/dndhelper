   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5      *    $DnDHelper.GUI.WPF.Panels.Repos.Races   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =Controls(clr-namespace:DnDHelper.GUI.WPF.Controls     5      
�������.��5	      �   � ColumnDefinitions  5
   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   ��  �� DisplayMemberPath$
 Name��6W   $
��Left=�6q   $	��535��6�   $��	10,10,0,0q�6�   $	��Top=�6�   $	��280��6�   ��6   �� *Races+ �� SelectedValue 60   �� *SelectedRace+6�   �� 5   
   $��Dodaj��6      
buttonInfo#��[  69   $
��Left=�6`   $��135,550,0,0q�6{   $	��Top=�6�   $��75��6�   ��6!   �� *Add+6�   �� 5   
   $��Usuń��6   #��[  6;   $
��Left=�6b   $��215,550,0,24q�6}   $	��Top=�6�   $��75��6�   ��6    �� *Remove+6�   �� 5   
   $��Zapisz��6   #��[  6=   $��1��6e   $
��Left=�6u   $��415,570,0,0q�6�   $	��Top=�6�   $��75��6�   ��6"   �� *Commit+6�   �� 5   
   $��1��6   $
��Left=�6$   $��	10,10,0,0q�6?   $
��Wrap=�6R   $��Nazwa:��6f   $	��Top=�6t   �� 5   
   $��1��6   $
��Left=�6"   $��23��6=   $��68,9,0,0q�6I    �� TextWrapping$
 Wrap=�6[   $	��Top=�6�   $	��242��6�   ��6o   �� *SelectedRace.Name+6�   �� 5   
   $��1��6   $
��Left=�6$   $��	10,40,0,0q�6?   $
��Wrap=�6R   $��Bazowa szybkość:��6f   $	��Top=�6~   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5   
   $��1��6Y   $
��Left=�6i   $��
130,37,0,0q�6�   $	��Top=�6�    J� RenderTransformOrigin$ 	0.944,0.2&�6�   $	��101��6�      Value 6#   �� *SelectedRace.BaseSpeed+ �� Mode$ TwoWay=�6�   ,   &DnDHelper.GUI.WPF.Controls.BonusEditor  5   
   $��1��6?   $
��Left=�6O   $��	22,79,0,0q�6j   $	��Top=�6}   $	��375��6�   $	��468��6�    � DataContext 6   �� *	Bonuses+6�   �� 5   
   $��Skrypt:��6   $��1��6"   $
��Left=�62   $��
22,464,0,0q�6M   $	��Top=�6a   �� 5   
   $��1��6    �� SelectedValuePath$
 Name��6p   $
 Name��6�   $
��Left=�6�   $��130,468,0,0q�6�   $	��Top=�6�   $	��240��6�   ��6#   �� *	Scripts+	 �� SelectedItem	 6C   �� *SelectedRace.Script+6�   �� 5   
   $��Edytuj��6   $��1��6A   $
��Left=�6Q   $��375,467,0,0q�6l   $	��Top=�6�   $��80��6�   #���   6�   ��6"   �� *
EditScript+6�   5      5      