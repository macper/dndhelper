   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:DnDHelper.GUI.WPF.PanelsDnDHelper.GUI.WPF.Panels  5   :   ,    &DnDHelper.GUI.WPF.PopUps.MoveCharacter   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5my&clr-namespace:DnDHelper.GUI.WPF.Panels     5   :     � Title$  Przenieś postać��6	   $	��169��6!   $	��323��6.    � WindowStartupLocation$ CenterScreen=�6l   .��5      � $	��137��6   $	��302��6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��150 �6    � RowDefinitions 5
   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��40 �6   .+�5   
   �� $��0��6   $��0��6$   $��5q�61   
Postać:6E   �� 5   
   $��1��6   $��0��6$   $��5q�61   $
��Bold"�6<   ��6N   ��  �� Path$ Character.Name�6u   �� 5   
   $��0��6   $��1��6$   $��5q�61   Grupa źródłowa:6M   �� 5   
   $��1��6   $��1��6#   $��5q�60    �� DisplayMemberPath$
 Name��6�   ��6;   �� $ Groups� �� SelectedValue 6_   �� $ SourceGroup�6�   �� 5   
   $��0��6   $��2��6$   $��5q�61   Grupa docelowa:6M   �� 5   
   $��2��6   $��2��6#   $��5q�60   $
 Name��6�   ��6;   �� $ Groups� 6_   �� $ DestinationGroup�6�   �� 5   
   $��Right=�6   $��2��6-   $��3��6=   $��5q�6J      
buttonInfo#��[  6U   $��Zapisz��6|   ��6�   �� $
 Save�5      5      