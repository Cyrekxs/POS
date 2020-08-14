using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Library.Helper
{
    class Connection
    {
        public static string SQLConnection { get { return @"Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True"; } }

    }
}
