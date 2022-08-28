using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcWpfApp.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowView => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
