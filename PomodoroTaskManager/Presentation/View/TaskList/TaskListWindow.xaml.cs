using PomodoroTaskManager.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PomodoroTaskManager.Presentation.View.TaskList
{
    /// <summary>
    /// TaskListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TaskListWindow : Window
    {
        private List<string> _CategoryComboBoxList = new List<string>();

        public TaskListWindow(TaskListVM taskListVM)
        {
            InitializeComponent();

            InputCategoryComboBox.ItemsSource = _CategoryComboBoxList;
            InputTaskData.DataContext = taskListVM;
            DG_TaskList.DataContext = taskListVM.TaskList;
        }
    }
}
