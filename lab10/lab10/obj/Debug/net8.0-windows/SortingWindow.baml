   M S B A M L   `   `   `  ����@  <lab10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab10lab10  5   	       lab10.SortingWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:lab10     5   	     � Title$  SortingWindow��5   	   $	��450��6   $	��800��6,   .��5	      � .+�5
   
   �� $
��Left=�6   $��
320,74,0,0q�6/   $
��Wrap=�6C   $��Choose type of order:��6W   $	��Top=�6t   $	��130��6�   $��25��6�   	� 5   
   -   5         	�Name$ ascendingOrderBut��5      $��	Ascending��61    	� 	GroupName$ order��6E   $
��Left=�6W   $��335,123,0,0q�6r   $	��Top=�6�   $	��116��6�   $��21��6�   	� 5   
   -   5        $ descendingOrderBut��5      $��
Descending��62   $ order��6G   $
��Left=�6Y   $��335,149,0,0q�6t   $	��Top=�6�   $	��108��6�   $��20��6�   �� 5   
   $��Center=�6   $��	0,192,0,0q�61   $
��Wrap=�6D   $4��.Choose what do you want items to be sorted by:��6X   $	��Top=�6�   $	��268��6�   $��25��6�   	� 5   
   -   5        $ yearBut��5      $
��Year��6'   $ sorting��66   $
��Left=�6J   $��350,242,0,0q�6e   $	��Top=�6z   $��21��6�   $��78��6�   	� 5   
   -   5        $ modelBut��5      $��Model��6(   $ sorting��68   $��Center=�6L   $��	0,269,0,0q�6i   $	��Top=�6|   $��21��6�   $	��102��6�   	� 5   
   -   5        $ motorBut��5      $��Motor��6(   $ sorting��68   $��Center=�6L   $��	0,297,0,0q�6i   $	��Top=�6|   $��20��6�   $	��100��6�   �� 5   
   -   5        $��Confirm��5      $
��Left=�6#   $��354,359,0,0q�6>   $	��Top=�6S   6k   5      5      