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
        Test_AutoFormat _testAutoFormat = new Test_AutoFormat();
        public void Go()
        {
            //_testCodeTransformer.TestGetTextFromWhenIdNotPresent();
            _testAutoFormat.Test_IsPosJustAfterWord_ThenCorrect();
           // _testCodeTransformer.FindSecondLineIndex();
        }

    }
}
