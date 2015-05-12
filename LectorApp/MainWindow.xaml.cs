using System;
using System.Windows;
using WebSocketSharp;

namespace LectorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebSocket WS;

        public MainWindow()
        {
            InitializeComponent();
            WS = new WebSocket("ws://localhost:8080");

            WS.OnOpen += conexionAbierta;

            WS.Connect();
        }

        void conexionAbierta(object sender, EventArgs e)
        {
            WS.Send("lectorStandby");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WS.Send("lectorOff");
            WS.Close();
            this.Close();
        }
    }
}
