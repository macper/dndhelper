   M S B A M L   `   `   `  ����L  HDnDHelper.GUI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:DnDHelper.GUI.WPF.ControlsDnDHelper.GUI.WPF.Controls  5   :   .    (DnDHelper.GUI.WPF.PopUps.AddBattleMember   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   7my(clr-namespace:DnDHelper.GUI.WPF.Controls     5   :     � Title$  Edycja członków��6	   $	��490��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�6n   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $	��250 �6   �� 5	      $	��300 �6   .+�5   
   �� $��23��6   $��Center=�6    $��5q�6=   $��Grupy��6I   $	��Top=�6V   $
��Bold"�6n   �� 5   
   $��23��6Y   $
��Left=�6e   $��	12,25,0,0q�6�   $	��Top=�6�   $	��227��6�   ��6   �� *GroupSearchName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
    �� DisplayMemberPath$
 Name��6Y   $	��355��6r   $
��Left=�6   $��	12,54,0,0q�6�   $	��Top=�6�   $	��227��6�   ��6   �� *Groups+ �� SelectedValue 61   �� *SelectedGroup+6�   �� 5   
   $
��Bold"�6   $��23��6&   $��Center=�62   $��5q�6O   $��Postacie��6Z   $	��Top=�6j   $��1��6�   �� 5   
   $��23��6\   $
��Left=�6h   $��	11,25,0,0q�6�   $	��Top=�6�   $	��227��6�   $��1��6�   ��6   �� *CharacterSearchName+$ PropertyChanged=�6�   �� 5   
   $
 Name��6a   $	��355��6z   $
��Left=�6�   $��	11,54,0,0q�6�   $	��Top=�6�   $	��227��6�   $��1��6�   ��6   �� *
Characters+ 65   �� *SelectedCharacter+6�   �� 5   
   -   5        $��label1��5   ]   $��Inicjatywa:��6   $��2��6&   $��28��66   $
��Left=�6B   $	��Top=�6k   $��	4,332,0,0q�6�   �� 5   
   $
��Bold"�6   $��23��6&   $��Center=�62   $��5q�6O   $��Wybrane��6Z   $	��Top=�6i   $��2��6�   0   *DnDHelper.GUI.WPF.Controls.AdditiveControl  5   
   -   5          Name$ additiveControl1��5   �   $��2��6G   $
��Left=�6W   $��
67,333,0,0q�6r   $	��Top=�6�   $��91��6�      Value 6   �� *
Initiative+ �� Mode$ TwoWay=�6�   �� 5   
   -   5        $��button1��5   �   $��Dodaj��6/   $��2��6?   $��26��6O   $
��Left=�6[   $��	4,364,0,0q�6v   $	��Top=�6�   $��77��6�      
buttonInfo#��[  6�   ��6   �� *	AddMember+6�   �� 5   
   $��2��6[   $	��301��6k   $
��Left=�6x   $��4,25,0,0q�6�   $
 Name��6�   $	��Top=�6�   $	��262��6�   ��6   �� *	Members+ 62   �� *SelectedMember+6�   �� 5   
   -   5        $��button2��5   �   $��Kopia��63   $��2��6C   $��26��6S   $
��Left=�6_   $��
83,364,0,0q�6z   $	��Top=�6�   $��75��6�   #��[  6�   ��6   �� *AddMemberCopy+6�   �� 5   
   -   5        $��button3��5   ~   $��Usuń��63   $��26��6B   $
��Left=�6N   $��191,334,0,0q�6i   #��[  6�   $	��Top=�6�   $��75��6�   $��2��6�   ��6   �� *RemoveMember+6�   �� 5   
   -   5        $��button4��5   u   $��OK��6,   $��26��69   $
��Left=�6E   $��207,423,0,0q�6`   #��[  6�   $	��Top=�6�   $��75��6�   $��2��6�   ��6   �� *Commit+6�   �� 5   
   $��2��6   $��Doklej do nazwy��6#   $
��Left=�6<   $��163,367,0,0q�6W   $	��Top=�6l   $��51��6�   $	��119��6�   .��5      �� $
��Left=�6   $��19��61   	 �� TextWrapping$
	 Wrap=�6=   $	��Top=�6m   $	��107��6�   
 J� RenderTransformOrigin$
 
0.508,0.69&�6�   $��0,7,0,0q�6�   ��6Q   �� *
NameSuffix+6�   5      5      5      