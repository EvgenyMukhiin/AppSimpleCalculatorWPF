using CalcWpfApp.Contract;
using Plug.Infrastructure.Commands.Base;
using Plug.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace Plug
{
    public class UserControlUIViewModel : ViewModel, IOperation
    {
        #region Заголовок окна
        private string _Titles = "Расширение Программист";
        public string Titles
        {
            get => _Titles;
            set => Set(ref _Titles, value);
        }
        #endregion
        #region Поля контрола
        private string _ResultPlug;
        public string ResultPlug
        {
            get => _ResultPlug;
            set => Set(ref _ResultPlug, value);
        }

        private string _TextArea;
        public string TextArea
        {
            get => _TextArea;
            set => Set(ref _TextArea, value);
        }

        private string _Hex;
        public string Hex
        {
            get => _Hex;
            set => Set(ref _Hex, value);
        }

        private int _Dec;
        public int Dec
        {
            get => _Dec;
            set => Set(ref _Dec, value);
        }

        private string _Bin;
        public string Bin
        {
            get => _Bin;
            set => Set(ref _Bin, value);
        }

        #endregion
        #region Команды
        public ICommand ClickButtonCommand { get; }
        private bool CanClickButtonCommandExecute(object p) => true;
        private void OnClickButtonCommandExecuted(object p)
        {
            Operation(p.ToString());
        }
        #endregion
        [Import(typeof(IOperation), AllowRecomposition = true)]
        IOperation operationDel;
        public UserControlUIViewModel()
        {
            ClickButtonCommand = new ActionCommand(OnClickButtonCommandExecuted, CanClickButtonCommandExecute);
        }
        public UserControlUIViewModel(IOperation operation)
        {
            this.operationDel = operation;
        }
        public void Operation(string input)
        {
            try
            {
                switch (input)
                {
                    case "<<":
                        Result((int.Parse(TextArea) << 1).ToString());
                        break;
                    case ">>":
                        Result((int.Parse(TextArea) >> 1).ToString());
                        break;
                    case "Сброс":
                        Result("");
                        break;
                    default:
                        if (TextArea == "0")
                            TextArea = "";
                        TextArea += input;
                        Result(TextArea);
                        break;
                }
            }
            catch (Exception)
            {
                TextArea = ":(";
            }
        }
        public void Result(string text)
        {
            if (text != "")
            {
                TextArea = text;
                Hex = Int32.Parse(text).ToString("X4");
                Dec = Int32.Parse(text);
                Bin = Convert.ToString(Dec, 2);
            }
            else
            {
                TextArea = "";
                Hex = "";
                Dec = 0;
                Bin = "";
            }
        }
        public IList<IOperation> GetOperations()
        {
            throw new System.NotImplementedException();
        }
        public void Operations(DependencyProperty property, object value)
        {
            throw new NotImplementedException();
        }
    }
}
