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
            WS = new WebSocket("ws://localhost:4649/Biometria");

            WS.OnOpen += conexionAbierta;
            WS.OnMessage += (sender, e) => forma.mensajeRecibido(e.Data);
        }

        public void Conectar()
        {
            WS.Connect();
        }

        void conexionAbierta(object sender, EventArgs e)
        {
            //mandarMensaje(1, "Cliente activado");
        }

        public bool Status()
        {
            if (WS.IsAlive) return true;
            else return false;
        }

        public void mandarMensaje(int codigo, string data)
        {
            // 1. LectorActivado
            // 2. LectorApagado
            // 3. Lectura capturada
            // 4. FMD
            // 5. Huella

            if(data.Length > 10)
            {
                forma.rtbEventos.AppendText("Enviando mensaje: {'code': '" + codigo + "', 'data': '" + data.Substring(0, 10) + "'\n");
            }
            else
            {
                forma.rtbEventos.AppendText("Enviando mensaje: {'code': '" + codigo + "', 'data': '" + data + "'\n");
            }
            WS.Send("{\"code\":\"" + codigo + "\", \"data\": \"" + data + "\"}");
        }
    }
}
