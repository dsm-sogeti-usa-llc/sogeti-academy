using System.Web;

namespace Sogeti.Academy.Api.General.Http
{
    public interface IServer
    {
        string MapPath(string virtualPath);
    }

    public class Server : IServer
    {
        public string MapPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }
    }
}
