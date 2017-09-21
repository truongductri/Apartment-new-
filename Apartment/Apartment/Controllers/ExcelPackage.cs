using System.IO;

namespace Apartment.Controllers
{
    internal class ExcelPackage
    {
        private FileInfo file;

        public ExcelPackage(FileInfo file)
        {
            this.file = file;
        }

        public object Workbook { get; internal set; }
    }
}