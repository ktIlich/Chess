﻿using System;
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
  /// Логика взаимодействия для Setting.xaml
  /// </summary>
  public partial class Setting : UserControl {

    MainWindow main;  

    public Setting( MainWindow parent ) {
      InitializeComponent( );
      InitSetting( );
      main = parent;
    }

    private void InitSetting( ) {
      int j = 1;
      string str = "Новая игра";
      for ( int i = 0; i < 5; i++ ) {
        StackPanel panel = new StackPanel( );
        //  Image img = GetImage( j );
        TextBlock text = new TextBlock( );
        text.Text = str;
        text.TextWrapping = TextWrapping.Wrap;
        text.FontSize = 25;
        text.TextAlignment = TextAlignment.Center;
        text.VerticalAlignment = VerticalAlignment.Center;
        panel.Name = "set" + i;
        panel.Margin = new Thickness( 20 );
        //panel.Children.Add( img );
        panel.Children.Add( text );
        panel.MouseEnter += Panel_MouseEnter;
        panel.MouseLeave += Panel_MouseLeave;
        panel.MouseDown += Panel_MouseDown;
        panel.Height = 150;
        panel.VerticalAlignment = VerticalAlignment.Center;
        SetMain.Children.Add( panel );
        Grid.SetColumn( panel , i + 1 );
        Grid.SetRow( panel , 1 );

        j = 2;
        str = $"Игра с комп. {i + 1} ур-нь.";
      }


    }

    private void Panel_MouseDown( object sender , MouseButtonEventArgs e ) {
      Panel panel = ( Panel )sender;
      switch ( panel.Name ) {
        case "set0": main.NewGame( 0 ); break;
        case "set1": main.NewGame( 1 ); break;
        case "set2": main.NewGame( 2 ); break;
        case "set3": main.NewGame( 3 ); break;
        case "set4": main.NewGame( 4 ); break;
      }
    }

    private void Panel_MouseLeave( object sender , MouseEventArgs e ) {
      ( ( Panel )sender ).Background = new SolidColorBrush( Color.FromRgb( 238 , 238 , 238 ) );
      Cursor = Cursors.Arrow;
    }

    private void Panel_MouseEnter( object sender , MouseEventArgs e ) {

      Panel panel = ( ( Panel )sender );
      panel.Background = new SolidColorBrush( Color.FromArgb( 50 , 68 , 68 , 68 ) );
      Cursor = Cursors.Hand;
    }

    private Image GetImage( int i ) {
      switch ( i ) {
        case 1: { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/new.png" , UriKind.Relative ) ); return img; }
        case 2: { Image img = new Image( ); img.Source = new BitmapImage( new Uri( $"/Resources/coding.png" , UriKind.Relative ) ); return img; }
        default: return null;
      }
    }

  }
}
