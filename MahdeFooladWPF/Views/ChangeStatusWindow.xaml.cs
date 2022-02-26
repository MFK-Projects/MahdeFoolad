﻿using MahdeFooladWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MahdeFooladWPF.Views
{
    /// <summary>
    /// Interaction logic for ChangeStatusWindow.xaml
    /// </summary>
    public partial class ChangeStatusWindow : Window
    {
        private readonly ChangeStatusViewModel _vmModel;
        public ChangeStatusWindow(ChangeStatusViewModel vmModel)
        {
            InitializeComponent();
            _vmModel = vmModel;
            this.DataContext = _vmModel;
        }

    }
}
