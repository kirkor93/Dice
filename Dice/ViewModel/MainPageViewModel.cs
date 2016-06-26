using System;

namespace Dice.ViewModel
{
    internal class MainPageViewModel
    {
        public Command NewGameCommand { get; set; }
        public Command LoadGameCommand { get; set; }
        public Command SaveGameCommand { get; set; }
        public Command CheckDiceStateCommand { get; set; }
        public Command AboutApplicationCommand { get; set; }
        public Command CloseApplicationCommand { get; set; }

        public MainPageViewModel()
        {
            NewGameCommand = new Command(NewGameExecuteMethod);
            LoadGameCommand = new Command(LoadGameExecuteMethod);
            SaveGameCommand = new Command(SaveGameExecuteMethod);
            CheckDiceStateCommand = new Command(CheckDiceStateExecuteMethod);
            AboutApplicationCommand = new Command(AboutApplicationExecuteMethod);
            CloseApplicationCommand = new Command(CloseApplicationExecuteMethod);
        }

        private void NewGameExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }

        private void LoadGameExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }

        private void SaveGameExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }

        private void CheckDiceStateExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }

        private void AboutApplicationExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }

        private void CloseApplicationExecuteMethod(object o)
        {
            throw new NotImplementedException();
        }
    }
}
