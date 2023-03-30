using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryGudosh.ClassHelper
{
    internal class EFClass
    {
        public static DB.Entities4 db { get; } = new DB.Entities4();
    }
}
