   M S B A M L   `   `   `  ����@  <lab10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab10lab10  5   	       lab10.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:lab10     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � $	��440��6   $	��Top=�6   $��0,0,0,0q�60   $	��800��6A   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridView1��5         CanUserSortColumns$
 True��6*   $��	0,0,504,0q�6D      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Year��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 35 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Year+6L     5      $ Model��6%   $ 50 6N    64   �� *Model+6N     5      $ Model��6%   $ 50 6T    64   �� *Motor.Model+6T     5      $ Displacement��6%   $ 80 6b    6;   �� *Motor.Displacement+6b     5      $ 
HorsePower��6%   $ 80 6^    69   �� *Motor.HorsePower+6^   5      �� 5   
   -   5        $��
Exercise 1��5       J� RenderTransformOrigin$ 1.504,-7.827&�6<   $��303,10,437,402q�6a   �� 5   
   -   5        $��
Exercise 2��5      $ 1.504,-7.827&�68   $��368,10,372,402q�6]   �� 5   
   -   5        $��
Exercise 3��5      $ 1.504,-7.827&�68   $��433,10,307,402q�6]   �� 5   
   -   5        $��Exercise 3b��5      $ 1.504,-7.827&�6;   $��498,10,238,402q�6`   �� 5   
   -   5        $��Clear 3b��5      $ 1.504,-7.827&�6<   $��567,10,181,402q�6a   5      �� 5   
   -   5         ��Name$ exercisesBox��5      $
��Left=�6(   $��
336,59,0,0q�6C   	 �� TextWrapping$
	 Wrap=�6W   $	��Top=�6k   $	��412��6�   $	��332��6�   �� 5   
   -   5        $��Delete��5      $ 1.504,-7.827&�67   $��624,10,124,402q�6\   5      �� 5   
   -	   5        $	��Add��5      $ 1.504,-7.827&�61   $��681,10,67,402q�6V   �� 5   
   -
   5        $
��Edit��5      $ 1.504,-7.827&�63   $��738,10,10,402q�6X   5      5       