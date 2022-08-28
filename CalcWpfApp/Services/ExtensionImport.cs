
using CalcWpfApp.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcWpfApp.Services
{
    class ImportEventArgs : EventArgs
    {
        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public ImportEventArgs(string status)
        {
            this.status = status;
        }
    }
    class ExtensionImport : IPartImportsSatisfiedNotification
    {
        public event EventHandler<ImportEventArgs> ImportsSatisfied;

        [ImportMany(typeof(ICalcExtension),AllowRecomposition = true)]
        //public IEnumerable<ICalcExtension> CalcExtensions { get; set; }
        public IEnumerable<Lazy<ICalcExtension>> extensionsCollection { get; set; }
        public void OnImportsSatisfied()
        {
            if (ImportsSatisfied != null)
            ImportsSatisfied.Invoke(this, new ImportEventArgs ("Плагин импортирован успешно"));
        }
    }
}
