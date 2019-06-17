namespace ChessWPF {
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Spatial;

  [Table( "Move" )]
  public partial class Move {
    
    [Key]
    [DatabaseGenerated( DatabaseGeneratedOption.None )]
    public int MOVE_ID { get; set; }

    public int MOVE_NUMBER { get; set; }

    [Required]
    [StringLength( 5 )]
    public string PLAYER_COLOR { get; set; }

    [Column( "MOVE" )]
    [Required]
    [StringLength( 15 )]
    public string MOVE1 { get; set; }

    [StringLength( 60 )]
    public string FEN { get; set; }

    public Move( int number , string color , string move , string fen ) {
      MOVE_ID = number;
      MOVE_NUMBER = number;
      PLAYER_COLOR = color;
      MOVE1 = move;
      FEN = fen;
    }
  }
}
