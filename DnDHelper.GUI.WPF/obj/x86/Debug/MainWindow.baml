   M S B A M L   `   `   `  ����  DnDHelper.GUI.WPF"    DnDHelper.GUI.WPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  DnD Helper V. 2.0��5   	   $	��800��6#   $
��1240��60    � WindowStartupLocation$ CenterScreen=�6=    � WindowState$ 	Maximized=�6b   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5
   
   �� 5      $��60 �6   6*   �� 5      $��* �6   6)   �� 5      $��22 �6   6*   .+�5   
   � ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ Black��6#    � Offset$ 0Q�61   � 5      $ 	#FF609671��6#   $ 1Q�65   5       5      �� 5      6%   �� 5      6%   .+�5      �� 	 �� Orientation$	 
Horizontal=�6   .+�5      ��    buttonTemplate#��[  6   
��� ���$
��Tech��6{   ��6D   �� *MenuItemChange+.��6�   TECH6�   �� 5      #��[  6   
��� ���$��General��6{   ��6D   �� *MenuItemChange+.��6�   	OGÓLNE6�   �� 5      #��[  6   
��� ���$
��Char��6{   ��6D   �� *MenuItemChange+.��6�   CHAR6�   �� 5      #��[  6   
��� ���$��Battle��6{   ��6D   �� *MenuItemChange+.��6�   BITWA6�   �� 5      #��[  6   
��� ���$��Repos��6{   ��6D   �� *MenuItemChange+.��6�   REPO6�   5       �� 5!      $��1��6   $��Right=�6%   #��[  6A   
����  �$
��+EXP��6�   ��6~   �� *AddExperience+6�   5"      ]� 5#   
   -   5        $��PanelHolder��5#   1   $��0��6   $��1��6$   $��Stretch=�6D   $��Stretch=�6`   5$      �� 5%   
   $��0��6   $��2��6%   
���   �.+�5&      �� .@�5'      �� .��5(      �� $��2,0,2,2q�6    Status69   5)      5*      5+      5,      5-      