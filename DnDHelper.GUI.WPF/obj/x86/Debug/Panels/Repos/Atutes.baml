   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   K   K GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain 6   +    %DnDHelper.GUI.WPF.Panels.Repos.Atutes   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7my(clr-namespace:DnDHelper.GUI.WPF.Controls     6K   KDomain8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     6   .��5      �   � ColumnDefinitions  5   
   �� 5	      $	��300 �6   �� 5
      $	��490 �6   .+�5   
   �� -   5        $��label1��5   [   $��Nazwa:��6   $��28��6!   $
��Left=�6-   $��	12,12,0,0q�6H   $	��Top=�6i   �� 5   
   $��23��6   $
��Left=�6   $��	66,16,0,0q�69   $	��Top=�6M   $	��226��6e   ��6q   �� *
SearchName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $	��513��6   $
��Left=�6   $��	12,46,0,0q�6:    �� DisplayMemberPath$
 Name��6�   $	��Top=�6�   $	��280��6�   ��6M   �� *Atutes+ �� SelectedValue 6l   �� *SelectedAtut+6�   �� 5   
   $��Dodaj��6   $��26��6!   $
��Left=�6-   $��217,565,0,0q�6H   $	��Top=�6y   $��75��6�      
buttonInfo#��[  6�   ��6]   �� *	AddAtut+6�   �� 5   
   $��Usuń��6   $��26��6    $
��Left=�6,   $��136,565,0,0q�6G   $	��Top=�6{   $��75��6�   #��[  6�   ��6\   �� *
RemoveAtut+6�   �� 5   
   $��Wybierz��6q   $��26��6�   $
��Left=�6�   $��
55,565,0,0q�6�   $	��Top=�6�   $��75��6�   #��[  6   y�6   ��  �� Path$ 
SelectMode�   BooleanToVisibilityConverter �� 	Converter# [ ��6�   �� *
SelectAtut+6   �� 5   
   -   5        $��label2��5   j   $��Nazwa:��6   $��1��6!   $��28��61   $
��Left=�6=   $��9,16,0,0q�6X   $	��Top=�6x   �� 5   
   $��1��6   $��23��6"   $��161,20,12,0q�6.   $	��Top=�6f   ��6C   �� *SelectedAtut.Name+6f   �� 5   
   -   5        $��label3��5   Y   $��Opis:��6   $��28��6    $
��Left=�6,   $��9,74,0,0q�6G   $	��Top=�6g   $��1��6   �� 5   
   $��1��6   $��97��6"   $
��Left=�6.   $��
161,48,0,0q�6I   $	��Top=�6�   $	��317��6�    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  �� 
AcceptsTab .  ��6]   �� *SelectedAtut.Description+6�   �� 5   
   -   5        $��button3��5   �   $��Zapisz��6,   $��1��6=   $��26��6M   $
��Left=�6Y   $��403,276,0,0q�6t   $	��Top=�6�   $��75��6�   #��[  6�   ��6   �� *Commit+6�   �� 5   
   -   5        $��label4��5   _   $��
Wymagania:��6   $��28��6%   $
��Left=�61   $��	9,170,0,0q�6L   $	��Top=�6m   $��1��6�   �� 5   
    .  .  $��70��6:   $
��Left=�6F   $��161,151,0,0q�6a   $
 Wrap=�6�   $	��Top=�6�   $	��317��6�   $��1��6�   ��6v   �� *SelectedAtut.Requirements+6�   �� 5   
   -   5        $��label5��5   \   $��Skrypt:��6   $��28��6"   $
��Left=�6.   $��	9,222,0,0q�6I   $	��Top=�6j   $��1��6�   �� 5   
   -   5        $��	groupBox1��5   �   $��1��6   $��Wybrany atut:��6�   $	��152��6�   $
��Left=�6�   $��	9,308,0,0q�6�   $	��Top=�6�   $	��469��6  y�6#   �� $ IsAtutInstaceVisible�# [ .��5      � .+�5      �� -   5        $��label6��5   p   $��Dodatkowe informacje:��6   $��28��68   $
��Left=�6D   $��6,6,0,0q�6_   $	��Top=�6~   �� 5       .  .  $��70��6B   $
��Left=�6N   $��
146,10,0,0q�6i   $
 Wrap=�6�   $	��Top=�6�   $	��305��6�   ��6}   �� *AtutInstance.AdditionalInfo+6�   �� 5      $��OK��6   $��26��6&   $
��Left=�62   $��
376,97,0,0q�6M   $	��Top=�6�   $��75��6�   #��[  6�   ��6a   �� *ConfirmSelect+6�   5      5       �� 5!   
   $��1��6   $��23��6#   $
��Left=�6/   $��161,227,0,0q�6J   $
 Name��6�   	 �� SelectedValuePath$
	 Name��6�   $	��Top=�6�   $	��236��6�   ��6_   �� *	Scripts+ 6   �� *SelectedAtut.Script+6�   �� 5"   
   $��Edytuj��61   $��26��6B   $��Right=�6N   $��
0,227,12,0q�6j   #��[  6   $	��Top=�6�   $��75��6�   $��1��6�   ��6   �� *EditScripts+6�   5#      5$      