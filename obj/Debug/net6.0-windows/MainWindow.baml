   M S B A M L   `   `   `  ����@  <paint, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:paintpaint  5   	       paint.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:paint     5   	     � Title$  
MainWindow��5   	   $	��800��6   $
��1200��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $	��Top=�6.   $��Center=�6F   .+�5      �� -   5        $��
ColorLabel��5      $��Color:��6&   $��10(�67   $��Center=�6E   $
��Auto��6`   �� 5      -   5        $��colorComboBox��5      $��60��6^   $��0,0,10,0q�6i   .@�5      �� .��6   Black6&   �� 5      .��6   Red6$   �� 5      .��6   Blue6%   �� 5      .��6   Eraser6'   5      �� 5      -   5        $��	penButton��5      $	��Pen��6&   $��0,0,10,0q�6L   $��50��6^   �� 5      -   5        $��
fillButton��5      $
��Fill��6'   $��0,0,10,0q�6O   $��50��6a   �� 5      -   5        $��
lineButton��5      $
��Line��6'   $��0,0,10,0q�6O   $��50��6a   �� 5      -   5        $��ellipseButton��5      $��Ellipse��6*   $��0,0,10,0q�6X   $��50��6j   �� 5      -   5        $��rectangleButton��5      $��	Rectangle��6,   $��0,0,10,0q�6^   $��60��6p   �� 5      -   5        $��polygonButton��5      $��Polygon��6*   $��0,0,10,0q�6X   $��50��6j   �� 5      -	   5        $��
pathButton��5      $
��Path��6'   $��0,0,10,0q�6O   $��50��6a   �� 5!      -
   5        $��clearButton��5!      $��Clear��6(   $��0,0,10,0q�6R   $��50��6d   �� 5#      $	��Top=�6   $��Center=�61   $��0,0,10,0q�6N   .+�5$      �� -   5        $��
RGBTextBox��5$      .��6,   	255,0,065   �� 5%      -   5        $��ConvertToHSV��5%      $��
RGB to HSV��6-   6B   �� 5&      -   5        $��
HSVTextBox��5&       �� 
IsReadOnly$
 True��6,   6@   5'      �� 5)      -   5        $��EmguCV��5)      $��EmguCV��6#   $��0,0,10,0q�6M   $��50��6_   �� 5+      -   5        $��ShapesLabel��5+      $��Predefined:��6'   $��10(�6=   $��Center=�6K   $
��Auto��6f   �� 5,      -   5        $��triangleButton��5,      $��Triangle��6+   $��0,0,10,0q�6[   $��50��6m   �� 5-      -   5        $��arrowButton��5-      $��Arrow��6(   $��0,0,10,0q�6R   $��50��6d   �� 5/      -   5        $��
saveButton��5/      $
��Save��6'   $��0,0,10,0q�6O   $��50��6a   50      �� 51   
   -   5        $��paintSurface��51      $��1��6%   ��52      �� 53      -   5        canvasBackground�� �� Color$ White��53   <    �� Opacity$ 1Q�6J   55      56      57      