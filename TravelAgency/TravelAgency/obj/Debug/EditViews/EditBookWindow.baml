   M S B A M L   `   `   `  ����G  CTravelAgency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TravelAgency.EditViewsTravelAgency.EditViews  5   	   +    %TravelAgency.EditViews.EditBookWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:TravelAgency.EditViews     5   	     � Title$;  5Редактирование Бронирования��5   	   $	��450��6-   $	��800��6:      PrimaryBrush#��[  5	   	   .��5      � .+�5   
   �� $��20q�6   .+�5      �� 
�������$
��Bold"�6+   $"��ID Бронирования:��6=   �� 5      -   5         ��Name$ BookIDTextBox��5       �� 
IsReadOnly . 6-   �� 5      
�������$
��Bold"�6+   $��ID Агента:��6=   �� 5      -   5        $ AgentIDTextBox��5      �� 5      
�������$
��Bold"�6+   $��ID Тура:��6=   �� 5      -   5        $ TourIDTextBox��5      �� 5      
�������$
��Bold"�6+   $(��"Дата бронирования:��6=   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ BookingDatePicker��5      �� 5      
�������$
��Bold"�6+   $��ID Отеля:��6=   �� 5      -   5        $ HotelIDTextBox��5      �� 5      
�������$
��Bold"�6+   $��	Цена:��6=   �� 5      -   5        $ PriceTextBox��5      �� 5      -   5        $��0,10,0,0q�5      $��Сохранить��6'   6;   5       5!      5"      