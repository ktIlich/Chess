using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
  /// Логика взаимодействия для Board.xaml
  /// </summary>
  public partial class Board : UserControl {

    public enum GameResult { };



    private Chess _chess;
    private Color LightColor;
    private Color DarkColor;
    private Border[ , ] board_map;

    public event EventHandler<EventArgs> BoardReset;
    public event EventHandler<NewMoveEventArgs> NewMove;
    public event EventHandler RedoPosChanged;

    private bool wait = true; //patern state

    int xFrom, yFrom;

    char[ ] arr_fig_symb = { 'P', 'R', 'N', 'B', 'Q' , 'K',
                            'p', 'r', 'n', 'b', 'q' , 'k', '.' };


    public Board( ) {
      InitializeComponent( );

      LightColor = ( Color )ColorConverter.ConvertFromString( "#eeeed2" );
      DarkColor = ( Color )ColorConverter.ConvertFromString( "#769656" );
      board_map = new Border[ 8 , 8 ];
      CreateChessBoard( );
    }

    public Chess GetChess {
      get => _chess;
    }

    public Chess SetChess {
      set {
        _chess = value != null ? value : new Chess( );
      }
    }


    private void CreateChessBoard( ) {
      Border cell;
      Brush brushDark = new SolidColorBrush( DarkColor );
      Brush brushLight = new SolidColorBrush( LightColor );

      for ( int y = 7; y >= 0; y-- ) {
        for ( int x = 0; x < 8; x++ ) {
          cell = new Border( );
          cell.Name = "C" + x + y;
          cell.BorderThickness = new Thickness( 0 );
          cell.Background = ( ( ( x + y ) % 2 ) == 0 ) ? brushLight : brushDark;
          cell.BorderBrush = cell.Background;
          cell.MouseDown += Cell_MouseDown;
          board_map[ x , y ] = cell;
          ChessBoard.Children.Add( cell );
        }
      }
    }

    public void SetFigureChess( ) {
      for ( int y = 7; y >= 0; y-- ) {
        for ( int x = 0; x < 8; x++ ) {
          SetFigure( x , y , _chess.GetFigureAt( x , y ) );
        }
      }
      MarkCell( );
    }

    private void SetFigure( int x , int y , char figure ) {
      Border cell;
      UserControl cell_figure;
      cell = board_map[ x , y ];
      cell_figure = new UserControl( );

      if ( cell_figure != null && figure != '.' ) {
        cell_figure.Content = GetFigure( figure );

        cell_figure.Margin = ( cell.BorderThickness.Top == 0 ) ? new Thickness( 1 ) : new Thickness( 3 );
      }
      cell.Child = cell_figure;
    }



    private Image GetFigure( Char figure ) {
      switch ( figure ) {
        case 'P': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhitePawn.png" , UriKind.Relative ) ); return img; }
        case 'p': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackPawn.png" , UriKind.Relative ) ); return img; }
        case 'N': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhiteKnight.png" , UriKind.Relative ) ); return img; }
        case 'n': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackKnight.png" , UriKind.Relative ) ); return img; }
        case 'B': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhiteBishop.png" , UriKind.Relative ) ); return img; }
        case 'b': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackBishop.png" , UriKind.Relative ) ); return img; }
        case 'R': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhiteRook.png" , UriKind.Relative ) ); return img; }
        case 'r': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackRook.png" , UriKind.Relative ) ); return img; }
        case 'Q': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhiteQueen.png" , UriKind.Relative ) ); return img; }
        case 'q': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackQueen.png" , UriKind.Relative ) ); return img; }
        case 'K': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/WhiteKing.png" , UriKind.Relative ) ); return img; }
        case 'k': { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/BlackKing.png" , UriKind.Relative ) ); return img; }
        default: return null;
      }
    }


    private void Button_Click( object sender , RoutedEventArgs e ) {

    }

    private void Cell_MouseDown( object sender , RoutedEventArgs e ) {
      Border cell_select = ( Border )sender;
      UserControl cell_figure = ( UserControl )cell_select.Child;

      string xy = cell_select.Name.Substring( 1 );
      int x = int.Parse( xy[ 0 ].ToString( ) );
      int y = int.Parse( xy[ 1 ].ToString( ) );
      if ( wait ) {
        wait = false;
        xFrom = x;
        yFrom = y;
      }
      else {
        wait = true;

        string figure = _chess.GetFigureAt( xFrom , yFrom ).ToString( );
        string move = figure + ToCoord( xFrom , yFrom ) + ToCoord( x , y );
        _chess = _chess.Move( move );
        
        OnNewMove( new NewMoveEventArgs( move ) );
      }

      //cell_figure.BorderBrush = new SolidColorBrush( Color.FromRgb( 55 , 55 , 55 ) );
      //cell_figure.BorderThickness = new Thickness( 3 );

      //if ( int.TryParse( cell_select.Name[ 1 ].ToString( ) , out start_pos[ 0 ] ) &&
      //    int.TryParse( cell_select.Name[ 2 ].ToString( ) , out start_pos[ 1 ] ) ) {

      //  move_figure = _chess.GetFigureAt( start_pos[ 0 ] , start_pos[ 1 ] );
      //  MessageBox.Show( start_pos[ 0 ].ToString( ) + start_pos[ 1 ].ToString( ) );

      //  if ( move_figure != '.' ) {
      //    cell_figure.BorderBrush = new SolidColorBrush( Color.FromRgb( 55 , 55 , 55 ) );
      //    cell_figure.BorderThickness = new Thickness( 3 );
      //  }
      //}
      SetFigureChess( );
    }

    private void MarkCellFrom( ) { // Pe2e4 xy = e2
      foreach ( string move in _chess.GetAllMoves( ) ) {
        string xy = move.Substring( 1 , 2 );
        int x = xy[ 0 ] - 'a';
        int y = xy[ 1 ] - '1';

        UserControl ctrl = ( UserControl )board_map[ x , y ].Child;
        ctrl.Margin = new Thickness( 2 );
        ctrl.BorderBrush = new SolidColorBrush( Color.FromRgb( 87 , 209 , 0 ) );
        ctrl.BorderThickness = new Thickness( 3 );
      }
    }

    private void RefreshBoard( ) {
      for ( int y = 7; y >= 0; y-- ) {
        for ( int x = 0; x < 8; x++ ) {
          UserControl control = ( UserControl )board_map[ x , y ].Child;
          control.Margin = new Thickness( 0 );
          control.BorderThickness = new Thickness( 0 );
        }
      }
    }


    private string ToCoord( int x , int y ) {
      return ( ( char )( 'a' + x ) ).ToString( ) + ( ( char )( '1' + y ) ).ToString( );
    }

    private void MarkCell( ) {
      RefreshBoard( );
      if ( wait ) {
        MarkCellFrom( );
      }
      else {
        MarkCellTo( );
      }
    }

    private void MarkCellTo( ) { // Pe2e4 xy = e4
      string suffix = _chess.GetFigureAt( xFrom , yFrom ) + ToCoord( xFrom , yFrom );
      foreach ( string move in _chess.GetAllMoves( ) ) {
        if ( move.StartsWith( suffix ) ) {
          string xy = move.Substring( 3 , 2 );
          int x = xy[ 0 ] - 'a';
          int y = xy[ 1 ] - '1';

          UserControl ctrl = ( UserControl )board_map[ x , y ].Child;
          ctrl.Margin = new Thickness( 2 );
          ctrl.BorderBrush = new SolidColorBrush( Color.FromRgb( 87 , 209 , 0 ) );
          ctrl.BorderThickness = new Thickness( 3 );
        }
      }

    }




    private void Button_MouseMove( object sender , MouseEventArgs e ) {

    }

    private void Button_MouseUp( object sender , MouseButtonEventArgs e ) {

    }

    public class NewMoveEventArgs : EventArgs {
      public string Move { get; private set; }

      public NewMoveEventArgs( string Move ) {
        this.Move = Move;
      }

    }

    protected void OnRedooPosChanged( EventArgs e ) {
      if ( RedoPosChanged != null ) {
        RedoPosChanged( this , e );
      }
    }

    protected void OnNewMove( NewMoveEventArgs e ) {
      if ( NewMove != null ) {
        NewMove( this , e );
      }
    }

    protected void OnBoardReset( EventArgs e ) {
      if ( BoardReset != null ) {
        BoardReset( this , e );
      }
    }

  }
}
