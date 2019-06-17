namespace ChessWPF {
  using System;
  using System.Data.Entity;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;

  public partial class MoveDB : DbContext {
    public MoveDB( )
        : base( "name=MoveDB" ) {
    }

    public virtual DbSet<Move> Moves { get; set; }

    protected override void OnModelCreating( DbModelBuilder modelBuilder ) {
    }
  }
}
