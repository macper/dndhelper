   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   7   K GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain 6k   +    %DnDHelper.GUI.WPF.Panels.Repos.Skills   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   7   KDomain8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     6k   .��5      �   � ColumnDefinitions  5	   
   �� 5
      $	��300 �6   �� 5      $	��490 �6   .+�5   
   �� -   5        $��label1��5   [   $��Nazwa:��6   $��28��6!   $
��Left=�6-   $��	12,12,0,0q�6H   $	��Top=�6i   �� 5   
   $��23��6   $
��Left=�6   $��	66,16,0,0q�69   $	��Top=�6M   $	��226��6e   ��6q   �� *
SearchName+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $	��513��6   $
��Left=�6   $��	12,46,0,0q�6:    �� DisplayMemberPath$
 Name��6�   $	��Top=�6�   $	��280��6�   ��6M   �� *Skills+ �� SelectedValue 6l   �� *SelectedSkill+6�   �� 5   
   $��Dodaj��6   $��26��6!   $
��Left=�6-   $��217,565,0,0q�6H   $	��Top=�6z   $��75��6�      
buttonInfo#��[  6�   ��6]   �� *
AddSkill+6�   �� 5   
   $��Usuń��6   $��26��6    $
��Left=�6,   $��136,565,0,0q�6G   $	��Top=�6|   $��75��6�   #��[  6�   ��6\   �� *RemoveSkill+6�   �� 5   
   -   5        $��label2��5   j   $��Nazwa:��6   $��1��6!   $��28��61   $
��Left=�6=   $��9,16,0,0q�6X   $	��Top=�6x   �� 5   
   $��1��6   $��23��6"   $��161,20,12,0q�6.   $	��Top=�6g   ��6C   �� *SelectedSkill.Name+6g   �� 5   
   -   5        $��label3��5   Y   $��Opis:��6   $��28��6    $
��Left=�6,   $��9,46,0,0q�6G   $	��Top=�6g   $��1��6   �� 5   
   $��1��6   $	��146��6"   $
��Left=�6/   $��
161,48,0,0q�6J   $	��Top=�6�   $	��317��6�    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  �� 
AcceptsTab .  ��6^   �� *SelectedSkill.Description+6�   �� 5   
   -   5        $��label4��5   d   $��Bonusowa cecha:��6   $��28��6*   $
��Left=�66   $��	9,197,0,0q�6Q   $	��Top=�6r   $��1��6�   �� 5   
   $��1��6   $��23��6#   $
��Left=�6/   $��161,200,0,0q�6J   $
 Name��6�    �� SelectedValuePath$ Value��6�   $	��Top=�6  $	��224��6,  ��6_   �� &   DnDHelper.Domain.EnumsDictionary   
MainSkills	 �� Source#	 Z  6�   �� *SelectedSkill.BonusProperty+6,  �� 5   
   -   5        $��label5��5   e   $��Modyfikator kar:��6   $��28��6+   $
��Left=�67   $��	9,231,0,0q�6R   $	��Top=�6s   $��1��6�   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5   
   $��1��6   $
��Left=�6-   $��161,231,0,0q�6H   $	��Top=�6�   $	��138��6�   
   Value
 6]   �� *SelectedSkill.PanaltyModifier+ �� Mode$ TwoWay=�6�   �� 5   
   -   5        $��button3��5   �   $��Zapisz��6,   $��1��6=   $��26��6M   $
��Left=�6Y   $��403,268,0,0q�6t   $	��Top=�6�   $��75��6�   #��[  6�   ��6   �� *Commit+6�   5      5      