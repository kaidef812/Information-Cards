   M S B A M L   `   `   `  ����R  NInformationCards_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:InformationCards_Client.ViewInformationCards_Client.View  5   	   1    +InformationCards_Client.View.EditCardWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:InformationCards_Client.View     5   	     � Title$  EditCardWindow��5   	   $	��500��6    $	��800��6-    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Styles/Style.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��75 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   ��    	CardImage#��[  6   ��5      �� *ImageCardSource+�� 5    
     ButtonStyle#��[ 6   $��0��5!      $��Bottom=�5"      $��Upload new Image��5#      $��0,0,0,20q�5%      ��5$      �� *UploadImage+5%      �� 5'   
     InputBoxStyle#��[ 6   $��0��5)      $��1��5*      $��Center=�5+      $��0q�5,      ��5(      �� *
NameCard+5,      �� 5.   
   #��[ 6   $
��Save��5/      $��2��50      $��2��51      ��52      �� *
SaveEdit+54      55      