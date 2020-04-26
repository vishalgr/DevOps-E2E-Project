using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Nunit
{
    class TestRunner : ITestRunner
    {
        int ITestRunner.Execute()
        {
            throw new NotImplementedException();
        }

        List<string> ITestRunner.FindTestAssemblies(string searchDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
