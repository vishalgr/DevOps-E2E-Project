using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.TestRunner
{
    interface ITestRunner {
        int Execute(string testAssembly, DirectoryInfo outPutDirectory);

        List<string> FindTestAssemblies(DirectoryInfo searchDirectory, string TestSuite = null);
    }
}
