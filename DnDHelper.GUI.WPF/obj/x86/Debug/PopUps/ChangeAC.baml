   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   3   '    !DnDHelper.GUI.WPF.PopUps.ChangeAC   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   3     � Title$  	Zmień KP��6	   $	��243��6   $	��383��6'   .��5      � .+�5   
   �� -   5        $��label1��5   g   $��Naturalny pancerz:��6   $��28��6-   $
��Left=�69   $��	12,21,0,0q�6T   $	��Top=�6u   �� 5   
   -   5        $��label2��5   \   $��Bazowe:��6   $��28��6"   $
��Left=�6.   $��	189,0,0,0q�6I   $	��Top=�6j   $��Italic$�6�   �� 5   
   -   5        $��label3��5   d   $��
Bieżące:��6   $��Italic$�6#   $
��Left=�66   $��	299,0,0,0q�6Q   $��28��6r   $	��Top=�6~   �� 5	   
   -   5        $��label4��5	   x   $��28��6=   $
��Left=�6I   $��
308,20,0,0q�6d   $	��Top=�6�   $��14(�6�   ��6   ��  �� Path$ CurrentNaturalArmor�6�   �� 5
   
   -   5        $��label5��5
   ]   $��Pancerz:��6   $��28��6#   $
��Left=�6/   $��	12,55,0,0q�6J   $	��Top=�6k   �� 5   
   -   5        $��label6��5      $��14(�66   $��28��6D   $
��Left=�6P   $��
308,54,0,0q�6k   $	��Top=�6�   ��6   �� $ CurrentArmor�6�   �� 5   
   -   5        $��label7��5   e   $��Magiczna osłona:��6   $��28��6+   $
��Left=�67   $��	12,89,0,0q�6R   $	��Top=�6s   �� 5   
   -   5        $��label8��5   �   $��14(�6<   $��28��6J   $
��Left=�6V   $��
308,88,0,0q�6q   $	��Top=�6�   ��6   �� $ CurrentMagicShield�6�   �� 5   
   -	   5        $��label9��5   `   $��Zręczność:��6   $��28��6%   $
��Left=�61   $��
12,123,0,0q�6L   $	��Top=�6n   �� 5   
   -
   5        $��label10��5   �   $��14(�6:   $��28��6H   $
��Left=�6T   $��308,122,0,0q�6o   $	��Top=�6�   ��6   �� $ CurrentDexterity�6�   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5   
   $
��Left=�6   $��
127,22,0,0q�68   $	��Top=�6�   $	��156��6�      Value 6L   �� $ BaseNaturalArmor� �� Mode$ TwoWay=�6�     5   
   $
��Left=�6   $��
127,57,0,0q�68   $	��Top=�6z   $	��156��6�    6L   �� $ 	BaseArmor�$ TwoWay=�6�     5   
   $
��Left=�6   $��
127,92,0,0q�68   $	��Top=�6�   $	��156��6�    6L   �� $ BaseMagicShield�$ TwoWay=�6�   �� 5   
   $��OK��6   $��28��6   $
��Left=�6*   $��
12,164,0,0q�6E   $	��Top=�6y   $��76��6�      
buttonInfo#��[  6�   ��6Y   �� $ Commit�6�   �� 5   
   -   5        $��label11��5   ]   $��RAZEM:��6   $��28��6!   $
��Left=�6-   $��232,164,0,0q�6H   $	��Top=�6l   �� 5   
   -   5        $��label12��5   m   $��39��61   $��Right=�6=   $��
0,159,25,0q�6Y   $	��Top=�6|   $��20(�6�   $��46��6�   $
��Bold"�6�   ��6   �� $ TotalAC�6�   5      5      