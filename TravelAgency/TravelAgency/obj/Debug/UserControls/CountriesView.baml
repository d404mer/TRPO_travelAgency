   M S B A M L   `   `   `  ����G  CTravelAgency, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TravelAgency.UserControlsTravelAgency.UserControls  5      +    %TravelAgency.UserControls.CountryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:TravelAgency.UserControls     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $
��197* �6   �� 5      $	��28* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ CountriesDataGrid��5      $��10,10,10,10q�6.      AutoGenerateColumns .     
IsReadOnly .    SelectionMode$ Single=�5         RowHeaderWidth$ 0Q�5      $��0��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID Страны��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6W   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 68   �� *
Country_ID+6W     5      $# Название страны��6%   $
 Auto 6_   	 6>   �� *Country_Name+6_   5      �� 5   
   
 �� Orientation$
 
Horizontal=�6   $��	10,7,10,0q�6.   $	��Top=�6A   $��1��6Y   .+�5      �� -   5        $#��Добавить страну��5      $��5q�6N   �� 5       -   5        $!��Удалить страну��5       $��5q�6P   5!      5"      5#      