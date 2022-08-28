using CalcWpfApp.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.Composition;
using System.Windows;


namespace Plug
{
    /// <summary>
    /// Логика взаимодействия для UserControl.xaml
    /// </summary>
    [Export(typeof(ICalcExtension))]
    public partial class UserControlUI : UserControl, ICalcExtension
    { 
        public UserControlUI()
        {
            InitializeComponent();
        }
        public FrameworkElement GetUI()
        {
            return this;
        }
        #region Реализация интерфейса(Информация о плагине)
        public Version Version
        {
            get { return new Version(6, 0, 0, 0); }
        }
        public string Description
        {
            get { return "Плагин"; }
        }
        public string Name => "Программист";
        #endregion
    }
}
