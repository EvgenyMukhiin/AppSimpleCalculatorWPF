using System.Collections.Generic;
using System.Windows;

namespace CalcWpfApp.Contract
{
    public interface IOperation
    {
        IList<IOperation> GetOperations();
        void Operations(DependencyProperty property, object value);
    }
}
