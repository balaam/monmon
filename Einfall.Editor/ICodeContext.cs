using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;

namespace Einfall.Editor
{
    public interface ICodeContext
    {
        /// <summary>
        /// Looks at the character and decides if this character is the start of a comment string.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool IsStartOfComment(char c);

        /// <summary>
        /// Take the currect selection and replace it with commented out code.
        /// </summary>
        /// <param name="_selection"></param>
        void CommentOutSelection(Scintilla scintilla, Selection selection);

        void UpdateFunctionList(Scintilla scintilla);

        List<FunctionMetaData> FunctionList
        {
            get;
        }
    }
}
