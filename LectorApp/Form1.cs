using System.Windows.Forms;
using System;
using System.Threading;

namespace LectorApp
{
    public partial class Form1 : Form
    {
        Lector lector;
        Sockets socket;
        Thread oThread;

        public Form1()
        {
            InitializeComponent();

            IniciarLector();
            IniciarWebSockets();
            oThread = new Thread(new ThreadStart(this.modoAcceso));
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

        void IniciarWebSockets()
        {
            socket = new Sockets(this);
        }

        public void mensajeRecibido(string mensaje)
        {
            if (mensaje.Length <= 50) Console.WriteLine("[Cliente] Mensaje recibido: " + mensaje);
            else Console.WriteLine("[Cliente] Mensaje recibido (Trimmed): " + mensaje.Substring(0, 50));
            switch (mensaje)
            {
                case "ping":
                    socket.mandarMensaje(1, "lectorActivado");
                    break;
                case "inscribirHuella":
                    string data = lector.inscribirHuella(socket);
                    socket.mandarMensaje(4, data);
                    rtbEventos.AppendText("Huella enviada.\n");
                    break;
                case "modoAcceso":
                    iniciarModoAcceso();
                    break;
                case "sleep":
                    terminarModoAcceso();
                    break;
                default:
                    if (mensaje.Length >= 50) mensaje = mensaje.Substring(0, 50);
                    rtbEventos.AppendText("Mensaje recibido: " + mensaje + ".\n");
                    break;
            }
        }

        private void modoAcceso()
        {
            while (true)
            {
                Console.WriteLine("modoAcceso.");
                string fmd = lector.LeerHuella();
                int usuarioID = lector.GetUsuarioID(fmd);
                socket.mandarMensaje(6, Convert.ToString(usuarioID));
            }
        }

        private void iniciarModoAcceso()
        {
            oThread = new Thread(new ThreadStart(this.modoAcceso));
            oThread.Start();
            while (!oThread.IsAlive) ;
            Thread.Sleep(1);
        }

        private void terminarModoAcceso()
        {
            if(oThread.IsAlive)
                oThread.Abort();
        }

        private void bSalir_Click(object sender, EventArgs e)
        {
            socket.mandarMensaje(2, "lectorApagado");
            this.Close();
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
