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
            WS = new WebSocket("ws://localhost:8080");

            WS.OnOpen += conexionAbierta;
            WS.OnMessage += (sender, e) => forma.mensajeRecibido(e.Data);

            WS.Connect();
        }

        void conexionAbierta(object sender, EventArgs e)
        {
            mandarMensaje(1, "lectorActivado");
        }

        public void mandarMensaje(int codigo, string data)
        {
            // 1. LectorActivado
            // 2. LectorApagado
            // 3. Lectura capturada
            // 4. FMD
            // 5. Huella

            if (codigo == 5) forma.rtbEventos.AppendText("Enviado: " + "{\"code\":\"" + codigo + "\", \"data\": \"Base64string\"}\n");
            else forma.rtbEventos.AppendText("Enviado: " + "{\"code\":\"" + codigo + "\", \"data\": \"" + data + "\"}\n");
            WS.Send("{\"code\":\"" + codigo + "\", \"data\": \"" + data + "\"}");
        }
    }
}
