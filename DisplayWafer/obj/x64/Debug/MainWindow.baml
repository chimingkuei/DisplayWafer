   M S B A M L   `   `   `  ����G  CDisplayWafer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TemplateTemplate  5   	       Template.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   "localclr-namespace:Template  5   	   -   5          � 
ResizeMode$  CanMinimize=�5
   	    � Title$ DisplayWafer��5   	   $	��800��6   $
��1000��6+   ���	#FF3E6FB3 � WindowStartupLocation$ CenterScreen=�6O    � DataContext 5         Template.MainViewModel  5   
   .��5      � .+�5   
   �� -   5         ��Name$	 Log��5      $
��Left=�6"   $	��125��6=   $��	1,655,0,0q�6J   $	��Top=�6]   $	��790��6u   ���	#FFE6F0FF � 	Resources 5       �� 
TargetType �� Property �� Value��%d  '��       'J� �     �� 5      " ��6   .�5      �� # �@��VerticalScrollBarVisibility$
 Auto=�6D   �� 5       � ��$ 0q�6/   �� 5       �@��
IsReadOnly . 63   �� 5       � ��$ Bottom=�67   5      �� 5      " J�6   .�5      ��  �@��Margin$ 0q�6/   �� 5       � ��$ 16(�61   5      5      .@�5      �� $��一般��6      BaseTabcontrol#��[  6"   .��5      �� -   5        	 ��Name$	 richTextBoxGeneral��5      ���	#FF43B5BF6:   5      �� 5       $��警告��6   #��[  6"   .��5!      �� -   5        $	 richTextBoxWarning��5!      ���	#FF43AABF6:   5"      �� 5#      $��錯誤��6   #��[  6"   .��5$      �� -   5        $	 richTextBoxError��5$      ���	#FF43A0BF68   5%      �� 5&      $��除錯��6   #��[  6"   .��5'      �� -   5        $	 richTextBoxDebug��5'      ���	#FF4396BF68   5(      5)      �� 5*   
   $
��Left=�6   $��5,3,0,0q�60   $	��Top=�6A   $	��646��6Y   $	��978��6f   ���	#FFE6F0FF.@�5+      �� $��	主畫面��6   #��[  6#   .��5,      � .+�5-      �� -   5        
 ��Name$

 Demo��5-        
BaseButton#��[ 6+   $��830,206,31,357q�6O   $
��Demo��6g   $��18(�6v   6�   �� 5.      -   5         ��Name$ 
Parameter1��5.        BaseTextBox#��[ 62   
 � Tag$ 请输入内容...��6W   $��841,312,34,262q�6f   �� 5/      -	   5        $ 
Parameter2��5/      #��[ 62   $ 请输入内容...��6W   $��841,374,34,202q�6f   � 50       � ColumnDefinitions 51      �� 52      �� 53      $	��600 �6/   �� 54       � RowDefinitions 56      �� 57      �� 58      $	��600 �6,   �� 59      .+�5;      �� -
   5         ��Name$ Wafer��5;   '   $��1��66   $��1��6F   ��6S   �� *Dies+ J� Clip 5<      @� 5=   "    @� Center$ 300 300&�62    @� RadiusX$	 300Q�6C    @� RadiusY$	 300Q�6Q   ��5?      �� 5@   "   .R�5A   &   A�  A� Columns$ 30��62    A� Rows$ 30��6?   5B   '   5C   #   ��5E      �� 5F   "   .R�5G   &   �� -   5        $��20��5G   -   $��20��69   $��0q�6D   
����·�6�   5H   #   5J      5K      5L      5M      �� 5N      $��參數畫面��6   #��[  6$   .��5O      � 5R      5S      5T      5U      5V      