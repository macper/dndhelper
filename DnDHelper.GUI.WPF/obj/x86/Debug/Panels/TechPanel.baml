   M S B A M L   `   `   `  ����  DnDHelper.GUI.WPF(    "DnDHelper.GUI.WPF.Panels.TechPanel   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      
�������.��5      � $��Stretch=�6   $��Stretch=�6'     � ColumnDefinitions  5	   
   �� 5
      $��5* �6   6,   �� 5      $��* �6   6+   .+�5   
   �� -   5        $��	LogEditor��5      $ �� HorizontalScrollBarVisibility$
 Auto=�6'   " �� VerticalScrollBarVisibility$
 Auto=�6L   $��0��6o   $��1q�6    �� 
IsReadOnly . 6�   �� 5   
   $��1��6   $��Stretch=�6%   $��Stretch=�6A   $��0,0,0,0q�6_   .+�5      �� -   5        $��label1��5   n   $��Ostatnia synchronizacja:��6   $��1��67   $��28��6G   $
��Left=�6S   6n   �� 5      $
��Bold"�6   $��Center=�6Y   $��0,0,0,5q�6v   ��6*   �� *LastSyncTime+ �� StringFormat$ f��6�   �� 5      $��Center=�65   $��Center=�6R   $��Synchronizuj��6m      
buttonInfo#��[  6�   $��	55,0,55,5q�6�   ��6   �� *Synchronize+6�   �� 5      -   5        $��	listView1��5   $   $	��300��6   $	��172��65   $��6,10,7,0q�6A   ��6S   �� *Repositories+ �� View 5      �� 5      .P�5      �� $��Repozytorium��6O    �� Width$	 100��6e    �� DisplayMemberBinding 6)   �� *Name+6e   �� 5      $��	Ile Zmian��6^   $ 60��6q    6)   �� *EntityChanges.Count+6q   5      5      �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� $��Right=�65   $��Center=�6Q   $��Odswież��6l   #��[  6~   $��5,5,8,0q�6�   ��6   �� *	Refresh+6�   �� 5      $��Right=�66   $��Center=�6R   $��Inicjalizuj��6m   #��[  6�   $��5,5,8,0q�6�   ��6   �� *
Populate+6�   5      5      5!      5"      