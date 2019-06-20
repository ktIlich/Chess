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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChessLib;

namespace ChessWPF {
  /// <summary>
  /// Логика взаимодействия для Result.xaml
  /// </summary>
  public partial class Result : Window {
    Chess _chess;
    MainWindow main;
    Board _board;

    public Result( Chess chess , Board board ) {
      InitializeComponent( );
      _chess = chess;
      _board = board;
      Init( );
    }

    private void Init( ) {
      Label label = new Label( );
      if ( _chess.IsCheckmate( ) ) {
        label.Content = _chess.GetCurrentColor( ) == "Black" ? "Белыe победили" : "Победили черные";
        label.FontSize = 25;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        res_grid.Children.Add( label );
        Grid.SetRow( label , 1 );
        Grid.SetRowSpan( label , 2 );
      }
      if ( _chess.IsStalemate( ) ) {
        label.Content = "Ничья";
        label.FontSize = 25;
        label.HorizontalAlignment = HorizontalAlignment.Center;
        res_grid.Children.Add( label );
        Grid.SetRow( label , 1 );
        Grid.SetRowSpan( label , 2 );
      }
    }


    private void New_game_Click( object sender , RoutedEventArgs e ) {
      main = _board.GetMainWindow;
      main.ListViewMenu.SelectedIndex = 1;
      Close( );

    }

    private void Exit_Click( object sender , RoutedEventArgs e ) {
      Close( );
    }
  }
}
