using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using TfsTaskCreator.Object;

namespace TfsTaskCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WorkflowController wc;

        public MainWindow()
        {
            InitializeComponent();
            this.InitUIComponents();
            this.Init();
        }

        private void Init()
        {
            wc = new WorkflowController();
        }

        private void InitUIComponents()
        {
            this.TextBox.KeyDown += TextBoxOnKeyDown;
            this.Prepare.Click += this.PrepareClick;
            this.Create.Click += this.CreateClick;
            this.Create.IsEnabled = false;
            this.ListBox.SelectionChanged += this.ListBoxSelectionChanged;
            this.ListBox.KeyDown += this.ListBoxKeyDown;
        }

        void ListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                this.ListBox.SelectAll();
            }
            if (e.Key == Key.Delete)
            {
                this.ListBox.SelectedIndex = -1;
            }
        }

        private void ListBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.Create.IsEnabled = this.ListBox.SelectedItems.Count != 0;
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            IList<int> storyIds = this.GetStoryIdsFromSelectedItems(this.ListBox.SelectedItems);
            this.AddTasksToStory(storyIds);
            this.wc.CreateTasks();
            this.ClearAfterCreating();
            MessageBox.Show("Tasks has ben created succesfully", "Succesfully created tasks", MessageBoxButton.OK);
        }

        private void ClearAfterCreating()
        {
            this.TextBox.Clear();
            this.ListBox.Items.Clear();
            this.Accounting.IsChecked = false;
            this.DoD.IsChecked = false;
            this.SST.IsChecked = false;
        }

        private void AddTasksToStory(ICollection<int> storyIds)
        {
            foreach (var storyId in storyIds)
            {
                var story = this.wc.GetStoryById(storyId);

                this.AddAccountingTask(story);
                this.AddDoDTask(story);
                this.AddSstTask(story);
                this.AddUspTask(story);
                this.AddHpqcDevTask(story);
                this.AddHpqcSstTask(story);
                this.AddBuildmasterTask(story);
                this.AddStabilizeSSTTask(story);
            }
        }

        private void AddStabilizeSSTTask(Story story)
        {
            var isChecked = this.StabilizeSST.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddStabilizationSstTaskToStory();
            }
        }

        private void AddBuildmasterTask(Story story)
        {
            var isChecked = this.Buildmaster.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddBuildmasterTaskToStory();
            }
        }

        private void AddHpqcSstTask(Story story)
        {
            var isChecked = this.HPQCSst.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddHPQCSstTaskToStory();
            }
        }

        private void AddHpqcDevTask(Story story)
        {
            var isChecked = this.HPQCDev.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddHPQCDevTaskToStory();
            }
        }

        private void AddAccountingTask(Story story)
        {
            var isChecked = this.Accounting.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddAccountingTaskToStory();
            }
        }

        private void AddUspTask(Story story)
        {
            var isChecked = this.USP.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddUspTaskToStory();
            }
        }

        private void AddDoDTask(Story story)
        {
            var isChecked = this.DoD.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddDoDTaskToStory();
            }
        }

        private void AddSstTask(Story story)
        {
            var isChecked = this.SST.IsChecked;
            if (isChecked != null && (bool)isChecked)
            {
                story.AddSstTaskToStory();
            }
        }

        private IList<int> GetStoryIdsFromSelectedItems(IList selectedItems)
        {
            List<int> list = new List<int>();
            foreach (object item in selectedItems)
            {
                string stringId = item.ToString().Split(':')[0];
                list.Add(int.Parse(stringId));
            }
            return list;
        }

        private void PrepareClick(object sender, RoutedEventArgs e)
        {
            this.PrepareStories();
        }

        private void PrepareStories()
        {
            this.ListBox.Items.Clear();
            this.wc.PrepareStories(this.TextBox.Text);
            this.DisplayStoriesOnListBox(this.wc.GetAllStories());
            this.TextBox.Clear();
        }

        private void DisplayStoriesOnListBox(IEnumerable<Story> getAllStories)
        {
            foreach (var story in getAllStories)
            {
                this.ListBox.Items.Add(story.ToString());
            }
        }

        private void TextBoxOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter)
            {
                this.PrepareStories();
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(keyEventArgs.Key.ToString(), "\\d+"))
            {
                keyEventArgs.Handled = true;
            }
        }
    }
}