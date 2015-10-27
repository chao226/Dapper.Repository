using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessModels
{
    class SearchField : Attribute
    {
        public SearchField(string field)
        {
            this.Field = field;
        }
        public string Field { get; set; }
    }
}
