using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _Title = "Зайцев Андрей Сергеевич";

        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            set
            {
                /*if (Equals(_Title, value)) return;
                _Title = value;
                OnPropertyChanged();*/

                Set(ref _Title, value);
            }
        }

        #endregion

        #region Предложения
        private string _FS;
        private string _SS;
        private string _Result;
        private double _V;

        public string FS
        {
            get => _FS;
            set
            {
                Set(ref _FS, value);
            }
        }

        public string SS
        {
            get => _SS;
            set
            {
                Set(ref _SS, value);
            }
        }

        public string Result
        {
            get => _Result;
            set
            {
                Set(ref _Result, value);
            }
        }

        public double V
        {
            get => _V;
            set
            {
                Set(ref _V, value);
            }
        }



        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region BCounterCommand
        public ICommand BCounterCommand { get; }
        private void OnBCounterCommandExecuted(object p)
        {
            int first_counter = 0;
            int first_b_counter = 0;

            for (int i = 0; i<FS.ToString().Length; i++)
            {
                first_counter++;
                if (FS[i].ToString().Equals("б")) first_b_counter++;
            }

            int second_counter = 0;
            int second_b_counter = 0;

            for (int i = 0; i < SS.ToString().Length; i++)
            {
                second_counter++;
                if (SS[i].ToString().Equals("б")) second_b_counter++;
            }

            if ((100 * first_b_counter / first_counter) > (100 * second_b_counter / second_counter))
            {
                V = (100 * first_b_counter / first_counter);
                Result = FS;
            }
            else if ((100 * first_b_counter / first_counter) < (100 * second_b_counter / second_counter))
            {
                V = (100 * second_b_counter / second_counter);
                Result = SS;
            }
            else
            {
                V = 0;
                Result = "";
            }
        }

        private bool CanBCounterCommandExecute(object p) => true;

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            BCounterCommand = new LambdaCommand(OnBCounterCommandExecuted, CanBCounterCommandExecute);

            #endregion
        }
    }
}
