using System;
using System.Windows;

namespace CalcWpfApp.Contract
{
    public interface ICalcExtension
    {
        string Name { get; }
        Version Version { get; }
        string Description { get; }
        FrameworkElement GetUI();
    }
}
