   M S B A M L   `   `   `  ����K  GDnDHelper.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.DomainDnDHelper.Domain  5   	    DnDHelper.GUI.WPF*   $DnDHelper.GUI.WPF.PopUps.EditDamages   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Hdnd8clr-namespace:DnDHelper.Domain;assembly=DnDHelper.Domain     5   	     � Title$  Edytor obrażeń��5   	   $	��300��6    $	��400��6-    � WindowStartupLocation$ CenterScreen=�69   .��5      �  � RowDefinitions 5   
   �� 5      $	��230 �6   �� 5	      6   .+�5   
   �� $��Stretch=�6   $��Stretch=�60   ��6L   �� *Damage.Elements+ �� SelectedValue 6t   �� *SelectedElement+ �� UpdateSourceTrigger$ PropertyChanged=���5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   $��30��6:   $��Stretch=�6F   .+�5      �� $��5q�6$   $��Center=�6/   	Kostka:6S   �� 5      $��	TestCombo��6#   $��2q�64   $��Center=�6?   $��50��6Z   $��23��6e    �� DisplayMemberPath$
 Name��6�    �� SelectedValuePath$ Value��6�   ��6q   �� &    DnDHelper.Domain.EnumsDictionary   
BonesTypes	 �� Source#	 Z  6�   �� *
MaxValue+
 �� Mode$
 TwoWay=�6  �� 5      $��5q�6$   $��Center=�6/   Ile:6P   �� 5      $��2q�6"   $��23��6-   $��40��69   $��Center=�6D   ��6_   �� *Count+$
 TwoWay=��� 5      $��Center=�6$   $��5q�6?   Typ:6P   �� 5      $	��120��6#   $��23��6/   $��2q�6;   $
 Name��6�   $ Value��6�   ��6F   ��    DamageTypes#	 Z  6�   �� *
DamageType+$
 TwoWay=�6�   5      5      5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��Stretch=�6;   $��Right=�6W   $��0,0,5,0q�6s   .+�5      �� $��Dodaj��6-   $��26��6=   $��Center=�6I   $��Center=�6f   $��75��6�      
buttonInfo#��[  6�   ��6   �� *Add+6�   �� 5      $��Usuń��60   $��26��6?   $��Center=�6K   $��Center=�6h   $��75��6�   #��[  6�   $��5,0,0,0q�6�   ��6   �� *Remove+6�   �� 5      $��OK��60   $��26��6=   $��Center=�6I   $��Center=�6f   $��75��6�   #��[  6�   $��5,0,0,0q�6�   ��6   �� *Commit+6�   5      5      5       