   M S B A M L   `   `   `  ����N  JStudentOrderManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:StudentOrderManagerStudentOrderManager  5   	   $    StudentOrderManager.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:StudentOrderManager     5   	     � Title$)  #Quản lý sinh viên & đơn hàng��5   	   $	��600��62   $	��900��6?   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $��2* �6   .+�5   
   �� $��Quản lý sinh viên��6   $��0��6.   $��0,0,0,10q�6;   .��5      �  � ColumnDefinitions 5      �� 5      $��3* �6'   �� 5      $��2* �6'   .+�5      �� $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��0,0,0,5q�6:   .+�5      �� $��
Tìm tên:��6$   $��Center=�64   �� 5      $	��120��6"   $��5,0,0,0q�6.   ��6?   �� *StudentNameFilter+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Lớp:��6$   $��10,0,0,0q�60   $��Center=�6B   �� 5      $��80��6"   $��5,0,0,0q�6-   ��6>   �� *StudentClassFilter+$ PropertyChanged=��� 5      $��Lọc��6!   $��10,0,0,0q�6/   ��6A   �� *FilterStudentsCommand+5      &   System.Windows.Controls.DataGrid  5         AutoGenerateColumns$ False��6v   $	��150��6�   $��0,0,0,5q�6�   ��6   �� *
Students+ �� SelectedItem 6@   �� *SelectedStudent+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��61   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 50 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Id+6T     5!      $
	 Tên��61   $
 * 6W    6>   �� *Name+6W     5"      $	 Lớp��61   $
 * 6X    6>   �� *Class+6X   5$      5%      �� 5&      $��1��6   $��10,0,0,0q�6-   .+�5'      �� $��Tên sinh viên:��6    �� 5(      ��6   �� *EditStudentName+$ PropertyChanged=��� 5)      $��Lớp:��6    $��0,5,0,0q�6,   �� 5*      ��6   �� *EditStudentClass+$ PropertyChanged=��� 5+      $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�5,      �� $��Thêm��6!   $��60��6V   ��60   �� *AddStudentCommand+6V   �� 5-      $��Sửa��6!   $��60��6V   $��5,0,0,0q�6a   ��6/   �� *EditStudentCommand+6a   �� 5.      $
��Xóa��6!   $��60��6X   $��5,0,0,0q�6c   ��6/   �� *DeleteStudentCommand+6c   5/      �� 50      
���  ����6    �� *StudentError+6>   51      52      53      �� 55   
   $��Quản lý đơn hàng��6   $��2��6-   .��56      �  57      �� 58      $��3* �6'   �� 59      $��2* �6'   .+�5;      �� $��0��6   .+�5<      �� $ 
Horizontal=�6!   $��0,0,0,5q�6:   .+�5=      �� $��Tìm khách:��6$   $��Center=�66   �� 5>      $	��120��6"   $��5,0,0,0q�6.   ��6?   �� *OrderCustomerFilter+$ PropertyChanged=��� 5?      $��Lọc��6!   $��10,0,0,0q�6/   ��6A   �� *FilterOrdersCommand+5@        5A      $ False��6r   $	��150��6�   $��0,0,0,5q�6�   ��6   �� *Orders+ 6>   �� *SelectedOrder+$ TwoWay=� 5B        5C      $	 ID��61   $
 50 6T    6=   �� *Id+6T     5D      $	 Khách��61   $
 * 6]    6@   �� *
Customer+6]     5E      $	 Tổng tiền��61   $
 * 6^    6D   �� *Total+6^   5G      5H      �� 5I      $��1��6   $��10,0,0,0q�6-   .+�5J      �� $��Khách hàng:��6    �� 5K       �� DisplayMemberPath$
 Name��6@    �� SelectedValuePath$
 Name��6Y   ��6   �� *
Students+ �� SelectedValue 6r   �� *EditOrderCustomer+$ PropertyChanged=��� 5L      $��Tổng tiền:��6    $��0,5,0,0q�62   �� 5M      ��6   �� *EditOrderTotal+$ PropertyChanged=��� 5N      $ 
Horizontal=�6!   $��0,10,0,0q�6:   .+�5O      �� $��Thêm��6!   $��60��6T   ��60   �� *AddOrderCommand+6T   �� 5P      $��Sửa��6!   $��60��6T   $��5,0,0,0q�6_   ��6/   �� *EditOrderCommand+6_   �� 5Q      $
��Xóa��6!   $��60��6V   $��5,0,0,0q�6a   ��6/   �� *DeleteOrderCommand+6a   5R      �� 5S      
���  ����6    �� *
OrderError+6<   5T      5U      5V      �� 5X   
   $��1��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,0,0,10q�6W   .+�5Y      �� $&�� Tổng đơn hàng theo khách: ��6   �� 5Z      $
��Bold"�6>   ��6   �� *TotalOrderByCustomer+6>   �� 5[      $��
Xuất PDF��6   $��20,0,0,0q�6M   ��6(   �� *ExportPdfCommand+6M   �� 5\      $��
Xuất CSV��6   $��5,0,0,0q�6M   ��6(   �� *ExportCsvCommand+6M   5]      5^      5_      