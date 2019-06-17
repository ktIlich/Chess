using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

  public class MoveItemList : ObservableCollection<Move> { }

  public class NewMoveSelectedEventArgs : System.ComponentModel.CancelEventArgs {
    public int NewIndex;
    public NewMoveSelectedEventArgs( int index ) : base( false ) {
      NewIndex = index;
    }
  }

  public partial class MoveViewer : UserControl {


    private Board _board;
    private Chess _chess;
    public MoveItemList MoveList { get; private set; }
    public event EventHandler<NewMoveSelectedEventArgs> NewMoveSelected;
    private MoveDB db;
    private ObservableCollection<Move> MovesList;


    public MoveViewer( ) {
      InitializeComponent( );

      MovesList = new ObservableCollection<Move>( );
      MoveList = listViewMoveList.ItemsSource as MoveItemList;
      listViewMoveList.SelectionChanged += new SelectionChangedEventHandler( listViewMoveList_SelectionChanged );
    }



    public Board Board {
      get => this._board;
      set {
        if ( _board != value ) {

          if ( _board != null ) {
            _board.BoardReset -= BoardReset;
            _board.NewMove -= NewMove;
            _board.RedoPosChanged -= RedoPosChanged;
          }
          _board = value;
          if ( _board != null ) {
            _board.BoardReset += BoardReset;
            _board.NewMove += NewMove;
            _board.RedoPosChanged += RedoPosChanged;
          }

        }
      }
    }

    public Chess Chess {
      get => _chess;
      set {
        _chess = value;
      }
    }


    private void AddCurrenMove( ) {
      Move move_item;
      string str_move;
      int MoveIndex;
      int MoveCount;
      int ItemCount;
      string Player;
      string FEN;
      Chess chess;

      chess = _chess;
      str_move = chess.MoveStack.CurrentMove;
      FEN = chess.fen;
      
      chess = chess.UndoMove( );
      Player = chess.GetCurrentColor( );
      MoveCount = chess.MoveStack.Count;
      ItemCount = listViewMoveList.Items.Count;
      while ( ItemCount >= MoveCount ) {
        ItemCount--;
        MoveList.RemoveAt( ItemCount );
      }
      chess = chess.RedoMove( );
      MoveIndex = ItemCount;
      move_item = new Move( MoveIndex + 1 , Player ,
                            str_move , FEN );
      MoveList.Add( move_item );
    }

    private void SelectCurrentMove( ) {
      int index;
      Move move_item;
      Chess chess;

      chess = _chess;
      index = chess.MoveStack.PositionInList;

      if ( index == -1 ) {
        listViewMoveList.SelectedItem = null;
      }
      else {
        move_item = listViewMoveList.Items[ index ] as Move;
        listViewMoveList.SelectedItem = move_item;
        listViewMoveList.ScrollIntoView( move_item );
      }

    }

    private void Reset( ) {
      int CurPos;
      int Count;
      Chess chess;

      MoveList.Clear( );
      chess = _chess;
      CurPos = chess.MoveStack.PositionInList;
      Count = chess.MoveStack.Count;
      chess = chess.UndoAllMoves( );

      for ( int index = 0; index < Count; index++ ) {
        chess = chess.RedoMove( );
        AddCurrenMove( );
      }
      SelectCurrentMove( );
    }



    private void OnNewMoveSelected( NewMoveSelectedEventArgs e ) {
      if ( NewMoveSelected != null ) {
        NewMoveSelected( this , e );
      }
    }

    private void BoardReset( object sender , EventArgs e ) {
      Reset( );
    }

    private void NewMove( object sender , Board.NewMoveEventArgs e ) {
      AddCurrenMove( );
      SelectCurrentMove( );
    }

    private void RedoPosChanged( object sender , EventArgs e ) {
      SelectCurrentMove( );
    }

    private void listViewMoveList_SelectionChanged( object sender , SelectionChangedEventArgs e ) {
      NewMoveSelectedEventArgs eventArgs;
      int CurPos;
      int NewPos;
      Chess chess;

      chess = _chess;
      CurPos = chess.MoveStack.PositionInList;
      if ( e.AddedItems.Count != 0 ) {
        NewPos = listViewMoveList.SelectedIndex;

        if ( NewPos != CurPos ) {
          eventArgs = new NewMoveSelectedEventArgs( NewPos );
          OnNewMoveSelected( eventArgs );

          if ( eventArgs.Cancel ) {
            if ( CurPos == -1 ) {
              listViewMoveList.SelectedItems.Clear( );
            }
            else {
              listViewMoveList.SelectedIndex = CurPos;
            }
          }
        }
      }
    }

  }
}
