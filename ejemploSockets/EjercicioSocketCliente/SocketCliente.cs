using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketCliente
{
    internal class SocketCliente
    {

        const string IP_Servidor = "127.0.0.1";
        const int PUERTO = 1002;
        byte[] BytesEntrada = new byte[1024];

        public void IniciarSocketCliente() 
        {
            IPEndPoint direccionServidor = new IPEndPoint(IPAddress.Parse(IP_Servidor), PUERTO);
            Socket socketCliente = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);//Tiene toda la comunicacion con el servidor
            try
            { 
                socketCliente.Connect(direccionServidor);
                Console.WriteLine("Conexión realizada con éxito al servidor...");
                string mensaje = "";
                do
                {
                    Console.WriteLine("Empece el DO");
                    //Envió
                    Console.WriteLine("Escribe el mensaje para enviarse al servidor: ");
                    mensaje = Console.ReadLine();
                    byte[] bytesMensaje = Encoding.ASCII.GetBytes(mensaje);
                    socketCliente.Send(bytesMensaje);

                    //Recepción
                    int numeroBytesRecibidos = socketCliente.Receive(BytesEntrada);
                    string respuestaServidor = Encoding.ASCII.GetString(BytesEntrada, 0 , numeroBytesRecibidos);
                    Console.WriteLine("Respuesta servidor: " + respuestaServidor);
                }while (!mensaje.ToLower().Equals("salir"));
                socketCliente.Shutdown(SocketShutdown.Both);
                socketCliente.Close();
                Console.WriteLine("Conexión cerrada con el servidor");
            }catch (Exception ex)
            {
                Console.WriteLine("Error en la conexión/comunicación con el servidor: " + ex.Message);

            }
        }
    }
}
