using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLib;

namespace ChessWPF {
  public class Bot {

    static Random rand = new Random( );


    public string GetBestMove( Chess chess , int diff ) {
      switch ( diff ) {
        case 1: return CalculatebestMove_1( chess );
        case 2: return CalculatebestMove_2( chess );
        case 3: return MinMaxRoot( 2 , chess , true );
        case 4: return MinMaxRootAB( 2 , chess , true );
        default: return MinMaxRootAB( 2 , chess , true );
      }
    }



    private string CalculatebestMove_1( Chess chess ) {
      List<string> moves = chess.GetAllMoves( );
      return moves[ rand.Next( moves.Count ) ];
    }

    private string CalculatebestMove_2( Chess chess ) {
      List<string> all_moves = chess.GetAllMoves( );
      string bestMove = null;
      int bestValue = -9999;
      for ( int i = 0; i < all_moves.Count; i++ ) {
        string newGameMove = all_moves[ i ];
        chess = chess.Move( newGameMove );
        int boardValue = -EvaluateBoard( chess );
        chess = chess.UndoMove( );
        if ( boardValue > bestValue ) {
          bestValue = boardValue;
          bestMove = newGameMove;
        }
      }
      return bestMove;
    }

    private string MinMaxRoot( int depth , Chess chess , bool isMaximisingPlayer ) {
      List<string> all_moves = chess.GetAllMoves( );
      int bestMove = -9999;
      string bestMoveFound = null;
      for ( int i = 0; i < all_moves.Count; i++ ) {
        string newGameMove = all_moves[ i ];
        chess = chess.Move( newGameMove );
        int value = MinMax( depth - 1 , chess , !isMaximisingPlayer );
        chess = chess.UndoMove( );
        if ( value >= bestMove ) {
          bestMove = value;
          bestMoveFound = newGameMove;
        }
      }
      return bestMoveFound;
    }

    private string MinMaxRootAB( int depth , Chess chess , bool isMaximisingPlayer ) {
      List<string> all_moves = chess.GetAllMoves( );
      int bestMove = -9999;
      string bestMoveFound = null;
      for ( int i = 0; i < all_moves.Count; i++ ) {
        string newGameMove = all_moves[ i ];
        chess = chess.Move( newGameMove );
        int value = MinMaxAlpaBeta( depth - 1 , chess , -10000 , 10000 , !isMaximisingPlayer );
        chess = chess.UndoMove( );
        if ( value >= bestMove ) {
          bestMove = value;
          bestMoveFound = newGameMove;
        }
      }
      return bestMoveFound;
    }

    private static int MinMax( int depth , Chess chess , bool isMaximisingPlayer ) {
      if ( depth == 0 ) {
        return -EvaluateBoard( chess );
      }
      List<string> all_moves = chess.GetAllMoves( );
      if ( isMaximisingPlayer ) {
        int bestMove = -9999;
        for ( int i = 0; i < all_moves.Count; i++ ) {
          chess = chess.Move( all_moves[ i ] );
          bestMove = Math.Max( bestMove , MinMax( depth - 1 , chess , !isMaximisingPlayer ) );
          chess = chess.UndoMove( );
        }
        return bestMove;
      }
      else {
        int bestMove = 9999;
        for ( int i = 0; i < all_moves.Count; i++ ) {
          chess = chess.Move( all_moves[ i ] );
          bestMove = Math.Min( bestMove , MinMax( depth - 1 , chess , !isMaximisingPlayer ) );
          chess = chess.UndoMove( );
        }
        return bestMove;
      }
    }

    private static int MinMaxAlpaBeta( int depth , Chess chess , int alpha , int beta , bool isMaximisingPlayer ) {
      if ( depth == 0 ) {
        return -EvaluateBoard( chess );
      }
      List<string> all_moves = chess.GetAllMoves( );
      if ( isMaximisingPlayer ) {
        int bestMove = -9999;
        for ( int i = 0; i < all_moves.Count; i++ ) {
          chess = chess.Move( all_moves[ i ] );
          bestMove = Math.Max( bestMove , MinMaxAlpaBeta( depth - 1 , chess , alpha , beta , !isMaximisingPlayer ) );
          chess = chess.UndoMove( );
          alpha = Math.Max( alpha , bestMove );
          if ( beta <= alpha ) {
            return bestMove;
          }
        }
        return bestMove;
      }
      else {
        int bestMove = 9999;
        for ( int i = 0; i < all_moves.Count; i++ ) {
          chess = chess.Move( all_moves[ i ] );
          bestMove = Math.Min( bestMove , MinMaxAlpaBeta( depth - 1 , chess , alpha , beta , !isMaximisingPlayer ) );
          chess = chess.UndoMove( );
          alpha = Math.Min( beta , bestMove );
          if ( beta <= alpha ) {
            return bestMove;
          }
        }
        return bestMove;
      }
    }


    private static int EvaluateBoard( Chess chess ) {
      int totalEvaluation = 0;
      for ( int i = 0; i < 8; i++ ) {
        for ( int j = 0; j < 8; j++ ) {
          totalEvaluation += GetFigureValue( chess.GetFigureAt( i , j ) );
        }
      }
      return totalEvaluation;
    }

    private static int GetFigureValue( char v ) {
      if ( v == '.' ) {
        return 0;
      }
      if ( v == 'P' ) return 10;
      else if ( v == 'p' ) return -10;
      else if ( v == 'N' ) return 30;
      else if ( v == 'n' ) return -30;
      else if ( v == 'B' ) return 30;
      else if ( v == 'b' ) return -30;
      else if ( v == 'R' ) return 50;
      else if ( v == 'r' ) return -50;
      else if ( v == 'Q' ) return 90;
      else if ( v == 'q' ) return -90;
      else if ( v == 'K' ) return 900;
      else if ( v == 'k' ) return -900;
      return 0;
    }


  }
}
