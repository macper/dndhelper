   M S B A M L   `   `   `  ����  DnDHelper.GUI.WPF,    &DnDHelper.GUI.WPF.PopUps.GlobalEffects   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Efekty bitewne��5   	   $	��484��6    $	��931��6-    � WindowStartupLocation$ CenterScreen=�69   .��5      � .+�5   
   �� $��Efekty:��6   $��28��6"   $
��Left=�6.   $��	12,12,0,0q�6I   $	��Top=�6\   �� 5   
   $	��338��6   $
��Left=�6    $��	12,46,0,0q�6;   $	��Top=�6N   $	��273��6f   ��6r   �� *GlobalEffects+ �� SelectedValue 6�   �� *SelectedEffect+ �� View 5      �� 5	      .P�5
      �� $��Nazwa��6%    �� Width$	 180��64    �� DisplayMemberBinding 6@   ��  �� Path$ InstanceName�6u   �� 5      $��Czas trwania��6%   $ 80��6;    6F   �� $ Duration�   DurationConverter �� 	Converter# [  6�   5      5      �� 5   
   -   5        $��button1��5   }   $��Usuń��6   $��26��6A   $
��Left=�6M   $��210,390,0,0q�6h   $	��Top=�6�   $��75��6�     
buttonInfo#��[ 6�   ��6    �� *RemoveEffect+6�   �� 5   
   -   5        $��button2��5   {   $��Dodaj��6   $��26��6?   $
��Left=�6K   $��129,390,0,0q�6f   $	��Top=�6�   $��75��6�   #��[ 6�   ��6!   �� *	AddEffect+6�   �� 5   
   -   5        $��label2��5   o   $��Postacie bez tego efektu:��6   $��28��64   $
��Left=�6@   $��
388,12,0,0q�6[   $	��Top=�6}   �� 5   
   $	��338��6   $
��Left=�6   $��
337,46,0,0q�6:    �� DisplayMemberPath$
 Name��6N   $	��Top=�6�   $	��261��6�   ��6g   �� *MembersWithoutEffect+ 6�   �� *SelectedCharacter+6�   �� 5   
   -   5        $��label3��5   m   $��Postacie z tym efektem:��6   $��28��62   $
��Left=�6>   $��
668,12,0,0q�6Y   $	��Top=�6{   �� 5   
   $	��338��6   $
��Left=�6   $��
617,46,0,0q�6:   $
 Name��6N   $	��Top=�6�   $	��261��6�   ��6g   �� *MembersWithEffect+ 6�   �� *SelectedCharacter+6�   �� 5   
   -   5        $��button3��5   z   $��4��62   $��26��6>   $
��Left=�6J   $��523,390,0,0q�6e   $	��Top=�6�   $��75��6�   $��Marlett)�6�   #��[ 6�   ��6   �� *AssignEffect+6�   �� 5   
   -   5        $��button4��5   z   $��3��62   $��26��6>   $
��Left=�6J   $��617,390,0,0q�6e   $	��Top=�6�   $��75��6�   $��Marlett)�6�   #��[ 6�   ��6   �� *ResignEffect+6�   �� 5   
   -   5        $��button5��5   u   $��OK��6,   $��26��69   $
��Left=�6E   $��822,410,0,0q�6`   $	��Top=�6�   $��75��6�   #��[ 6�   ��6   �� *Commit+6�   5      5      