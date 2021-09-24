using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notespace.Web.Models
{
    public class ExtendedNotebooks : ExtendedNotes
    {
        public override string GetHTML()
        {
            string updated = "";
            int offset = 10;
            foreach (char c in this.HTML)
            {
                updated += c + offset;
            }
            return updated;
        }
    }
}
