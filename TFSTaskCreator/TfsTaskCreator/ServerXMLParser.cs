using System.Xml.Linq;

namespace TfsTaskCreator
{
    public class ServerXMLParser
    {
        private readonly XElement xml;

        public ServerXMLParser()
        {
            string fileName = string.Format("{0}", "Server.xml");
            xml = XElement.Load(fileName, LoadOptions.None);
        }

        public string GetTfsServer()
        {
            var tfsServer = xml.Element("TFSServer");
            return tfsServer.Value;
        }

        public string GetTeamProject()
        {
            return xml.Element("TeamProject").Value;
        }
    }
}
