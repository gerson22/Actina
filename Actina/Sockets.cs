using System;
using WebSocketSharp;

namespace LectorApp
{
    class Sockets
    {
        public WebSocket WS;
        Form1 forma;

        public Sockets(Form1 forma)
        {
            this.forma = forma;
            WS = new WebSocket("ws://localhost:4649/Actina");

            WS.OnOpen += conexionAbierta;
            WS.OnMessage += (sender, e) => forma.mensajeRecibido(e.Data);
            WS.OnClose += WS_OnClose;
            WS.OnError += WS_OnError;
        }

        void WS_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: " + e.ToString());
        }

        void WS_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine("Cliente socket desconectado.\n");
        }

        public void Conectar()
        {
            WS.Connect();
        }

        void conexionAbierta(object sender, EventArgs e)
        {
            //mandarMensaje(1, "Cliente activado");
        }

        public void mandarMensaje(int codigo, string data)
        {
            WS.Send("{\"code\":\"" + codigo + "\", \"data\": \"" + data + "\"}");
        }
    }
}
