   M S B A M L   `   `   `  ����J  FOutOfYourLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OutOfYourLeagueOutOfYourLeague  5   	   $    OutOfYourLeague.TopGoalScorers   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:OutOfYourLeague     5   	     � Title$  TopGoalScorers��5   	   $	��450��6    $	��450��6-   $	��450��6=   $	��800��6M   $	��800��6Y   $	��800��6h    � Icon$ Resources/finalicon.png��6w    � 	Resources 5	         datagrid �� 
TargetType&   System.Windows.Controls.DataGrid �� Property  chbackground �� Value5  /System.Windows.Controls.DataGridLengthConverter=  7System.Windows.Controls.Primitives.DataGridColumnHeader  roundbutton �� 
TargetType ��Name J� Opacity	 �� RadiusX
 �� RadiusY �� 
StartPoint �� EndPoint � Color � Offset J� Effect3  -System.Windows.Media.Effects.DropShadowEffect ��Name �� Triggers P� Property P� Value �� 
TargetName��%p  &	        &	 �    &	 s    �� 5
   
   "  6!   .�5      ��  �@ ColumnHeaderStyle# �  62   �� 5        �@ AlternatingRowBackground
 �����69   �� 5       � �� �	#FF060A346+   �� 5       �@ ColumnWidth$ * 6,   �� 5       �@ CanUserAddRows .  6/   �� 5       �@ CanUserResizeColumns .  65   �� 5       �@ CanUserDeleteRows .  62   �� 5       �@ CanUserResizeColumns .  65   �� 5       �@ CanUserReorderColumns .  66   5      �� 5   
   "  6%   .�5      ��  � �� �	#FF060A346+   �� 5       � ��
 �����6+   5      �� 5   
   " ��6.   .R�5      � .+�5      �� $ xBackground��6   $ 1Q�61   $	 9��6=   $
 9��6I   ���	#FF060A34$
��0.35��6f   ��5      �� 5      $	 0,0&�6.   $	 0,1&�6?   .��5      � $ White��6+   $ 0Q�69   � 5       $ #666666��6+   $ 1Q�6;   5!       5#        5$      5&      �� 5'      $ xShine��6   $��2,2,2,0q�5(      $	��Top=�5)      $	 6��5*      $
 6��5+      $ 1Q�5,      
������ $
��15px��5.      ��5/      �� 50      $	 0,0&�6.   $	 0,1&�6?   .��51      � $ 	#ccffffff��6+   $ 0Q�6=   � 52      $ Transparent��6+   $ 1Q�6?   53      55      �� 56      $ contentPresenter��6#   $��Center=�6=   $��Center=�6Z   57       58      P� 59       �@J�IsMouseOver . . �5:      �� $ xBackground��6-    � �� 5;      
� 5<      .��5=   "   � $
 Navy��6/   $ 0Q�6<   � 5>   "   $ Black��6/   $ 0Q�6=   5?      5A      5B      P� 5C       � �� . . �5D      �� $ xBackground��6-    � �� 5E      
� 5F      .��5G   "   � $ Silver��6/   $ 0Q�6>   � 5H   "   $ #008080��6/   $ 0Q�6?   5I      5K      5L      5N      5O      .��5P      � $��-2,0,2,0q�6    � ColumnDefinitions 5Q   
   �� 5R      $	��35* �6   �� 5S      $	��15* �6   �� 5T      $	��15* �6   .+�5V   
   (� $
��Left=�6   $	��271��6-   $��44��69   $��
88,345,0,0q�6E   $	��Top=�6Y   .��5W      �� -   5         ��Name$ 	addplayer��5W   _   #��[ 6   
�������$��14(�6Q   $��
Add player��6r   $
��Left=�6�   $	��Top=�6�   $	��251��6�   $��44��6�   5Y      5Z      (� 5[   
   $
��Left=�6   $	��216��6-   $��25��69   $��1,75,0,0q�6E   $	��Top=�6W   $��2��6o   .��5\      �� -   5        $ 	standings��5\   `   #��[ 6   
�������$��14(�6R   $��	Standings��6s   $
��Left=�6�   $	��Top=�6�   $	��150��6�   $��26��6�   5^      5_      (� 5`   
   $
��Left=�6   $	��216��6-   $��27��69   $��1,25,0,0q�6E   $	��Top=�6W   $��2��6o   .��5a      �� -   5        $
 main��5a   `   #��[ 6   
�������$��14(�6R   $
��Home��6n   $
��Left=�6}   $	��Top=�6�   $	��139��6�   $��26��6�   5c      5d      (� 5e   
   $
��Left=�6   $	��216��6-   $��25��69   $��	1,125,0,0q�6E   $	��Top=�6X   $��2��6p   .��5f      �� -   5        $ fixtures��5f   `   #��[ 6   
�������$��14(�6R   $��Fixtures��6r   $
��Left=�6�   $	��Top=�6�   $	��150��6�   $��26��6�   5h      5i      (� 5j   
   $
��auto��6   $
��auto��6    $
��Left=�6-   $��	34,72,0,0q�6H   $	��Top=�6[   .��5k        -   5          Name$ topgoalscorers��5k   H   $
��auto��6   #���   6%   $��Right=�6`   $	��Top=�6|   $	��362��6�   5l      �� 5m   
   -   5        $ updatetopgoalscorer��5m   [   #��[ 6   
�������$��14(�6M   $��Update top goal scorers��6x   $
��Left=�6�   $	��Top=�6�   $	��251��6�   $��44��6�   $��
86,266,0,0q�6�   6�   (� 5n   
   $
��Left=�6   $	��216��6-   $��27��69   $��3,169,-36,0q�6E   $	��Top=�6Z   $��2��6r   .��5o      �� -   5        $ logoff��5o   `   #��[ 6   
�������$��14(�6R   $��Log off��6p   $
��Left=�6�   $	��Top=�6�   $	��139��6�   $��26��6�   6�   5p      ��5q   
   �� 5r      $	 0,0&�6"   $	 0,1&�63   .��5s      � $ White��6   $ 0Q�6-   � 5t      $ #666666��6   $ 1Q�6/   5u      5w      5x      