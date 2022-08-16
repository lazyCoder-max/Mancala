using MancalaAssessment.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        BoardViewModel boardViewModel;
        public Board()
        {
            InitializeComponent();
            Loaded += Board_Loaded;
        }

        private void Board_Loaded(object sender, RoutedEventArgs e)
        {
            boardViewModel = DataContext as BoardViewModel;
        }

        private async void MancalaPitButton_Click(object sender, RoutedEventArgs e)
        {
            var pit = sender as Pit;
            pit.IsEnabled = false;
            boardViewModel.Player[boardViewModel.PlayerTurn].SelectedPit = pit.PitModel;
            await boardViewModel.ThrowStone();
            pit.IsEnabled = true;
        }

        private void PitsPlayer2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (boardViewModel.PlayerTurn == 1)
            {
                PitsPlayer2.IsEnabled = true;
                PitsPlayer1.IsEnabled = false;
            }
            else
            {
                PitsPlayer2.IsEnabled = false;
                PitsPlayer1.IsEnabled = true;
            }
            
        }

        private void PitsPlayer1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (boardViewModel.PlayerTurn == 0)
            {
                PitsPlayer1.IsEnabled = true;
                PitsPlayer2.IsEnabled = false;
            }
            else
            {
                PitsPlayer1.IsEnabled = false;
                PitsPlayer2.IsEnabled = true;
            }
        }
    }
}
