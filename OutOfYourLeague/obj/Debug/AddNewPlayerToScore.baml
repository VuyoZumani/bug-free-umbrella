   M S B A M L   `   `   `  ����J  FOutOfYourLeague, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OutOfYourLeagueOutOfYourLeague  5   	   )    #OutOfYourLeague.AddNewPlayerToScore   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:OutOfYourLeague     5   	     � Title$  AddNewPlayerToScore��5   	   $	��450��6%   $	��450��62   $	��450��6B   $	��800��6R   $	��800��6^   $	��800��6m    � Icon$ Resources/finalicon.png��6|    � 	Resources 5	         
largelabel �� 
TargetType  roundbutton ��Name J� Opacity �� RadiusX �� RadiusY �� 
StartPoint	 �� EndPoint
 � Color � Offset J� Effect3  -System.Windows.Media.Effects.DropShadowEffect ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value��%j  &	        &	 w     �� 5
   
   " ��6-   .R�5      � .+�5      �� $��18(�6   ���	#FF060A34#��z��6=   5      5      �� 5   
   " ��6.   .R�5      � .+�5      �� $ xBackground��6   $ 1Q�61   $ 9��6=   $ 9��6I   ���	#FF060A34$
��0.35��6f   ��5      �� 5      $	 0,0&�6.   $		 0,1&�6?   .��5      � $
 White��6+   $ 0Q�69   � 5      $
 #666666��6+   $ 1Q�6;   5       5        5      5      �� 5      $ xShine��6   $��2,2,2,0q�5      $	��Top=�5      $ 6��5      $ 6��5       $ 1Q�5!      
������ $
��15px��5#      ��5$      �� 5%      $	 0,0&�6.   $		 0,1&�6?   .��5&      � $
 	#ccffffff��6+   $ 0Q�6=   � 5'      $
 Transparent��6+   $ 1Q�6?   5(      5*      �� 5+      $ contentPresenter��6#   $��Center=�6=   $��Center=�6Z   5,       5-      P� 5.       �@J�IsMouseOver . . �5/      �� $ xBackground��6-    � �� 50      
� 51      .��52   "   � $

 Navy��6/   $ 0Q�6<   � 53   "   $
 Black��6/   $ 0Q�6=   54      56      57      P� 58       � �� . . �59      �� $ xBackground��6-    � �� 5:      
� 5;      .��5<   "   � $
 Silver��6/   $ 0Q�6>   � 5=   "   $
 #008080��6/   $ 0Q�6?   5>      5@      5A      5C      5D      .��5E      �  � ColumnDefinitions 5F   
   �� 5G      $	��31* �6   �� 5H      $	��55* �6   �� 5I      $	��25* �6    � RowDefinitions 5K   
   �� 5L      $	��10* �6   �� 5M      $	��10* �6   �� 5N      $	��10* �6   �� 5O      $	��10* �6   �� 5P      $	��20* �6   .+�5R   
   (� $��1��6   $
��Left=�6"   $��83��6=   $��Bottom=�6I   $	��360��6d   $��2��6p    J� RenderTransformOrigin$ 0.502,0.642&�6}   $��2��6�   .��5S      �� -   5         ��Name$ 	addplayer��5S   _   #��[ 6   
�������$��14(�6P   $��
Add Player��6r   $
��Left=�6�   $	��Top=�6�   $	��333��6�   $��47��6�   6�   5T      �� 5U   
   -   5         ��Name$ goals��5U   ,   $
��Left=�6;   $��40��6V    �� TextWrapping$
 Wrap=�6b   $	��Top=�6v   $	��358��6�   $��1��6�   $��0,17,0,0q�6�   $��2��6�   �� 5V   
   -   5        $ player��5V   -   $
��Left=�6=   $��42��6X   $
 Wrap=�6d   $	��Top=�6x   $	��360��6�   $��1��6�   $��0,28,0,0q�6�   �� 5W   
   -   5         ��Name$ teamofplayer��5W   K   ���	#FF060A34$
��Left=�6a   $	��Top=�6|   $	��358��6�   $��42��6�   $��1��6�   $��0,28,0,0q�6�   $��1��6�   (� 5X   
   $��2��6   $
��Left=�6"   $��50��6=   $��	36,22,0,0q�6I   $	��Top=�6\   $	��132��6t   $��2��6�   .��5Y      �� -   5        $
 main��5Y   _   #��[ 6   
�������$��14(�6P   $
��Home��6m   $
��Left=�6|   $	��Top=�6�   $	��166��6�   $��26��6�   6�   5Z      (� 5[   
   $
��Left=�6   $��68��6-   $��	33,28,0,0q�69   $	��Top=�6L   $	��190��6d   $��2��6p   $��2��6�   .��5\      �� #��[  6   $��Name of player:��6<   $
��Left=�6V   $	��Top=�6q   $	��190��6�   5]      (� 5^   
   $
��Left=�6   $��38��6-   $��	36,20,0,0q�69   $	��Top=�6L   $	��185��6d   $��1��6p   (� 5_   
   $��2��6   $
��Left=�6"   $��50��6=   $��	36,61,0,0q�6I   $	��Top=�6\   $	��132��6t   $��2��6�   .��5`      �� -   5        $ topgoalscorers��5`   _   #��[ 6   
�������$��14(�6P   $��Top Goal Scorers��6w   $
��Left=�6�   $	��Top=�6�   $	��166��6�   $��26��6�   6�   5a      (� 5b   
   $
��Left=�6   $��68��6-   $��	31,28,0,0q�69   $	��Top=�6L   $	��190��6d   $��1��6p   $��2��6}   .��5c      �� #��[  6   $��Team:��6<   $
��Left=�6L   $	��Top=�6g   $	��190��6   5d      (� 5e   
   $
��Left=�6   $��68��6-   $��	31,17,0,0q�69   $	��Top=�6L   $	��190��6d   $��2��6p   $��2��6�   .��5f      �� #��[  6   $��Goals:��6<   $
��Left=�6M   $	��Top=�6h   $	��190��6�   5g      (� 5h   
   $��2��6   $
��Left=�6"   $��50��6=   $��	36,28,0,0q�6I   $	��Top=�6\   $	��132��6t   $��2��6�   $��1��6�   .��5i      �� -   5        $ logoff��5i   _   #��[ 6   
�������$��14(�6P   $��Log off��6o   $
��Left=�6�   $	��Top=�6�   $	��166��6�   $��26��6�   6�   5j      ��5k   
   �� 5l      $	 0,0&�6"   $		 0,1&�63   .��5m      � $
 White��6   $ 0Q�6-   � 5n      $
 #666666��6   $ 1Q�6/   5o      5q      5s      