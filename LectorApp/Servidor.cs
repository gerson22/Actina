using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace LectorApp
{
    public class Servidor : WebSocketBehavior
    {
        WebSocketServer wssv;
        Form1 _form;

        public Servidor(Form1 forma)
        {
            this._form = forma;
            wssv = new WebSocketServer(4649);
            //wssv.AddWebSocketService<Biometria>("/Biometria");
        }
        
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
            _form.rtbEventos.AppendText(e.Data);
        }

        public bool iniciarServidor()
        {
            if(!wssv.IsListening)
                wssv.Start();
            return wssv.IsListening;
        }

        public void detenerServidor()
        {
            wssv.Stop();
        }

        public bool Estado()
        {
            return wssv.IsListening;
        }

        public int Puerto
        {
            get { return wssv.Port; }
        }
    }

    /*public class Biometria : WebSocketBehavior
    {
        public Biometria()
        {

        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data);
            if (e.Data.Length <= 50)
                Console.WriteLine("[Servidor] Mensaje recibido: " + e.Data);
            else
                Console.WriteLine("[Servidor] Mensaje recibido (trimmed): " + e.Data.Substring(0, 50));
        }
    }*/
}
