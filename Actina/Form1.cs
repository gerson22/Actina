using System.Windows.Forms;
using System;
using System.Threading;

namespace LectorApp
{
    public partial class Form1 : Form
    {
        Lector lector;
        Sockets socket;
        Thread accesoThread;
        Thread inscripcionThread;

        public Form1()
        {
            InitializeComponent();

            IniciarLector();
            IniciarWebSockets();
            accesoThread = new Thread(new ThreadStart(this.modoAcceso));
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
                    //iniciarModoInscripcion();
                    this.terminarModoAcceso();
                    string data = lector.inscribirHuella(socket);
                    if (data == "0")
                        socket.mandarMensaje(0, null);
                    else
                        socket.mandarMensaje(4, data);
                    break;
                case "modoAcceso":
                    lector.actualizarListaUsuarios();
                    iniciarModoAcceso();
                    break;
                case "sleep":
                    terminarModoAcceso();
                    break;
                default:
                    if (mensaje.Length >= 50) mensaje = mensaje.Substring(0, 50);
                    break;
            }
        }

        #region Inscripcion
        private void modoInscripcion()
        {
            while(true)
            {
                try
                {
                    Console.WriteLine("modoInscripcion");
                    string data = lector.inscribirHuella(socket);
                    socket.mandarMensaje(4, data);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }

        private void iniciarModoInscripcion()
        {
            inscripcionThread = new Thread(new ThreadStart(this.modoInscripcion));
        }
        #endregion

        #region Acceso
        bool modoAccesoON = false;
        private void modoAcceso()
        {
            while (modoAccesoON)
            {
                try
                {
                    Console.WriteLine("modoAcceso.");
                    string fmd = lector.LeerHuella();
                    int usuarioID = lector.GetUsuarioID(fmd);
                    socket.mandarMensaje(6, Convert.ToString(usuarioID));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    terminarModoAcceso();
                }
            }
        }

        private void iniciarModoAcceso()
        {
            modoAccesoON = true;
            accesoThread = new Thread(new ThreadStart(this.modoAcceso));
            accesoThread.Start();
            while (!accesoThread.IsAlive) ;
            Thread.Sleep(10);
        }

        private void terminarModoAcceso()
        {
            modoAccesoON = false;
            /*if (accesoThread.IsAlive)
                accesoThread.Abort();*/
        }
        #endregion

        private void bSalir_Click(object sender, EventArgs e)
        {
            socket.mandarMensaje(2, "lectorApagado");
            terminarModoAcceso();
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
