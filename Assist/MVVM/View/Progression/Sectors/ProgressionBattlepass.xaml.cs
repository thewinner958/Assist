﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Assist.Controls.Progression;
using Assist.MVVM.View.Progression.ViewModels;

namespace Assist.MVVM.View.Progression.Sectors
{
    /// <summary>
    /// Interaction logic for ProgressionBattlepass.xaml
    /// </summary>
    public partial class ProgressionBattlepass : Page
    {
        private BattlepassSectorViewModel _viewModel;
        public static ProgressionBattlepass instance;
        public ProgressionBattlepass()
        {
            instance = this;
            DataContext = _viewModel = new BattlepassSectorViewModel();
            InitializeComponent();
        }

        private async void Battlepass_Loaded(object sender, RoutedEventArgs e)
        {
           await _viewModel.LoadBattlepass(BattlepassContainer);


        }

        public static void ClearSelected()
        {
            if (instance != null)

                foreach (var obj in instance.BattlepassContainer.Children)
                {
                    var item = obj as BattlepassItem;
                    item.bIsSelected = false;
                }
            }
            
        }
    }
