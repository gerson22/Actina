using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NopalSocketServer.Servicios
{
    class Eco : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var name = Context.QueryString["name"];
            var msg = !name.IsNullOrEmpty() ? String.Format("'{0}' para {1}", e.Data, name) : e.Data;
            Send(msg);
        }
    }
}
