   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:DnDHelper.GUI.WPF.PanelsDnDHelper.GUI.WPF.Panels  5   8   G(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  6j   %    DnDHelper.GUI.WPF.Panels.Battle   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5my&clr-namespace:DnDHelper.GUI.WPF.Panels     5   8   8my1(clr-namespace:DnDHelper.GUI.WPF.Controls     6j   .��5      � 
�������  � ColumnDefinitions  5	   
   �� 5
      $	��400 �6   6-   �� 5      $	��300 �6   6-   �� 5      $	��350 �6   6-   .+�5   
   �� $��Stretch=�6\   $	��Top=�6z   $	��486��6�   $��5,38,5,0q�6�   ��6   �� *	Members+ �� SelectedValue 63   �� *SelectedMember+��5      �� 5      .R�5      �� $��30��6!   $��Stretch=�6-   $��Center=�6K    �� Orientation$ 
Horizontal=�6f   ��6   ��  -  )BattleCharacterToBackgroundColorConverter �� 	Converter# [  .+�5      �� $��Center=�6#   $��0,0,5,0q�6l    d� 	IsChecked 6>   �� *
SendNotify+ �� Mode$ TwoWay=�6   �� 5      $��Center=�6$   $��12(�6?   $
��Bold"�6M   $	��200��6u   ��6_   �� *Name+6u   �� 5      $��Center=�6$   $��12(�6?   $
��Bold"�6M   ��6_   �� *Character.Life+�� 5      $��Center=�6$   $��12(�6?   /6P   �� 5      $��Center=�6$   $��12(�6?   $
��Bold"�6M   $��0,0,5,0q�6�   ��6_   �� *Character.CurrentStats.HP+6�   �� 5      $��Center=�6$   $��Italic$�6�   $��Stretch=�6�   ��6?   �� *Character.Effects+  EffectsToStringConverter# [ 6�   5      5       � ContextMenu 5      �� 5      .@�5      m� $
��Atak��6:    m� Command 6   �� *Attack+6:   m� 5      $��Atak okazyjny��6E    6   �� *OpportunityAttack+6E   m� 5      $��Zadaj obrażenia��6B    6   �� *DoDamageDirect+6B   5       5"      �� 5#   
   -   5        $��label3��5#   G   $��Tura:��6   $��28��6    $
��Left=�6,   $	��Top=�6U   $��11,4,0,0q�6m   �� 5$   
   -   5        $��label4��5$   b   $��28��6)   $
��Left=�65   $��42,4,0,0q�6P   $	��Top=�6p   $��14(�6�   $
��Bold"�6�   ��6   �� *Turn+6�   �� 5%   
   -   5        $��button1��5%   }   $��
Nowa bitwa��6/   $��26��6D   $
��Left=�6P   $��75,6,0,0q�6k   $	��Top=�6�   $��75��6�     
buttonInfo#��[ 6�   ��6   �� *	NewBattle+6�   �� 5&   
   -   5        $��button6��5&   u   $��Start��6+   $��26��6;   $
��Left=�6G   $��	154,6,0,0q�6b   #��[ 6�   $	��Top=�6�   $��75��6�   ��6   �� *Start+6�   �� 5'   
   -   5        $��button2��5'   �   $��Dodaj/Usuń��6/   $��26��6D   $
��Left=�6P   $��315,529,0,0q�6k   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *	AddMember+6�   �� 5(   
   -   5        $��	groupBox1��5(   ]   $��1��6   $
��Atak��6#   $	��475��61   $��Stretch=�6>   $	��Top=�6n   .��5)      �   5*      �� 5+      �� 5,      $	��65* �6'   .+�5.      �� -   5        $��label5��5.   e   $��Atakujący:��6   $��28��6-   $
��Left=�69   $��6,6,0,0q�6T   $	��Top=�6s   $��2��6�   �� 5/      $��23��6   $
��Left=�6'   $��70,9,0,0q�6B    �� DisplayMemberPath$
 Name��6t   $	��Top=�6�   $	��194��6�   $��1��6�   ��6T   �� *	Members+ 6�   �� *
Attacker+	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   �� 50      -   5        $��label6��50   d   $��	Obrońca:��6   $��28��6+   $
��Left=�67   $��6,35,0,0q�6R   $	��Top=�6r   $��2��6�   �� 51      $��23��6   $
��Left=�6'   $��	70,38,0,0q�6B   $
 Name��6u   $	��Top=�6�   $	��194��6�   $��1��6�   ��6U   �� *	Members+ 6�   �� *
Defender+$	 PropertyChanged=�6�   �� 52      -	   5        $��label7��52   h   $��Modyfikator:��6   $��28��6/   $
��Left=�6;   $��6,61,0,0q�6V   $	��Top=�6v   $��2��6�   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  53      -
   5        
  Name$
 additiveControl1��53   �   $
��Left=�6&   $��	79,64,0,0q�6t   $	��Top=�6�   $	��130��6�   $��1��6�      Value 6A   �� *EnvironmentModifier+$ TwoWay=�6�   �� 54      -   5        $��label8��54   f   $��Obrażenia:��6   $��28��6-   $
��Left=�69   $��6,88,0,0q�6T   $	��Top=�6t   $��2��6�     55      -   5        $
 additiveControl2��55   z   $
��Left=�6&   $��	79,90,0,0q�6g   $	��Top=�6�   $	��130��6�   $��1��6�    6A   �� *Damage+$ TwoWay=�6�   �� 56      -   5        $��button3��56   �   $��Zadaj��6   $��26��6F   $
��Left=�6R   $��
214,90,0,0q�6m   $	��Top=�6�   $��67��6�   #��[ 6�   $��1��6�   ��6)   �� *
DoDamage+6�   �� 57      $	��200��6j   $��Stretch=�6w   $��	0,121,0,0q�6�   $	��Top=�6�   $��1��6�   ��6   �� *	Attacks+ 6;   �� *SelectedAttackHeader+��58      �� 59      .R�5:      � 
����·�y�6=   �� *
Disabled+ ' #BooleanToInverseVisibilityConverter# [  � RowDefinitions 5;   "   �� 5<   &   $��1* �64   6B   �� 5=   &   $��3* �64   6B   .+�5?   "   �� $��0��6,   $��12(�69   $
��Bold"�6G   ��6Y   �� *Header+6s   �� 5@   "   -   5        $��1��5@   +   $��Stretch=�6�   $��Stretch=�6�   $��1��6=  ��68   �� *	Attacks+ 6X   ��  �� Path$  DataContext.SelectedAttack� �� RelativeSource �� *FindAncestor+ �� AncestorType$ UserControlL���5A   &   �� 5B   *   .R�5C   .   � y�63   �� *Done+# [  5D   2   �� 5E   6   6F   �� 5F   6   6F   .+�5H   2   �� $��0��6<   $��9(�6I   ��6V   �� *Name+6n   �� 5I   2   $��1��6=   $ 
Horizontal=�6J   .+�5J   6   �� Tr:6E   �� 5K   6   $
��Bold"�6W   $	��3,0q�6i   ��6@   �� *ToHit+6x   �� 5L   6   Obż:6F   �� 5M   6   $
��Bold"�6X   ��6@   �� *Damage+6l   5N   3   5O   /   5P   +   5R   #   5S      5T      5V      � 5W      $��1��6   $��2��6'   $
��Left=�6;   $	��125��6V   $��	5,325,0,0q�6c   $	��Top=�6v   $	��270��6�     5X      �� 5Y      6-   �� 5Z      6-   .+�5\      �� ���	#FFE7FFDB��6#   �� *AttackInfo.AttackerModifiers+��5]      �� 5^      .R�5_   "   � 5`      ��5b      �� 5c      .R�5d   "   �� $��2q�6,   $��10(�67    �� Padding$ 4q�6E   
���������6d   �� $ Positive�  BoolToColorConverter# [ ��6�   �� *Name+ � ToolTip 6�   �� *Description+5e      5g      �� 5h      $��1��6#   ���	#FFF5D3D3��63   �� *AttackInfo.DefenderModifiers+��5i      �� 5j      .R�5k   "   � 5l      ��5n      �� 5o      .R�5p   "   �� $��2q�6,   $��10(�67   $ 4q�6E   
���������6d   �� $ Positive�# [ ��6�   �� *Name+ 6�   �� *Description+5q      5s      5t      5u      5v      �� 5x   
   -   5        $��button5��5x   �   $��	Następny��61   $��26��6D   $
��Left=�6P   $��235,529,0,0q�6k   #��[ 6�   $	��Top=�6�   $��75��6�   ��6   �� *
NextMember+6�   �� 5y   
   $��1��6   $
��Left=�6(   $��Stretch=�6C   $��5��6_   �� 5z   
   $��2��6   $��Efekty��6#   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   $	��396��6y   $	��280��6�   .��5{      � .+�5|      �� $	��273��6   $
��Left=�6(   $	��Top=�6D   $	��258��6\    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��0,10,0,0q�6�   ��6h   �� *	Effects+ 6�   �� *SelectedEffect+��5}      �� 5~      .R�5      �� $ 
Horizontal=�6)   $��30��6B   .+�5�   "   �� $	��140��6,   $��12(�68   $
��Bold"�6F   $��Center=�6X   ��6s   �� *InstanceName+6�   �� 5�   "   $��80��6,   $��12(�67   $
��Bold"�6E   $��Center=�6W   ��6r   �� *
Duration+  DurationConverter# [ 6�   5�      5�      5�      �� 5�      $��Dodaj��67   $��26��6G   $
��Left=�6S   $��103,288,0,0q�6n   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *	AddEffect+6�   �� 5�      $��Usuń��6:   $��26��6I   $
��Left=�6U   $��183,288,0,0q�6q   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *RemoveEffect+6�   �� 5�      -   5        $��button4��5�   �   $��Globalne��6   $��26��6L   $
��Left=�6X   $��183,316,0,0q�6s   $	��Top=�6�   $��75��6�   #��[ 6�   ��6,   �� *ShowEffects+6�   �� 5�      $
��Left=�6   $��
10,348,0,0q�67   $
��Wrap=�6K   $��Skrypt:��6_   $	��Top=�6n   �� 5�      $
��Left=�6   $��
51,346,0,0q�66   $	��Top=�6J   $	��127��6b   $
 Name��6�    6n   �� *SelectedScript+��6�   �� *CustomScripts+6�   �� 5�      $��Wykonaj��6   $
��Left=�6M   $��183,344,0,0q�6h   $	��Top=�6}   $��75��6�   #��[ 6�   ��6+   �� *ExecuteScript+6�   �� 5�      $��Kopiuj��68   $��26��6I   $
��Left=�6U   $��
23,288,0,0q�6p   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *
CopyEffect+6�   5�      5�      �� 5�   
   $��1��6   $��Szybkie przedmioty��6#   $
��Left=�6?   $��
10,480,0,0q�6Z   $	��Top=�6n   $	��160��6�   $	��280��6�   .��5�      � .+�5�      �� $
��Left=�6   $��	10,10,0,0q�67   $
��Wrap=�6J   $��Pocisk:��6^   $	��Top=�6m   �� 5�      $ 	Item.Name��6<   $
��Left=�6�   $��	66,10,0,0q�6�   $	��Top=�6�   $	��130��6�   ��6   �� *
Missiles+ 6Z   �� *SelectedMissile+6�     5�      $
��Left=�6g   $��
201,10,0,0q�6�   $	��Top=�6�   $��67��6�    6&   �� $" SelectedMissile.Item.Charges�$ TwoWay=�6�   �� 5�      $
��Left=�6   $��	10,40,0,0q�67   $
��Wrap=�6J   $��	Mikstura:��6^   $	��Top=�6o   �� 5�      $ 	Item.Name��6;   $
��Left=�6�   $��	66,40,0,0q�6�   $	��Top=�6�   $	��130��6�   ��6   �� *	Potions+ 6Y   �� *SelectedPotion+6�     5�      $
��Left=�6f   $��
201,40,0,0q�6�   $	��Top=�6�   $��67��6�    6&   �� $! SelectedPotion.Item.Charges�$ TwoWay=�6�   �� 5�      $
��Left=�6   $��	10,70,0,0q�67   $
��Wrap=�6J   $��Różdżka:��6^   $	��Top=�6n   �� 5�      $ 	Item.Name��6;   $
��Left=�6�   $��	66,70,0,0q�6�   $	��Top=�6�   $	��130��6�   ��6   �� *	Staffes+ 6Y   �� *SelectedStaff+6�     5�      $
��Left=�6f   $��
201,70,0,0q�6�   $	��Top=�6�   $��67��6�    6&   �� $  SelectedStaff.Item.Charges�$ TwoWay=�6�   5�      5�      �� 5�   
   $��Nazwa:��6   $��2��6!   $
��Left=�61   $��
10,411,0,0q�6L   $	��Top=�6`   $��59��6x   �� 5�   
   $��2��6   $
��Left=�6"   $��23��6=   $��
74,414,0,0q�6I    �� TextWrapping$
 Wrap=�6]   $	��Top=�6�   $	��216��6�   ��6q   �� *
BattleName+6�   5�      5�      