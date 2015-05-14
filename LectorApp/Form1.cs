using System.Windows.Forms;
using System;
using System.Drawing;

namespace LectorApp
{
    public partial class Form1 : Form
    {
        Lector lector;
        Sockets socket;

        public Form1()
        {
            InitializeComponent();

            iniciarLector();
            iniciarWebSockets();
        }

        void iniciarLector()
        {
            try
            {
                lector = new Lector(this);
                lector.inicializar();
                rtbEventos.AppendText("Lector iniciado.\n");
            }
            catch (Exception ex)
            {
                rtbEventos.AppendText("Error al iniciar el lector. " + ex.Message + "\n");
            }
        }

        void iniciarWebSockets()
        {
            socket = new Sockets(this);
        }

        public void mensajeRecibido(string mensaje)
        {
            switch (mensaje)
            {
                case "ping":
                    if (socket.WS.IsAlive) socket.mandarMensaje(1, "lectorActivado");
                    break;
                case "inscribirHuella":
                    string data = lector.inscribirHuella(socket);
                    socket.mandarMensaje(4, data);
                    rtbEventos.AppendText("Huella enviada.\n");
                    break;
                default:
                    break;
            }
        }

        private void bSalir_Click(object sender, EventArgs e)
        {
            socket.mandarMensaje(2, "lectorApagado");
            this.Close();
        }
    }
}
