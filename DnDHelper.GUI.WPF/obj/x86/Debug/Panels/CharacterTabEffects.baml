   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   7   2    ,DnDHelper.GUI.WPF.Panels.CharacterTabEffects   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   7   
�������.��5      � $	��800��6   $	��Top=�6   $
��Left=�6/     � ColumnDefinitions  5	   
   �� 5
      6!   �� 5      6!   .+�5   
   �� -   5        $��label1��5   d   $��Aktywne efekty:��6   $��28��6*   $
��Left=�66   $��	12,12,0,0q�6Q   $	��Top=�6r   �� 5   
   $	��530��6   $
��Left=�6    $��	12,46,0,0q�6;   $	��Top=�6N   $	��378��6f   ��6r   �� *	Effects+ �� SelectedValue 6�   �� *SelectedEffect+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   $��30��6:   .+�5      �� $	��200��6$   $��14(�60   $
��Bold"�6>   $��Center=�6P   ��6k   �� *InstanceName+6�   �� 5      $	��100��6$   $��14(�60   $��Center=�6>   Pozostały czas:6j   �� 5      $��50��6$   $��14(�6/   $
��Bold"�6=   $��Center=�6O   ��6j   �� *
Duration+   DurationConverter �� 	Converter# [  6�   5      5      5      �� 5   
   -   5        $��button1��5   {   $��Dodaj��6/   $��26��6?   $
��Left=�6K   $��315,582,0,0q�6f   $	��Top=�6�   $��75��6�     
buttonInfo#��[ 6�   ��6   �� *	AddEffect+6�   �� 5   
   -   5        $��button2��5   }   $��Usuń��62   $��26��6A   $
��Left=�6M   $��234,582,0,0q�6h   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *RemoveEffect+6�   �� 5   
   -   5        $��label2��5   k   $��Nazwa:��6   $��1��6!   $��28��61   $
��Left=�6=   $��	10,12,0,0q�6X   $	��Top=�6y   �� 5   
   $��1��6   $��23��6"   $
��Left=�6.   $��
135,16,0,0q�6I   $	��Top=�6]   $	��253��6u    �� 
IsReadOnly . ��6�   �� *SelectedEffect.InstanceName+6�   �� 5   
   $��Opis:��6   $��28��6    $
��Left=�6,   $��	10,46,0,0q�6G   $	��Top=�6Z   $��1��6r   �� 5   
   $��1��6   $��95��6"   $
��Left=�6.   $��
135,50,0,0q�6I   $	��Top=�6]   $	��253��6u    . ��6�   �� *'%SelectedEffect.Definition.Description+6�   �� 5   
   $��Czas trwania:��6   $��28��6(   $
��Left=�64   $��
10,157,0,0q�6O   $	��Top=�6c   $��1��6{   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5    
   $��1��6?   $
��Left=�6O   $��
95,158,0,0q�6j   $	��Top=�6~   $	��120��6�      OnChange 6   �� *EffectChange+   Value 6�   �� *EffectModel.Duration+ �� Mode$ TwoWay=��� 5!   
   $��Licznik:��6   $��28��6#   $
��Left=�6/   $��229,158,0,0q�6J   $	��Top=�6`   $��1��6x     5"   
   $��1��6?   $
��Left=�6O   $��283,160,0,0q�6j   $	��Top=�6�   $	��105��6�    6   �� *EffectChange+ 6�   �� *EffectModel.Counter+$ TwoWay=�,   &DnDHelper.GUI.WPF.Controls.BonusEditor  5#   
   $��1��6   $
��Left=�6)   $��
12,269,0,0q�6D   $	��Top=�6Y   $	��376��6q   $	��160��6}    � DataContext 6�   �� *EffectModel.Bonuses+�� 5$   
   -   5        $��	groupBox1��5$   z   $��1��6   $��Kustomowe skrypty��6#   $	��173��6>   $
��Left=�6K   $��
12,435,0,0q�6f   $	��Top=�6�   $	��376��6�   .��5%      � .+�5&      �� $	��144��6   $
��Left=�6'   $��6,6,0,0q�6B   	 �� DisplayMemberPath$
	 Name��6s   $	��Top=�6�   $	��241��6�   ��6S   �� *	Scripts+ 6�   �� *SelectedScript+6�   �� 5'      -   5        $��stackPanel1��5'   X   $	��144��6   $
��Left=�6*   $��	253,6,0,0q�6E   $��Stretch=�6k   $	��105��6�   .+�5(      �� -   5        $��button3��5(   ]   $��Wykonaj��6?   $��26��6Q   $��75��6l   $��5,10,5,5q�6w   #��[ 6�   ��6   �� *ExecuteScript+6�   �� 5)      -   5        $��button4��5)   Y   $��Edytuj��6<   $��26��6M   $��75��6h   $��0,0,0,5q�6s   #��[ 6�   ��6   �� *
EditScript+6�   5*      5+      5,      �� 5-   
   $��1��6   $��74��6"   $
��Left=�6.   $��135,189,0,0q�6I   $	��Top=�6�   $	��253��6�   
 �� AcceptsReturn
 . ��6^   �� *SelectedEffect.CustomText+6�   �� 5.   
   $��Dodatkowe info:��6   $��28��6*   $
��Left=�66   $��
12,209,0,0q�6Q   $	��Top=�6e   $��1��6}   5/      50      