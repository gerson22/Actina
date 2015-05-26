using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using NopalSocketServer.Servicios;

namespace NopalSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer(4649);

            // Que no desconecte sesiones inactivas:
            wssv.KeepClean = false;

            // Registrar servicios:
            wssv.AddWebSocketService<Eco>("/Eco");
            wssv.AddWebSocketService<Actina>("/Actina");

            wssv.Start();
            if (wssv.IsListening)
            {
                Console.WriteLine("Escuchando en el puerto {0}, se proveen los siguientes servcios: ", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            Console.WriteLine("\nPresiona cualquier tecla para cerrar...");
            Console.ReadLine();

            wssv.Stop();
        }
    }
}
