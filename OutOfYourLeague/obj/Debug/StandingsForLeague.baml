   M S B A M L   `   `   `  ����J  FOutOfYourLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OutOfYourLeagueOutOfYourLeague  5   	   (    "OutOfYourLeague.StandingsForLeague   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:OutOfYourLeague     5   	     � Title$  StandingsForLeague��5   	   $	��450��6$   $	��450��61   $	��450��6A   $	��800��6Q   $	��800��6]   $	��800��6l    � Icon$ Resources/finalicon.png��6{    � 	Resources 5	         datagrid �� 
TargetType&   System.Windows.Controls.DataGrid �� Property  chbackground �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  roundbutton �� 
TargetType ��Name J� Opacity	 �� RadiusX
 �� RadiusY �� 
StartPoint �� EndPoint � Color � Offset J� Effect3  -System.Windows.Media.Effects.DropShadowEffect ��Name �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 �    &	     �� 5
   
   "  6!   .�5      ��  �@ ColumnHeaderStyle# �  62   �� 5       � �� �	#FF060A346+   �� 5       �@ CanUserAddRows .  6/   �� 5       �@ CanUserResizeColumns .  65   �� 5       �@ CanUserDeleteRows .  62   �� 5       �@ CanUserResizeColumns .  65   �� 5       �@ CanUserReorderColumns .  66   5      �� 5   
   "  6%   .�5      ��  � �� �	#FF060A346+   �� 5       � ��
 �����6+   5      �� 5   
   " ��6.   .R�5      � .+�5      �� $ xBackground��6   $ 1Q�61   $	 9��6=   $
 9��6I   ���	#FF060A34$
��0.35��6f   ��5      �� 5       $	 0,0&�6.   $	 0,1&�6?   .��5!      � $ White��6+   $ 0Q�69   � 5"      $ #666666��6+   $ 1Q�6;   5#       5%        5&      5(      �� 5)      $ xShine��6   $��2,2,2,0q�5*      $	��Top=�5+      $	 6��5,      $
 6��5-      $ 1Q�5.      
������ $
��15px��50      ��51      �� 52      $	 0,0&�6.   $	 0,1&�6?   .��53      � $ 	#ccffffff��6+   $ 0Q�6=   � 54      $ Transparent��6+   $ 1Q�6?   55      57      �� 58      $ contentPresenter��6#   $��Center=�6=   $��Center=�6Z   59       5:      P� 5;       �@J�IsMouseOver . . �5<      �� $ xBackground��6-    � �� 5=      
� 5>      .��5?   "   � $
 Navy��6/   $ 0Q�6<   � 5@   "   $ Black��6/   $ 0Q�6=   5A      5C      5D      P� 5E       � �� . . �5F      �� $ xBackground��6-    � �� 5G      
� 5H      .��5I   "   � $ Silver��6/   $ 0Q�6>   � 5J   "   $ #008080��6/   $ 0Q�6?   5K      5M      5N      5P      5Q      .��5R      � $��-1,0,1,0q�6   .+�5S   
     -   5        $��league��5S         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ 30 6!   $
��auto��62   $	��250��6@   #���   6L   $��Center=�6p   $��236,50,306,0q�6�   $	��Top=�6�   �� 5T   
   -   5         ��Name$
 main��5T   [   #��[ 6   
�������$��14(�6M   $
��Home��6i   $
��Left=�6x   $��
652,10,0,0q�6�   $	��Top=�6�   $	��130��6�   $��20��6�   6�   �� 5U   
   -   5        $ fixture��5U      #��[ 6"   
�������$��14(�6^   $��Fixtures��6l   $
��Left=�6   $��
652,48,0,0q�6�   $	��Top=�6�   $	��130��6�   $��20��6�   6�   �� 5V   
   -   5        $ topgoalscorer��5V      #��[ 6(   
�������$��14(�6d   $��Top Goal Scorers��6r   $
��Left=�6�   $��
652,85,0,0q�6�   $	��Top=�6�   $	��130��6�   $��20��6�   6�   �� 5W   
   -   5        $ logoff��5W      #��[ 6!   
�������$��14(�6\   $��Log off��6j   $
��Left=�6|   $��652,122,0,0q�6�   $	��Top=�6�   $	��130��6�   6�     5X   
   -   5          Name$ teams��5X      $
��auto��6"   #���   60   $��Center=�6T   $��136,50,546,0q�6q   $	��Top=�6�   $	��110��6�      MinColumnWidth$ 70Q�6�   ��5Y   
   �� 5Z      $	 0,0&�6"   $	 0,1&�63   .��5[      � $ White��6   $ 0Q�6-   � 5\      $ #666666��6   $ 1Q�6/   5]      5_      5`      