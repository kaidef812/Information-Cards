   M S B A M L   `   `   `  ����R  NInformationCards_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:InformationCards_Client.ViewInformationCards_Client.View  5   	   -    'InformationCards_Client.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:InformationCards_Client.View     5   	     � Title$  
MainWindow��5   	   $	��650��6   $	��800��6)    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Styles/Style.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $	��100 �6   .+�5   
   �    	CardsGrid#��[  6   $��0��5      .+�5      �� $��1q�6   
���   �._�5      �� $��Hidden=�6   .��5      �� -   5         ��Name$ 
CardsPanel��5   !   5      5      5       � 5"   
   $��1��6    � ColumnDefinitions 5#      �� 5$      �� 5%      �� 5&      �� 5'      .+�5*      ��   ButtonStyle#��[ 6   $��0��5+      $��	Get cards��5,      ��5-      �� *
GetCards+�� 5/      #��[ 6   $��1��50      $��	Post card��51      ��52      �� *
PostCard+�� 54      #��[ 6   $��2��55      $��Put card��56      ��57      �� *	PutCard+�� 59      #��[ 6   $��3��5:      $��Delete card��5;      ��5<      �� *
DeleteCard+5=      5>      5?      