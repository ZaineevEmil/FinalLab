using FinalLab.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FinalLab.ViewModels
{
    //создаем класс посредик для связи между представлеием калькулятора и логическими действиями, реализующими функционал калькулятора
    class MainWindowViewModel : INotifyPropertyChanged
    {

        //создаем общий метод для определния и связи с представлением свойств переменных, используемых при расчетах
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //создаем переменные, используемые при расчетах
        private string number1; //первое число
        public string Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }

        private string number2; //второе число
        public string Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }

        private string result; //результат вычисления 
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        private string display; //поле для отображения 
        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged();
            }
        }


        private string operation; //тип выполняемого вычисления
        public string Operation
        {
            get => operation;
            set
            {
                operation = value;
                OnPropertyChanged();
            }
        }


        //Определяем конструкторы взаимодействия с MainWindow.xaml
        public MainWindowViewModel()
        {
            AddOperation = new RelayCommand(OnAddOperationExecute, CanAddOperationExecute);
            AddNumber = new RelayCommand(OnAddNumberExecute, CanAddNumberExecute);
            DeleteNumber = new RelayCommand(OnDeleteNumberExecute, CanDeleteNumberExecute);
            DeleteAll = new RelayCommand(OnDeleteAllExecute, CanDeleteAllExecute);
            DeleteLastNumber = new RelayCommand(OnDeleteLastNumberExecute, CanDeleteLastNumberExecute);
            AddPlusMinus = new RelayCommand(OnAddPlusMinusExecute, CanAddPlusMinusExecute);
            AddResult = new RelayCommand(OnAddResultExecute, CanAddResultExecute);
        }

        //создаем метод для назначения типа выполняемого вычисления
        public ICommand AddOperation { get; }
        private void OnAddOperationExecute(object p)
        {
            if (Operation == null)
            {
                if (Result == Display)
                {
                    Operation = p.ToString();
                    Number1 = Result;
                }
                else
                {
                    Operation = p.ToString();
                }
            }
            else
            {
                if (Number2 == null)
                {
                    Operation = p.ToString();
                }
                else
                {
                    Result = Display = Number1 = Arithmetic.AddResult(Number1, Number2, Operation);
                    Operation = p.ToString();
                    Number2 = null;
                }
            }
        }
        private bool CanAddOperationExecute(object p)
        {
            return true;
        }

        //создаем метод для ввода значений
        public ICommand AddNumber { get; }
        private void OnAddNumberExecute(object p)
        {
            //string number = (p as Button).Content.ToString();
            string number = p.ToString();
            if (Operation == null)
            { Display = Number1 = Arithmetic.AddNumber(Number1, Number2, Operation, number); }
            else
            { Display = Number2 = Arithmetic.AddNumber(Number1, Number2, Operation, number); }
        }
        private bool CanAddNumberExecute(object p)
        {
            return true;
        }

        //создаем метод для удаления текущего значений
        public ICommand DeleteNumber { get; }
        private void OnDeleteNumberExecute(object p)
        {
            if (Operation == null)
            { Display = Number1 = "0"; }
            else
            { Display = Number2 = "0"; }
        }
        private bool CanDeleteNumberExecute(object p)
        {
            return true;
        }

        //создаем метод для удаления всех значений
        public ICommand DeleteAll { get; }
        private void OnDeleteAllExecute(object p)
        {
            Operation = null;
            Number1 = Display = "0";
            Number2 = null;
        }
        private bool CanDeleteAllExecute(object p)
        {
            return true;
        }

        //создаем метод для удаления последнего значения
        public ICommand DeleteLastNumber { get; }
        private void OnDeleteLastNumberExecute(object p)
        {
            if (Operation == null)
            { Display = Number1 = Arithmetic.AddDeleteLastNumber(Number1, Number2, Operation); }
            else
            { Display = Number2 = Arithmetic.AddDeleteLastNumber(Number1, Number2, Operation); }
        }
        private bool CanDeleteLastNumberExecute(object p)
        {
            if (Operation == null && Number1 != null)
                return true;
            else
            {
                if (Operation != null && Number2 != null)
                    return true;
                else return false;
            }
        }

        //создаем метод для изменения знака значения
        public ICommand AddPlusMinus { get; }
        private void OnAddPlusMinusExecute(object p)
        {
            if (Operation == null)
            { Display = Number1 = Arithmetic.AddPlusMinus(Number1, Number2, Operation); }
            else
            { Display = Number2 = Arithmetic.AddPlusMinus(Number1, Number2, Operation); }
        }
        private bool CanAddPlusMinusExecute(object p)
        {
            if (Operation == null && Number1 != null)
                return true;
            else
            {
                if (Operation != null && Number2 != null)
                    return true;
                else return false;
            }
        }

        //создаем метод для вычисления результата
        public ICommand AddResult { get; }
        private void OnAddResultExecute(object p)
        {
            Result = Display = Arithmetic.AddResult(Number1, Number2, Operation);
            Operation = null;
            Number1 = null;
            Number2 = null;
        }
        private bool CanAddResultExecute(object p)
        {
            if (Operation != null) return true;
            else return false;
        }
    }
}
