using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notespace.Web
{
    public class NoUserException : Exception
    {
        public NoUserException() : base("No User Associated With Object")
        { }
    }
    public class NoNoteException : Exception
    {
        public NoNoteException() : base("No Note Associated With Object")
        { }
    }
    public class NoNotebookException : Exception
    {
        public NoNotebookException() : base("No Notebook Associated With Object")
        { }
    }
}
