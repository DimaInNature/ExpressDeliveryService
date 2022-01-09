﻿using ExpressDeliveryService.ViewModel;
using System.Windows.Controls;

namespace ExpressDeliveryService.View
{
    public partial class SettingsView : UserControl
    {
        internal SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
