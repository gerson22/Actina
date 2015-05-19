using System.Windows.Forms;
using System;
using System.Drawing;

namespace LectorApp
{
    public partial class Form1 : Form
    {
        Lector lector;
        Sockets socket;
        Servidor servidor;

        public Form1()
        {
            InitializeComponent();

            IniciarLector();
            IniciarServidor();
            IniciarWebSockets();
        }

        void IniciarLector()
        {
            try
            {
                lector = new Lector();
                lector.inicializar();
                rtbEventos.AppendText("Lector iniciado.\n");
            }
            catch (Exception ex)
            {
                rtbEventos.AppendText("Error al iniciar el lector. " + ex.Message + "\n");
            }
        }

        void IniciarServidor()
        {
            servidor = new Servidor(this);
        }

        void IniciarWebSockets()
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
                    if (mensaje.Length >= 50) mensaje = mensaje.Substring(0, 50);
                    rtbEventos.AppendText("Mensaje recibido: " + mensaje + ".\n");
                    break;
            }
        }

        private void bSalir_Click(object sender, EventArgs e)
        {
            socket.mandarMensaje(2, "lectorApagado");
            this.Close();
        }

        private void bIniciar_Click(object sender, EventArgs e)
        {
            if (servidor.iniciarServidor())
            {
                rtbEventos.AppendText("Servidor iniciado en el puerto " + servidor.Puerto + ".\n");
            }
            else
            {
                rtbEventos.AppendText("Servidor detenido.\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            socket.mandarMensaje(0, tbMensaje.Text);
        }

        private void bIniciarCliente_Click(object sender, EventArgs e)
        {
            socket.Conectar();
            rtbEventos.AppendText("Cliente socket conectado\n");
        }
    }
}
