   M S B A M L   `   `   `  ����I  EMahdeFooladWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:MahdeFooladWPF.ViewsMahdeFooladWPF.Views  5   	   +    %MahdeFooladWPF.Views.CustomMessageBox   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:MahdeFooladWPF.Views     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesgin1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowState$  Normal=�5	   	    � WindowStyle$
 None=�5
   	    � WindowStartupLocation$ CenterOwner=�5   	    J� Opacity$ 1Q�5   	    � Title$ CustomMessageBox��5   	   $	��200��6"   $	��300��6/    � Triggers 5      :� 5   
    :� RoutedEvent$ Window.Loaded��6   .[�5      �� .��5      �� .��5      W�  g� Duration$ 0:0:0.5D�5      	 W� To 15      	 W� From	 05      
 �� TargetProperty$
 Opacity�5      5      5      5      .��5      �� ���#e8f7f4.+�5   
   �� -   5         ��Name$ 
txtMessage��5       �� Padding$ 10q�5      $��Center=�5      
�������$
��Wrap=�5!      $��5 10 0 0q�5"      �� 5#   
   $��Center=�6   .��5$      � .+�5%      A� -   5         A�Name$ xbackground��5&      $��80��5%      $��Center=�5'      ���#34e065$��0 0 0 0q�5)      $��Center=�5*      $��80��5+      '  !MaterialDesignThemes.Wpf.PackIcon  5,      -   5          Name$ xIocn��5-   *      Kind$
 Done=�5,   *   $��Center=�5.   *   $��Center=�5/   *   $��80��50   *   
�������$��80��52   *   53      54      �� 56   
   -   5         ��Name$ stackpanelBtns��58       �� Orientation$ 
Horizontal=�56      $��0 15 0 0q�57      5:      5<      5=      