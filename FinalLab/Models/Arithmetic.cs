using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalLab.Models
{
    static class Arithmetic
    {
        //метод для вычисления 
        public static string AddResult(string number1, string number2, string operation)
        {
            try
            {
                double num1 = Convert.ToDouble(number1);
                double num2 = Convert.ToDouble(number2);
                switch (operation)
                {
                    case "/":
                        {
                            if (number2 == "0")
                            { return "деление на ноль невозможно"; }

                            else
                            {
                                return (num1 / num2).ToString();
                            }
                        }
                    case "×":
                        {
                            return (num1 * num2).ToString();
                        }
                    case "-":
                        {
                            return (num1 - num2).ToString();
                        }
                    case "+":
                        {
                            return (num1 + num2).ToString();
                        }
                    default:
                        {
                            return "0";
                        }
                }
            }
            catch (DivideByZeroException)
            {
                return "деление на ноль невозможно";
            }
            catch (Exception)
            {
                return "Недопустимый ввод";
            }
        }


        //создаем метод для ввода значений
        public static string AddNumber(string number1, string number2, string operation, string number)
        {
            if (operation == null)
            {// Оператор пуст, переданное значение сохраняем в первое значение
                return Number(number1, number);
            }
            else
            {// Оператор не пуст, сохраненный номер является вторым
                return Number(number2, number);
            }
        }
        private static string Number(string numberI, string number)
        {
            if (numberI == null || numberI == "0")
            {
                if (number == ",")
                {
                    numberI = "0,";
                }
                else
                {
                    numberI = number;
                }
            }
            else
            {
                if (numberI.Length < 14)
                {
                    numberI += number;
                }
            }
            return numberI;
        }



        //создаем метод для удаления последнего значения
        public static string AddDeleteLastNumber(string number1, string number2, string operation)
        {
            if (operation == null)
            {// Оператор пуст, удаляем символ у первого значение
                return DeleteLastNumber(number1);
            }
            else
            {// Оператор не пуст, удаляем символ у второго значение
                return DeleteLastNumber(number2);
            }
        }
        private static string DeleteLastNumber(string numberI)
        {
            if (numberI.Length > 1)
            {
                numberI = numberI.Substring(0, numberI.Length - 1);
            }
            else
            {
                numberI = "0";
            }
            return numberI;
        }


        //создаем метод для изменения знака значения
        public static string AddPlusMinus(string number1, string number2, string operation)
        {
            if (operation == null)
            {// Оператор пуст, меняем знак у первого числа
                return PlusMinus(number1);
            }
            else
            {// Оператор не пуст, меняем знак у второго числа
                return PlusMinus(number2);
            }
        }
        private static string PlusMinus(string numberI)
        {
            if (numberI.Contains("-") == true)
            {
                numberI = numberI.Substring(1, numberI.Length - 1);
            }
            else
            {
                numberI = "-" + numberI;
            }
            return numberI;
        }
    }
}
