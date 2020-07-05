using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.TestRunner.MSTest
{
    class TestRunner : ITestRunner
    {

        public int Execute(string testAssembly, DirectoryInfo outPutDirectory) {
            throw new NotImplementedException();
        }

        public List<string> FindTestAssemblies(DirectoryInfo searchDirectory, string TestSuite = null) {
            throw new NotImplementedException();
        }

    }
}
