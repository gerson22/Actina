using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NopalSocketServer.Servicios
{
    class Actina : WebSocketBehavior
    {
        private string      _name;
        private static int  _number = 0;
        private string      _prefijo;

        public Actina () : this (null)
        {

        }

        public Actina(string prefijo)
        {
            _prefijo = prefijo != null ? prefijo : "Actina#";
        }

        private string getName()
        {
            return _prefijo + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment (ref _number);
        }

        #region Overrides
        protected override void OnOpen()
        {
            _name = getName ();
            Console.WriteLine(String.Format("{0} se conectó...", _name));
        }

        protected override void OnMessage (MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data);
            Console.WriteLine(e.Data);
        }

        protected override void OnClose (CloseEventArgs e)
        {
            Sessions.Broadcast(String.Format("{0} se desconectó...", _name));
            Console.WriteLine(String.Format("{0} se desconectó...", _name));
        }
        #endregion
    }
}
