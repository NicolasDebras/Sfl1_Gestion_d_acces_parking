   M S B A M L   `   `   `  ����H  DAPP_C_PARKING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:APP_C_PARKINGAPP_C_PARKING  5   	       APP_C_PARKING.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:APP_C_PARKING     5   	     � Title$
  Menu��5   	   $	��450��6   $	��800��6#    � Icon$ logo_parking.png��6/    � 	Resources 5	       
  Brush1 �� Color��%-   &	        �� 5
   
   $ 	#FFDDDDDD��6)   5      .��5      � $��0,0,-8,1q�6    J� OpacityMask 5   
   �� 5       �� ImageSource$ rfid.png��6    � ColumnDefinitions 5   
   �� 5      $��9* �6   �� 5      $	��10* �6   �� 5      $	��66* �6   �� 5      $
��328* �6   �� 5      $
��112* �6   �� 5      $
��101* �6   �� 5      $	��52* �6   �� 5      $
��123* �6   .+�5   
   �� -   5         ��Name$ delete��5      $�� ��6!   $
��Left=�6,   $��75.4,221,0,0q�6G   $	��Top=�6]   $	��100��6u   $	��100��6�   
������� J� RenderTransformOrigin$ -0.626,1.174&�6�   $��5��6�   $��3��6�   ��5      �� 5      $ remove-user.png��6   5      �� 5   
   -   5        $	 add��5      $
��Left=�6   $	��Top=�6:   $	��100��6R   $��272.2,221,0,0q�6^   $��	Open SANS)�6u   $	��100��6�   ���	#FFF9F9F9	 � Cursor$	 Arrow��6�   $��3��6�   $��2��6�   ��5       �� 5!      $ add-user.png��6   5#      �� 5$   
   $��GESTION PARKING��6   $
��Left=�6*   $��43��6E   $��25.8,32,0,0q�6Q   $	��Top=�6f   $	��287��6~   $
��Bold"�6�   $��28(�6�   $��	Open SANS)�6�   $��2��6�   $��2��6�   �� 5%   
   -   5        $ Button_reversation��5%      $��RESERVATION��6-   $
��Left=�6C   $��32.4,41,0,0q�6^   $	��Top=�6s   $	��208��6�   $��32��6�   $��5��6�   $��3��6�   �� 5&   
   -   5        $
 rfid��5&      $�� ��6   $
��Left=�6*   $��25.8,221,0,0q�6E   $	��Top=�6[   $	��169��6s   $	��100��6   
�������$��2��6�   $��2��6�   ��5'      �� 5(      $ rfid.png��6    5*      �� 5+      $ rfid.png��6   
 j� Stretch$
 UniformToFill=�64   5-      �� 5.   
   -   5        $ liste��5.      $��3��6    $��LISTE UTILISATEUR��64   $��5��6P   $
��Left=�6`   $��32.4,92,0,0q�6{   $	��Top=�6�   $	��208��6�   $��36��6�   6�   50      51      