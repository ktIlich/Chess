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
using System.Windows.Threading;
using ChessLib;

namespace ChessWPF {
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {

    static Chess game;
    static Board board;
    static MoveViewer viewer;
    static ToolBar toolBar;
    static DispatcherTimer dispatcherTimer;
    static Setting setting;


    public MainWindow( ) {

      game = new Chess( "rnbq1rk1/ppp2pbp/8/3pp3/8/3P4/PPPBQPPP/RN2KBNR w - - 0 8" );

      board = new Board( );
      board.SetChess = game;

      toolBar = new ToolBar( board );
      board.SetToolBar = toolBar;

      board.SetFigureChess( );

      viewer = new MoveViewer( );
      viewer.Board = board;
      viewer.Chess = game;

      InitializeComponent( );

      dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
        DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
        Dispatcher );
      dispatcherTimer.Start( );

      setting = new Setting( this );
    }

    public void NewGame( int index ) {
      switch ( index ) {
        case 0: {
          game = new Chess( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1" );
          board = new Board( );
          board.SetChess = game;
          toolBar = new ToolBar( board );
          board.SetToolBar = toolBar;
          board.WithBot = false;
          board.SetFigureChess( );
          viewer = new MoveViewer( );
          viewer.Board = board;
          viewer.Chess = game;
          viewer.Reset( );
          game.MoveStack.Clear( );
          game.FenStack.Clear( );
          dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
          DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
          Dispatcher );
          dispatcherTimer.Start( );
          setting = new Setting( this );
          ListViewMenu.SelectedIndex = 0;
          break;
        }
        case 1: {
          game = new Chess( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1" );
          board = new Board( );
          board.SetChess = game;
          toolBar = new ToolBar( board );
          board.SetToolBar = toolBar;
          board.WithBot = true;
          board.Diff = 1;
          board.SetFigureChess( );
          viewer = new MoveViewer( );
          viewer.Board = board;
          viewer.Chess = game;
          viewer.Reset( );
          game.MoveStack.Clear( );
          game.FenStack.Clear( );
          dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
          DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
          Dispatcher );
          dispatcherTimer.Start( );
          setting = new Setting( this );
          ListViewMenu.SelectedIndex = 0;
          break;
        }
        case 2: {
          game = new Chess( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1" );
          board = new Board( );
          board.SetChess = game;
          toolBar = new ToolBar( board );
          board.SetToolBar = toolBar;
          board.WithBot = true;
          board.Diff = 2;
          board.SetFigureChess( );
          viewer = new MoveViewer( );
          viewer.Board = board;
          viewer.Chess = game;
          viewer.Reset( );
          game.MoveStack.Clear( );
          game.FenStack.Clear( );
          dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
          DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
          Dispatcher );
          dispatcherTimer.Start( );
          setting = new Setting( this );
          ListViewMenu.SelectedIndex = 0;
          break;
        }
        case 3: {
          game = new Chess( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1" );
          board = new Board( );
          board.SetChess = game;
          toolBar = new ToolBar( board );
          board.SetToolBar = toolBar;
          board.WithBot = true;
          board.Diff = 3;
          board.SetFigureChess( );
          viewer = new MoveViewer( );
          viewer.Board = board;
          viewer.Chess = game;
          viewer.Reset( );
          game.MoveStack.Clear( );
          game.FenStack.Clear( );
          dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
          DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
          Dispatcher );
          dispatcherTimer.Start( );
          setting = new Setting( this );
          ListViewMenu.SelectedIndex = 0;
          break;
        }
        case 4: {
          game = new Chess( "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1" );
          board = new Board( );
          board.SetChess = game;
          toolBar = new ToolBar( board );
          board.SetToolBar = toolBar;
          board.WithBot = true;
          board.Diff = 4;
          board.SetFigureChess( );
          viewer = new MoveViewer( );
          viewer.Board = board;
          viewer.Chess = game;
          viewer.Reset( );
          game.MoveStack.Clear( );
          game.FenStack.Clear( );
          dispatcherTimer = new DispatcherTimer( TimeSpan.FromSeconds( 1 ) ,
          DispatcherPriority.Normal , new EventHandler( dispatcherTimer_Tick ) ,
          Dispatcher );
          dispatcherTimer.Start( );
          setting = new Setting( this );
          ListViewMenu.SelectedIndex = 0;
          break;
        }
      }
    }



