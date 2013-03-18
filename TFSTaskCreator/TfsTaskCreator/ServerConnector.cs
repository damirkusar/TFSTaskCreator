using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsTaskCreator
{
    public class ServerConnector
    {
        private ServerXMLParser serverXmlParser;
        private WorkItemStore SetupServer()
        {
            serverXmlParser = new ServerXMLParser();
            string tfsServer = serverXmlParser.GetTfsServer();
            var tfs = new TfsTeamProjectCollection(TfsTeamProjectCollection.GetFullyQualifiedUriForName(tfsServer));
            return tfs.GetService<WorkItemStore>();
        }

        private WorkItemStore Connect()
        {
            var workItemStore = this.SetupServer();
            return workItemStore;
        }

        public WorkItem GetWorkItemById(int id)
        {
            var store = this.SetupServer();
            return store.GetWorkItem(id);
        }
    }
}
