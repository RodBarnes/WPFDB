using Common;
using Common.ViewModels;
using System;
using System.Reflection;

namespace WPF.ViewModel
{
    public class MainVM : BaseVM
    {
        private static MainWindow window;

        public MainVM(MainWindow wdw)
        {
            try
            {
                window = wdw;

                InitAboutPanel();
                InitMainPanel();
            }
            catch (Exception ex)
            {
                currentMessageAction = MessageAction.Acknowledge;
                window.MessagePanel.Show("EXCEPTION!", Utility.ParseException(ex));
            }
        }

        #region Commands

        public Command SaveCommand { get; set; }
        private void SaveAction(object obj) => Save();
        public Command LoadCommand { get; set; }
        private void LoadAction(object obj) => Load();

        #endregion

        #region MessagePanel

        public enum MessageAction
        {
            Acknowledge,
            ClearDatabase
        }

        private MessageAction currentMessageAction;

        private string mainMessageResponse;
        public string MainMessageResponse
        {
            get => mainMessageResponse;
            set
            {
                mainMessageResponse = value;
                NotifyPropertyChanged();
                if (mainMessageResponse == "Proceed")
                {
                    switch (currentMessageAction)
                    {
                        case MessageAction.Acknowledge:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        #region Properties
        public AboutProperties AboutProperties { get; set; }

        #endregion

        #region Methods

        private void InitAboutPanel()
        {
            var assyInfo = new AssemblyInfo(Assembly.GetExecutingAssembly());
            AboutProperties = new AboutProperties
            {
                ApplicationName = assyInfo.Product,
                ApplicationVersion = assyInfo.AssemblyVersion.ToString(),
                Background = window.Background.ToString(),
                ImagePath = "/WPF;component/Images/searchicon-400x400.png",
                Description = "This decribes the purpose of the application.\n" +
                $"Copyright © 2021 Advanced Applications. Apace 2.0 Licensed."
            };
        }

        private void InitMainPanel()
        {
            // Initialize commands
            LoadCommand = new Command(LoadAction);
            SaveCommand = new Command(SaveAction);
        }

        private void Save()
        {
            currentMessageAction = MessageAction.Acknowledge;
            window.MessagePanel.Show("Save!", "You pressed the Save button");
        }

        private void Load()
        {
            currentMessageAction = MessageAction.Acknowledge;
            window.MessagePanel.Show("Load!", "You pressed the Load button");
        }

        #endregion
    }
}
