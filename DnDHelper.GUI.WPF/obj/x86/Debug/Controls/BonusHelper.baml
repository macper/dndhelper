   M S B A M L   `   `   `  ����  DnDHelper.GUI.WPF,    &DnDHelper.GUI.WPF.Controls.BonusHelper   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        $��helperControl��5   7   .��5      � $��25��6   $��25��6     � ToolTip  5	   
   _� 5
       � DataContext 6   ��  �� Path$ PlacementTarget.Parent� �� Self �� RelativeSource# Z .��5      �� .+�5      �� $
��Bold"�6    	Bonusy:6;   �� 5      $��5q�6    y�6+   �� $ 	NoBonuses�    BooleanToVisibilityConverter �� 	Converter# [  Brak6�   �� 5      $��5q�6#   ��6.   �� $ Bonuses���5      �� 5      .R�5   "   �� .+�5   &   �� $
��Bold"�6�   y�60   �� $ FullDescriptionMode� �� *FindAncestor+ �� AncestorType$ UserControlL� ' #BooleanToInverseVisibilityConverter# [ ��6�   �� $ ShortDescription���6!  �� $ 
IsPositive�  BoolToColorConverter# [ 5   '   �� 5   &   $
��Bold"�6�   y�60   �� $ FullDescriptionMode� �� *FindAncestor+$ UserControlL�# [  ��6�   �� ��6  �� $ 
IsPositive�# [ 5   '   5   #   5      5      5      5      .+�5   
   A� $��25��6   $��25��6   ���	#FF444490
���   �5      �� 5    
   
�������$��Center=�6'   $��Center=�6B   $��20(�6_   ?6p   5!      5"      