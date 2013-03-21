using System.Collections.Generic;

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
            try
            {
                return this.store.GetWorkItem(id);
            }
            catch (DeniedOrNotExistException)
            {
                return null;
            }
        }

        public WorkItem CreateNewTask(int storyId, string taskName)
        {
            var workItem = this.project.WorkItemTypes["TASK"].NewWorkItem();
            var userStory = this.GetWorkItemById(storyId);

            var taskTitle = this.CreateValidString(taskName);

            if (userStory != null)
            {
                workItem.Title = taskTitle;
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
            }

            return workItem;
        }

        public string CreateValidString(string taskName)
        {
            const string ReplaceWithNone = "";
            var taskTitle = taskName.Replace("\r\n", ReplaceWithNone).Replace("\n", ReplaceWithNone).Replace("\r", ReplaceWithNone).Replace("\"", ReplaceWithNone);
            var titleCount = taskTitle.Length;
            if (titleCount > 75)
            {
                return taskTitle.Substring(0, 75);
            }
            return taskTitle;
        }

        public void SetTaskToRemoved(WorkItem workItem)
        {
            workItem.State = "Removed";
            workItem.Save();
        }
    }
}