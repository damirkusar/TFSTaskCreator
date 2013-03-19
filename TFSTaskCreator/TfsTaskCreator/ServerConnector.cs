using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsTaskCreator
{
    public class ServerConnector
    {
        private readonly ServerXMLParser serverXmlParser;
        private readonly string tfsServer;
        private readonly string teamProject;

        public ServerConnector()
        {
            serverXmlParser = new ServerXMLParser();
            this.tfsServer = serverXmlParser.GetTfsServer();
            this.teamProject = serverXmlParser.GetTeamProject();

            this.SetupServer();
            this.SetupProject();
        }

        public WorkItemStore Store { get; private set; }
        public Project Project { get; private set; }

        private void SetupServer()
        {
            var tfs = new TfsTeamProjectCollection(TfsTeamProjectCollection.GetFullyQualifiedUriForName(tfsServer));
            this.Store = tfs.GetService<WorkItemStore>();
        }

        private void SetupProject()
        {
            this.Project = this.Store.Projects[teamProject];
        }
    }
}
