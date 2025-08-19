using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DndToolkit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener server = new HttpListener();

            server.Prefixes.Add("http://localhost:8888/");

            server.Start();

            Console.WriteLine("Server started");

            while (true)
            {

                var context = server.GetContext();

                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        Console.WriteLine("Get");
                        HttpMethods.WriteResponse(context.Response, "hello from server");
                        break;
                    case "POST":
                        Console.WriteLine("Post");
                        break;
                    case "PUT":
                        Console.WriteLine("Put");
                        break;
                    case "DELETE":
                        Console.WriteLine("Delete");
                        break;
                    default:
                        Console.WriteLine("Unknown request");
                        break;
                }
            }
        }
    }
}


static class HttpMethods
{
    public static void WriteResponse(HttpListenerResponse response, string content)
    {
        var bytes = Encoding.UTF8.GetBytes(content);

        using (var stream = response.OutputStream)
        {
            stream.Write(bytes);
        }

        response.Close();
    }
}