using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketServidor
{
    internal class SocketServidor
    {
        const string IP_SERVIDOR = "127.0.0.1";
        const int PUERTO = 1002;

        public void IniciarServidor()
        {
            IPEndPoint direccionIP = new IPEndPoint(IPAddress.Parse(IP_SERVIDOR), PUERTO);
            Socket socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Pondremos en linea/visible el servidor para escuchar peticiones
            socketServidor.Bind(direccionIP);
            socketServidor.Listen();
            Console.WriteLine("Servidor listo para recibir conexion");
        try{

                Socket socketClienteRemoto = socketServidor.Accept();
                Console.WriteLine("Cliente conectado correctamente");
                string mensaje = "";
                do
                {
                    //Recibiendo Informacion
                    byte[] BytesEntrada = new byte[1024];
                    int numeroBytesEntrada = socketClienteRemoto.Receive(BytesEntrada, 0, BytesEntrada.Length, 0);
                    mensaje = Encoding.ASCII.GetString(BytesEntrada);
                    Console.WriteLine("El mensaje del cliente es : " + mensaje);

                    //Enviando Informacion
                    String mensajeRespuesta = "Mensaje recibido en el servidor";
                    byte[] bytesMensajeRespuesta = Encoding.ASCII.GetBytes(mensajeRespuesta);
                    socketClienteRemoto.Send(bytesMensajeRespuesta);
                } while (!mensaje.ToLower().Equals("salir"));

                //Cerrando la conexion
                socketServidor.Close();

            }catch(Exception ex)
            {
                Console.WriteLine("Cierre de conexion...");
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Fin de la ejecucion de servidor...");
        }
    }
}
