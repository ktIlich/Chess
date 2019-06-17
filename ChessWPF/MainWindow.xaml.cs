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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLib;

namespace ChessWPF {
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    Chess game;
    Board board;
    MoveViewer viewer;

    public MainWindow( ) {
     
      game = new Chess( "rnbq1rk1/ppp2pbp/8/3pp3/8/3P4/PPPBQPPP/RN2KBNR w KQ - 0 8" );
      
      board = new Board( );
      board.SetChess = game;
      board.SetFigureChess( );

      viewer = new MoveViewer( );
      viewer.Board = board;
      viewer.Chess = game;
      InitializeComponent( );
    }


    private void main_window_loaded( object sender , RoutedEventArgs e ) {
      double pos = ( GridMenu.ActualHeight - ListViewMenu.ActualHeight ) / 2;

      MoveCursorMenu( 0 , pos );
    }

    private void MoveCursorMenu( int index , double pos ) {
      TrainsitionigContentSlide.OnApplyTemplate( );
      GridCursor.Margin = new Thickness( 0 , ( pos + ( index * 60 ) ) , 0 , 0 );
    }

    public DockPanel dock;
    private void ListViewMenu_SelectionChanged( object sender , SelectionChangedEventArgs e ) {
      int index = ListViewMenu.SelectedIndex;
      double pos = ( GridMenu.ActualHeight - ListViewMenu.ActualHeight ) / 2;

      MoveCursorMenu( index , pos );




      board.Width = 500;
      board.Height = 500;
      viewer.Width = 300;

      switch ( index ) {
        case 0:
          content.Children.Clear( );
          content.Children.Add( viewer );
          content.Children.Add( board );

          break;
        case 1:
          content.Children.Clear( );
          break;
        //case 2:
        //  GridPrincipal.Children.Clear( );
        //  GridPrincipal.Children.Add( online );
        //  break;
        default:
          break;
      }
    }

    private void Window_SizeChanged( object sender , SizeChangedEventArgs e ) {
      int index = ListViewMenu.SelectedIndex;
      double pos = ( GridMenu.ActualHeight - ListViewMenu.ActualHeight ) / 2;

      MoveCursorMenu( index , pos );
    }


  }
}
