   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DnDHelper.GUI.WPFDnDHelper.GUI.WPF  5   E       DnDHelper.GUI.WPF.App   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      /WPFclr-namespace:DnDHelper.GUI.WPF     6E     �� 	Resources  5         buttonTemplate �� 
TargetType J� Opacity  
buttonInfo �� EndPoint �� 
StartPoint � Color � Offset 	 label  textBox �� RelativeSource �� Path	 �� Mode 	 combo
 �� DisplayMemberPath �� SelectedValuePath �� SelectedValue   BooleanToVisibilityConverter;   5DnDHelper.GUI.WPF.BooleanToInverseVisibilityConverter ' #BooleanToInverseVisibilityConverter,   &DnDHelper.GUI.WPF.BoolToColorConverter  BoolToColorConverter5   /DnDHelper.GUI.WPF.IntValueToVisibilityConverter ! IntValueToVisibilityConverter.   (DnDHelper.GUI.WPF.DamageToModelConverter 	 DamageToModelConverter0   *DnDHelper.GUI.WPF.SpellDefinitionConverter 
 SpellDefinitionConverter9   3DnDHelper.GUI.WPF.BonusesToEditorViewModelConverter % !BonusesToEditorViewModelConverter)   #DnDHelper.GUI.WPF.DurationConverter  DurationConverterA  ;DnDHelper.GUI.WPF.BattleCharacterToBackgroundColorConverter - )BattleCharacterToBackgroundColorConverter0	   *DnDHelper.GUI.WPF.EffectsToStringConverter  EffectsToStringConverter;
   5DnDHelper.GUI.WPF.CharacterTabModelToControlConverter ' #CharacterTabModelToControlConverter%   DnDHelper.GUI.WPF.DateConverter  DateConverter��%�  &	        &	 �     &	 "    &	 |    &	 F    &	     &	 ,    &	 ?    &	 R    &		 e    &	
 x    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    �� 5   
   " ��61   .R�5      �� $	��100��6   $	 0.5Q�6!   #��z��6/   #��z��6Y   #��z��6}   .��5      �� $��18(�6   $
��Bold"�6*   
���  ��#��z��6M   6p   5      5	      �� 5
   
   " ��6-   .R�5      �� $��26��6   $��75��6!   #��z��6,   ���	#FF030303��5      �� 5      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5      � $ 	#FF4BAA2E��6'   $ 0Q�69   � 5      $ 	#FF073807��6'   $ 1Q�69   5      .��5      �� $
��Bold"�6   
�������#��z��6A   6d   5      5      �� 5   
   " ��6(   .R�5      �� $��14(�6   $��26��6"   #��z��6.   5      �� 5   
   " ��6*   .R�5      �� $��14(�6   #��z��6$   $
��Bold"�6J   $��24��6\   $��2q�6�   
��� d ���6h   ��  �� *TemplatedParent+$
 Text�$	 TwoWay=�6�   5      �� 5   
   " ��6(   .R�5      �� $��24��6   $��14(�6#   $
��Bold"�61   #��z��6C   #
 z
 6�   # z 6   6o   ��  �� *TemplatedParent+$ SelectedValue�$	 TwoWay=�6  5      �� 5    
   6'     5!   
   62     5"   
   6#     5#   
   6,     5$   
   6%     5%   
   6'     5&   
   60     5'   
   6      5(   
   68   	  5)   
   6'   
  5*   
   62     5+   
   6   5,      5-      