    public MainWindow GetMain( ) {
      return this;
    }

    private void main_window_loaded( object sender , RoutedEventArgs e ) {
      double pos = ( GridMenu.ActualHeight - ListViewMenu.ActualHeight ) / 2;

      double temp = content.ActualWidth / 3;
      toolBar.Undo.Margin = new Thickness( temp , 0 , 0 , 0 );

      MoveCursorMenu( 0 , pos );

      board.SetmainWinndow = this;

      board.GameTimer.PlayerColor = game.GetCurrentColor( );
      board.GameTimer.Enabled = game.IsCheck( ) || game.GetAllMoves( ).Count > 0;
    }

    private void MoveCursorMenu( int index , double pos ) {
      TrainsitionigContentSlide.OnApplyTemplate( );
      GridCursor.Margin = new Thickness( 0 , ( pos + ( index * 60 ) ) , 0 , 0 );
    }

    private void ListViewMenu_SelectionChanged( object sender , SelectionChangedEventArgs e ) {
      int index = ListViewMenu.SelectedIndex;
      double pos = ( GridMenu.ActualHeight - ListViewMenu.ActualHeight ) / 2;

      MoveCursorMenu( index , pos );
      double temp = content.ActualWidth / 3;
      toolBar.Undo.Margin = new Thickness( temp , 0 , 0 , 0 );


      switch ( index ) {
        case 0:
          Grid gr = new Grid( );
          gr.Width = 150;

          viewer.MinWidth = 210;
          viewer.MaxWidth = 220;
          viewer.Margin = new Thickness( 5 , 0 , 0 , 0 );

          board.MinWidth = 350;
          board.MinHeight = 350;
          board.Margin = new Thickness( 5 , 0 , 5 , 0 );

          content.Children.Clear( );
          content.Children.Add( toolBar );
          content.Children.Add( viewer );
          content.Children.Add( gr );
          content.Children.Add( board );

          DockPanel.SetDock( toolBar , Dock.Top );
          DockPanel.SetDock( viewer , Dock.Left );
          DockPanel.SetDock( gr , Dock.Right );
          break;
        case 1:
          content.Children.Clear( );
          content.Children.Add( setting );
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

      double temp = content.ActualWidth / 3;
      toolBar.Undo.Margin = new Thickness( temp , 0 , 0 , 0 );

      MoveCursorMenu( index , pos );
    }

    private void dispatcherTimer_Tick( object sender , EventArgs e ) {
      GameTimer gameTimer;

      gameTimer = board.GameTimer;
      toolBar.labelWhitePlayTime.Content = GameTimer.GetHumanElapse( gameTimer.WhitePlayTime );
      toolBar.labelBlackPlayTime.Content = GameTimer.GetHumanElapse( gameTimer.BlackPlayTime );

      if ( gameTimer.MaxWhitePlayTime.HasValue ) {
        toolBar.labelWhiteLimitPlayTime.Content = "(" + GameTimer.GetHumanElapse( gameTimer.MaxWhitePlayTime.Value ) + "/" + gameTimer.MoveIncInSec.ToString( ) + ")";
      }
      if ( gameTimer.MaxBlackPlayTime.HasValue ) {
        toolBar.labelBlackLimitPlayTime.Content = "(" + GameTimer.GetHumanElapse( gameTimer.MaxBlackPlayTime.Value ) + "/" + gameTimer.MoveIncInSec.ToString( ) + ")";
      }
    }

  }
}
