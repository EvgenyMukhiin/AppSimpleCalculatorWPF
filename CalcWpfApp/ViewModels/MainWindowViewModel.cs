using CalcWpfApp.Contract;
using CalcWpfApp.Infrastructure.Commands.Base;
using CalcWpfApp.Services;
using CalcWpfApp.ViewModels.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcWpfApp.ViewModels
{
    [Export(typeof(IOperation))]
    public partial class MainWindowViewModel : ViewModel, IOperation
    {
        #region Заголовок окна
        private string _Title = $"Калькулятор v{Assembly.GetExecutingAssembly().GetName().Version.ToString(2)}";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion
        #region Команды
        #region Закрытие приложения
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion
        #region Свернуть приложение
        public ICommand MinimizedApplicationCommand { get; }
        private bool CanMinimizedApplicationCommandExecute(object p) => true;
        private void OnMinimizedApplicationCommandExecuted(object p)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
        #endregion
        public ICommand ClickButtonCommand { get; }
        private bool CanClickButtonCommandExecute(object p) => true;
        private void OnClickButtonCommandExecuted(object p)
        {
            Operation(p.ToString());
        }
        #endregion
        #region Результат
        private string _Result;
        public String Result
        {
            get => _Result;
            set => Set(ref _Result, value);
        }
        #endregion
        #region Результат в памяти
        private ObservableCollection<string> _ResultMemory;
        public ObservableCollection<string> ResultMemory
        {
            get => _ResultMemory;
            set => Set(ref _ResultMemory, value);
        }
        #endregion
        #region Название плагина
        private string _NamePlugin;
        public string NamePlugin
        {
            get => _NamePlugin;
            set => Set(ref _NamePlugin, value);
        }
        #endregion
        private string operation = "";
        private bool ResultOperation = false;
        private StringBuilder op1 = new StringBuilder();
        private StringBuilder op2 = new StringBuilder();
        private readonly ILogger _logger;
        #region для плагина
        DirectoryCatalog dirCatalog;
        ExtensionImport import;
        CompositionContainer container;
        private Grid _element;
        public Grid Element
        {
            get => _element;
            set => Set(ref _element, value);
        }
        #endregion
        #region Видимость вкладки плагина
        private string _tab = "Hidden";
        public string tab
        {
            get => _tab;
            set => Set(ref _tab, value);
        }
        #endregion

        public MainWindowViewModel(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("Действия пользователя");
            ResultMemory = new ObservableCollection<string>() { };
            Element = new Grid { };
            InitializeContainer();
            RefreshExtensions();
            #region Команды
            ClickButtonCommand = new ActionCommand(OnClickButtonCommandExecuted, CanClickButtonCommandExecute);
            CloseApplicationCommand = new ActionCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            MinimizedApplicationCommand = new ActionCommand(OnMinimizedApplicationCommandExecuted, CanMinimizedApplicationCommandExecute);
            #endregion
        }
        public void Operation(string input)
        {
            if (input != "c")
            {
                Result += input;
                if (ResultOperation)
                {
                    op1.Clear();
                    op2.Clear();
                    operation = "";
                    ResultOperation = false;
                }
                switch (input)
                {
                    case "+":
                        operation = input;
                        _logger.LogInformation(LogId.InfoCode, $"Пользователь выбрал действие {operation}");
                        break;
                    case "-":
                        operation = input;
                        _logger.LogInformation(LogId.InfoCode, $"Пользователь выбрал действие {operation}");
                        break;
                    case "*":
                        operation = input;
                        _logger.LogInformation(LogId.InfoCode, $"Пользователь выбрал действие {operation}");
                        break;
                    case "/":
                        operation = input;
                        _logger.LogInformation(LogId.InfoCode, $"Пользователь выбрал действие {operation}");
                        break;
                    case "=":
                        Result += Sum(op1.ToString(), op2.ToString(), operation);
                        ResultMemory.Insert(0, Result.ToString());
                        Result = null;
                        break;
                    default:
                        if (operation == "") //первый операнд
                        {
                            var res = Result;
                            op1.Append(input);
                            _logger.LogInformation(LogId.InfoCode, $"Пользователь ввел первый операнд {op1}");
                            return;
                        }
                        if (operation != "") //второй операнд
                        {
                            var res = Result;
                            op2.Clear();
                            op2.Append(res.Substring(res.IndexOf(operation) + 1)); //удаляем из строки второго операнда оператор
                            _logger.LogInformation(LogId.InfoCode, $"Пользователь ввел второй операнд {op2}");
                        }
                        break;
                }
                if (operation != "" & op1.ToString()=="")
                {
                    Result = "";
                    operation = "";
                    _logger.LogInformation(LogId.InfoCode, $"Пользователь пытался ввести действие до ввода значений");
                }
            }
            else
            {
                Result = "";
                operation = "";
                op1.Clear();
                op2.Clear();
                _logger.LogInformation(LogId.InfoCode, $"Очистка поля ввода");
            }
        }
        private string Sum(string op1, string op2, string operation)
        {
            string c = "0";
            try
            {
                double a = Convert.ToDouble(op1);
                double b = Convert.ToDouble(op2);

                c = operation switch
                {
                    "+" => (a + b).ToString(),
                    "-" => (a - b).ToString(),
                    "*" => (a * b).ToString(),
                    "/" => (a / b).ToString(),
                };
                ResultOperation = true;
                _logger.LogInformation(LogId.InfoCode, $"Результат {c}");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LogId.ErrorCode,$"Ошибка вывода:{ex}");
                Result = null;
            }
            return c;
        }
        private void RefreshExtensions()
        {
            dirCatalog.Refresh();
            foreach (var extension in import.extensionsCollection)
            {
                try
                {
                    NamePlugin = extension.Value.Name;
                    Element.Children.Add(extension.Value.GetUI());
                    tab = "Visible";
                    _logger.LogInformation("плагин загружен");
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void InitializeContainer()
        {
            dirCatalog = new DirectoryCatalog(App.CurrentDirectory);//Settings1.Default.AddInDirectory);App.CurrentDirectory
            container = new CompositionContainer(dirCatalog);
            import = new ExtensionImport();
            import.ImportsSatisfied += (sender, e) =>
            {
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {

                }
                else
                {
                    MessageBox.Show(e.Status);
                }
            };
            container.ComposeParts(this, import);
        }
        public IList<IOperation> GetOperations()
        {
            throw new NotImplementedException();
        }
        public void Operations(DependencyProperty property, object value)
        {
            throw new NotImplementedException();
        }
        class LogId
        {
            public const int InfoCode = 1000;
            public const int ErrorCode = 2000;
        }
    }
}
