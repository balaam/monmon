using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using Einfall.Editor;


namespace MonMon
{
    class TestRunner
    {
        Test_CodeTransformer _testCodeTransformer =new Test_CodeTransformer();
        Test_CodeContextLua _textCodeContextLua = new Test_CodeContextLua();
        public void Go()
        {
            //_testCodeTransformer.TestGetTextFromWhenIdNotPresent();
            _textCodeContextLua.Test_FindFunction_MemberFunction();
           // _testCodeTransformer.FindSecondLineIndex();
        }

    }
}
