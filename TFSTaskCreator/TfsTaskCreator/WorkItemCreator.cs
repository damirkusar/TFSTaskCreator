using System.Collections.Generic;
using System.Linq;

namespace TfsTaskCreator
{
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    public class WorkItemCreator
    {
        private readonly ServerConnector serverConnector;
        private readonly Project project;
        private readonly WorkItemStore store;
        private readonly IList<string> taskTypes;


        public WorkItemCreator()
        {
            this.taskTypes = new List<string>();

            this.serverConnector = new ServerConnector();
            this.project = serverConnector.Project;
            this.store = serverConnector.Store;

            this.FillTaskTypesIntoList();
        }

        private void FillTaskTypesIntoList()
        {
            var workItemTypeCollection = this.project.WorkItemTypes;
            for (int i = 0; i < workItemTypeCollection.Count; i++)
            {
                var name = workItemTypeCollection[i].Name;
                this.taskTypes.Add(name);
            }
        }

        public IEnumerable<string> TaskTypes()
        {
            return this.taskTypes;
        }

        public WorkItem GetWorkItemById(int id)
        {
            return this.store.GetWorkItem(id);
        }

        public WorkItem CreateNewTask(int storyId, string taskName)
        {
            WorkItem workItem = this.project.WorkItemTypes["TASK"].NewWorkItem();
            var userStory = this.GetWorkItemById(storyId);

            workItem.Title = taskName;
            workItem.Description = "";
            workItem.AreaPath = userStory.AreaPath;
            workItem.IterationPath = userStory.IterationPath;
            workItem.Fields["Activity"].Value = "";
            workItem.Fields["Original Estimate"].Value = 0;
            workItem.Fields["Remaining Work"].Value = 0;
            workItem.Fields["Assigned To"].Value = "";

            //Create a hierarchy type (Parent-Child) relationship 
            WorkItemLinkType hierarchyLinkType = serverConnector.Store.WorkItemLinkTypes[CoreLinkTypeReferenceNames.Hierarchy];

            //Set user story as parent of new task
            workItem.Links.Add(new WorkItemLink(hierarchyLinkType.ReverseEnd, userStory.Id));

            //Save the task
            workItem.Save();

            return workItem;
        }

        public void SetTaskToRemoved(WorkItem workItem)
        {
            workItem.State = "Removed";
            workItem.Save();
        }
    }
}