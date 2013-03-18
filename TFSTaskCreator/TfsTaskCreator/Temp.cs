namespace TfsTaskCreator
{
    using System;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    class Temp
    {
        private const string TfsServer = "";
        private const string TeamProject = "";

        static void Intro(string[] args)
        {
            Temp p = new Temp();
            p.CreateTask();
            Console.ReadLine();
        }

        public void CreateTask()
        {
            Console.WriteLine("Creating Task");
            //Connect to TFS Project Collection, provide server URL in format http:// ServerName:Port/Collection
            var tfs = new TfsTeamProjectCollection(TfsTeamProjectCollection.GetFullyQualifiedUriForName(TfsServer));

            Console.WriteLine("GetService");
            //Get data store that contains all workitems on a particular server
            var store = tfs.GetService<WorkItemStore>();

            //Get particular Team Project
            Project project = store.Projects[TeamProject];
            Console.WriteLine("Connected to URL: {0} and project: {1} ", tfs.Uri, project.Name);

            //Get reference of existing workitem from ID, here user story id is passed new task will be created as child of this user story
            WorkItem userStory = store.GetWorkItem(232791);
            Console.WriteLine(" Parent UserStory is: " + userStory.Title);

            WorkItem task = null;
            var workItemTypes = project.WorkItemTypes;
            if (workItemTypes.Contains("TASK"))
            {
                //Create new workitem of type Task in the project
                task = project.WorkItemTypes["TASK"].NewWorkItem();
            }
            else
            {
                Console.WriteLine("No Type TASK Found");
            }

            Console.WriteLine("Creating Task...");
            if (task != null)
            {
                //Set properties of task like title, iteration, activity, assigned to
                task.Title = "DamirTest";
                task.Description = "";
                task.AreaPath = userStory.AreaPath;
                task.IterationPath = userStory.IterationPath;
                task.Fields["Activity"].Value = "";
                task.Fields["Original Estimate"].Value = 0;
                task.Fields["Remaining Work"].Value = 0;
                task.Fields["Assigned To"].Value = "";

                //Create a hierarchy type (Parent-Child) relationship 
                WorkItemLinkType hierarchyLinkType = store.WorkItemLinkTypes[CoreLinkTypeReferenceNames.Hierarchy];

                //Set user story as parent of new task
                task.Links.Add(new WorkItemLink(hierarchyLinkType.ReverseEnd, userStory.Id));

                //Save the task
                task.Save();

                Console.WriteLine("Successfully created Task# " + task.Id);
            }
        }

        //   Summary of steps -
        //1. Connect to TFS server.
        //2. Get WorkitemStore
        //3. Get project from store.
        //4. Optional, Get existing workitem from id, if any type of link is required.
        //5. Create a new workitem in a project providing type of workitem. Possible type include Task, Bug, Test Case, User Story, Shared Step, etc.
        //6. Set properties of new workitem.
        //7. Save workitem.
    }
}