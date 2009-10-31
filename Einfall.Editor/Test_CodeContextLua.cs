using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Einfall.Editor.Lua;
using NUnit.Framework;

namespace Einfall.Editor
{
    [TestFixture]
    public class Test_CodeContextLua
    {
        CodeContextLua _context = new CodeContextLua();

        [Test]
        public void Test_FindNoFunctions()
        {
            _context.ClearFunctionList();

            string test1 = "";

            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 0);
        }

        [Test]
        public void Test_FindSingleFunction()
        {
            _context.ClearFunctionList();

            string test1 = "function Test() end";

            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 1);
        }

        [Test]
        public void Test_FindFunctionOnFirstLine()
        {
            _context.ClearFunctionList();

            string test1 = "function Test() end";

            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList[0].StartLine == 0);
        }

        [Test]
        public void Test_FunctionOnSecondLine()
        {
                      _context.ClearFunctionList();

            string test1 = "--this is a test method\r\nfunction Test() end";

            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList[0].StartLine == 1);
        }

        [Test]
        public void Test_DontFindMalformedFunction_NoClosingBrace()
        {
            _context.ClearFunctionList();
            string test1 = "function UnFinished(";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 0);
        }

        [Test]
        public void Test_DontFindMalformedFunction_WrongClosingBrace()
        {
            _context.ClearFunctionList();
            string test1 = "function Broke(()";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 0);
        }

        [Test]
        public void Test_FindCorrectlyAnonymousFormattedFunction()
        {
            _context.ClearFunctionList();
            string test1 = "=function()";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 1);
        }

        [Test]
        public void Test_FindCorrectlyNamedFormattedFunction()
        {
            _context.ClearFunctionList();
            string test1 = "function TestFunction()";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 1);
        }

        [Test]
        public void Test_DontFindIncorrectlySpacedFunction()
        {
            _context.ClearFunctionList();
            string test1 = "functionTestFunction()";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList.Count == 0);
        }

        [Test]
        public void Test_FindNamedFunction()
        {
            _context.ClearFunctionList();
            string test1 = "function TestFunction()";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList[0].Name == "TestFunction");
        }

        [Test]
        public void Test_FindNamedFunctionWithArgs()
        {
            _context.ClearFunctionList();
            string test1 = "function TestFunction(one, two, three)";
            _context.UpdateFunctionList(test1);
            Assert.IsTrue(_context.FunctionList[0].Name == "TestFunction");
        }

        [Test]
        public void Test_FindNamedTabledFunction()
        {
            _context.ClearFunctionList();
            string test1 = "Cat = {} function Cat:TestFunction(one, two, three)";
            _context.UpdateFunctionList(test1);

            //
            // WARNING, this is probably a temporary, in the future, 
            // table functions maybe grouped in some unspecified way.
            // a tree?
            //
            Assert.IsTrue(_context.FunctionList[0].Name == "Cat:TestFunction");
        }

 
        [Test]
        public void Test_FindFunction_WorkExample()
        {
            _context.ClearFunctionList();
            string test1 = @"function Main:OnNPCLoadedButNotVisited()
                                self:DoRepeatNPCNotification()
                                self.npc:ShowSignal()
                            end";
            _context.UpdateFunctionList(test1);

            //
            // WARNING, this is probably a temporary, in the future, 
            // table functions maybe grouped in some unspecified way.
            // a tree?
            //
            Assert.IsTrue(_context.FunctionList[0].Name == "Main:OnNPCLoadedButNotVisited");
        }

        [Test]
        public void Test_FindFunction_VarArgs()
        {
            _context.ClearFunctionList();
            string test1 = @"        function printf(...)
                                        io.write(string.format(...))
                                    end";
            _context.UpdateFunctionList(test1);

            Assert.IsTrue(_context.FunctionList[0].Name == "printf");
        }

        [Test]
        public void Test_FindFunction_MemberFunction()
        {
            _context.ClearFunctionList();
            string test1 = @"        function a.printf(...)
                                        io.write(string.format(...))
                                    end";
            _context.UpdateFunctionList(test1);

            Assert.IsTrue(_context.FunctionList[0].Name == "a.printf");
        }

    }
}
