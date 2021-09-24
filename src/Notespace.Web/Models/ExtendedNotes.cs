using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notespace.Web.Models
{
    public abstract class ExtendedNotes : Note
    {
        public bool Collaborative { get; set; }
        
        private int pageCount;

        public abstract string GetHTML();

        public void IncreasePageCount() => pageCount++;
        public void DecreasePageCount() => pageCount--;
        public int GetPageCount() => pageCount;
    }
}
