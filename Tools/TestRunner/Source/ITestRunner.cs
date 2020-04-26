using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps
{
    interface ITestRunner {
        int Execute();

        List<string> FindTestAssemblies(string searchDirectory);
    }
}
