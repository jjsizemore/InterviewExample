using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Unnecessary usings
// Convention for Fields & Namepaces is PascalCase

namespace InterviewExample.models
{
    class User
    {
        public int id { get; set; }
        public String userName { get; set; }
        public String email { get; set; }
        
        // Field name could be more descriptive i.e. CreatedDate
        public DateTime created { get; set; }
    }
}
