   M S B A M L   `   `   `  ����  MahdeFooladWPF&     MahdeFooladWPF.Views.LoginWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aGmaterialDesign11http://materialdesigninxaml.net/winfx/xaml/themes  5   	   
������   � WindowStyle$
  None=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � AllowsTransparency$
 True��5   	    � Title$ LoginWindow��5   	   $	��400��6   $	��600��6*    � Triggers 5      :� 5   
    :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      t�  g� Duration$ 0:0:1D�5       g� 	BeginTime	 0:0:35      	 t� To 05      	 �� TargetProperty$	 Margin�5      
 �� 
TargetName$
 	logingrid��5       t� EasingFunction 5      -  'System.Windows.Media.Animation.BackEase  5         	Amplitude$	 0.3Q�6'   7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode$ EaseOut=�5   (   5      5      5      5      .��5#      ��  �� CornerRadius$ 10��6   ��5$   
   �� 5%      .��5&      �  � Color$ #0077b6��6    � Offset$ 0Q�6/   � 5'      $ #48cae4��6   $	 1.1Q�6/   5(      ._�5*   
   � -   5        $��	MainPanel��5+      
������ .+�5-      � -   5        $��	logingrid��5/      $��
-860 0 0 0q�5.      .+�50      �� $��Center=�6   $	��Top=�66   $	��100��51      $	��100��52      $ 50��53      $��0 20 0 0q�54       K� ZIndex$ 1��55      ���#0077b6 J� Effect 57      3  -System.Windows.Media.Effects.DropShadowEffect  58         Color$ #0077b6��59         ShadowDepth$ 0Q�5:         
BlurRadius$ 8Q�5;         	Direction$	 180Q�5<      ._�5?      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 User=�6/   $��Center=�5@   /   $��Center=�5A   /   $	��100��5B   /   
�������$	��100��5D   /   5G      �� 5H      $��Center=�5I      
�������$��Center=�5K      $ 30��5L       �� Padding$ 15q�5M      $	��250��5N      $	��250��5O      ._�5P      �  � RowDefinitions 5Q      �� 5R      �� 5S      �� 5T      .+�5W      �� $��16(�5Y      $��	0 20 0  0q�5Z      $��Center=�5[      $��Center=�5]      ��5\      ��  �� Path$ UserName�5]      G� 5_      -   5        $��txtpassword��5`   !   $��RightToLeft=�5a   !   $��1��5b   !   $��1��5c   !   $��6q�5d   !   $	��Top=�5e   !   �� 5h      $��2��6%   .+�5i      �� -   5        $��btnEnter��5j      $��Center=�5n      $��ورود��5o      
������ $��0 0 0 10q�5q      ���#0077b6$��2��5s      ��5l      �� *LoginCommand+ �� IsAsync$
 True����5m      ��  �� ElementName$ txtpassword�� 5t   "   :� 5u   &   $ Button.Click��63   .[�5v   *   �� .��5w   .   �� .��5x   2   t� $
 	logingrid��5y   5   $	 Margin�5z   5    	860,0,0,05{   5   $ 0:0:2D�5|   5    5}   6   .  (System.Windows.Media.Animation.CubicEase  5~   :   $ EaseIn=�6E   5�   3   5�   /   5�   +   5�   '   5�      �� 5�      -   5        $��btnClose��5�      $��لغو��5�      $��Center=�5�      $��2��5�      
���  ��
������  5�   "   :� 5�   &   $ Button.Click��63   .[�5�   *   �� .��5�   .   �� .��5�   2   t� $ 0:0:1D�6E    
-860,0,0,05�   A   $
 	logingrid��5�   A   $	 Margin�5�   A    5�   6     5�   :   $	 0.2Q�6C   $ EaseIn=�5�   A   5�   3   5�   /   5�   +   5�   '   5�      5�      5�      5�      5�      5�      5�      5�      