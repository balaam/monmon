using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Einfall.Editor
{
    public class KeyHelper
    {
        static public char KeycodeToChar(Keys keyCode)
        {
                 switch (keyCode)
                 {
                     case Keys.Add:
                         return '+';
                     case Keys.Decimal:
                         return '.';
                     case Keys.Divide:
                         return '/';
                     case Keys.Multiply:
                         return '*';
                     case Keys.OemBackslash:
                         return '\\';
                     case Keys.OemCloseBrackets:
                         return ']';
                     case Keys.OemMinus:
                         return '-';
                     case Keys.OemOpenBrackets:
                         return '[';
                     case Keys.OemPeriod:
                         return '.';
                     case Keys.OemPipe:
                         return '|';
                     case Keys.OemQuestion:
                         return '/';
                     case Keys.OemQuotes:
                         return '\'';
                     case Keys.OemSemicolon:
                         return ';';
                     case Keys.Oemcomma:
                         return ',';
                     case Keys.Oemplus:
                         return '+';
                     case Keys.Oemtilde:
                         return '`';
                     case Keys.Separator:
                         return '-';
                     case Keys.Subtract:
                         return '-';
                     case Keys.D0:
                         return '0';
                     case Keys.D1:
                         return '1';
                     case Keys.D2:
                         return '2';
                     case Keys.D3:
                         return '3';
                     case Keys.D4:
                         return '4';
                     case Keys.D5:
                         return '5';
                     case Keys.D6:
                         return '6';
                     case Keys.D7:
                         return '7';
                     case Keys.D8:
                         return '8';
                     case Keys.D9:
                         return '9';
                     case Keys.Space:
                         return ' ';
                     default:
                         return keyCode.ToString()[0];
                 }
             }
    }
}
