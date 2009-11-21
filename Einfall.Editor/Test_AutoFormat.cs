using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Einfall.Editor.Lua;

namespace Einfall.Editor
{
    [TestFixture]
    public class Test_AutoFormat
    {
        [Test]
        public void Test_FunctionDef_EmptyFile()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function()";
            int testPosition = 10;

            Assert.True(af.ShouldAutoFinishFunctionDef(testString, testPosition));

        }

        [Test]
        public void Test_FunctionDef_DirectlyBeforeFunction()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function()function()";
            int testPosition = 10;

            Assert.True(af.ShouldAutoFinishFunctionDef(testString, testPosition));

        }

        [Test]
        public void Test_FunctionDef_DontCloseExistingFunction()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function()
                                    return 'a'
                                  end";
            int testPosition = 10;
            Assert.False(af.ShouldAutoFinishFunctionDef(testString, testPosition));
        }

        [Test]
        public void Test_FunctionDef_DontCloseExistingFunction_WithFunctionData()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function()
                                    a = function()
                                    end
                                  end";
            int testPosition = 10;
            Assert.False(af.ShouldAutoFinishFunctionDef(testString, testPosition));
        }

        [Test]
        public void Test_FunctionDef_DontCloseExistingFunction_WithOwnFunctionData()
        {
            AutoFormat af = new AutoFormat();

            
            string testString = @"function()
                                    g_GlobalThing = function()
                                    end
                                  ";
            int testPosition = 10;
            Assert.True(af.ShouldAutoFinishFunctionDef(testString, testPosition));
        }

        [Test]
        public void Test_PositionIsJustBeforeFunction()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function()";
            int testPosition = 9;
            Assert.True(af.IsPosJustAfterFunction(testString, testPosition));
        }

        [Test]
        public void Test_PositionIsJustBeforeFunction_WithSingleSpace()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function( )";
            int testPosition = 10;
            Assert.True(af.IsPosJustAfterFunction(testString, testPosition));
        }

        [Test]
        public void Test_PositionIsJustBeforeFunction_Malformed()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"function(function() )";
            int testPosition = testString.Length-1;
            Assert.False(af.IsPosJustAfterFunction(testString, testPosition));
        }

        [Test]
        public void Test_PositionIsJustBeforeFunction_PreSpacing()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"           function()";
            int testPosition = testString.Length - 1;
            Assert.True(af.IsPosJustAfterFunction(testString, testPosition));
        }

        [Test]
        public void Test_PositionIsJustBeforeFunction_RealworldFail()
        {     
            AutoFormat af = new AutoFormat();
            string testString = @"           function AnimationPlayer:Update(dt)";
            int testPosition = testString.Length - 1;
            Assert.True(af.IsPosJustAfterFunction(testString, testPosition));
        }

        [Test]
        public void Test_IsPosJustAfterWord_ThenCorrect()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"then";
            int testPosition = testString.Length - 1;
            Assert.True(af.IsPosJustAfterWord(testString, "then", testPosition));
        }


        [Test]
        public void Test_IsPosJustAfterWord_EndDiffer()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"hen";
            int testPosition = testString.Length - 1;
            Assert.False(af.IsPosJustAfterWord(testString, "then", testPosition));
        }


        [Test]
        public void Test_IsPosJustAfterWord_FullString()
        {
            AutoFormat af = new AutoFormat();
            string testString = @"if x > 100 then";
            int testPosition = testString.Length - 1;
            Assert.True(af.IsPosJustAfterWord(testString, "then", testPosition));
        }

        [Test]
        public void Test_DoesLineHaveForStatement()
        {
            AutoFormat af = new AutoFormat();
            Assert.True(af.DoesLineHaveForStatement("for"));
        }

        [Test]
        public void Test_DoesLineHaveForStatement_InString()
        {
            AutoFormat af = new AutoFormat();
            Assert.False(af.DoesLineHaveForStatement("\"for\""));
        }
    }
}
