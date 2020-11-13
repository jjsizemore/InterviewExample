using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Unnecessary usings
// Convention for Fields & Namepaces is PascalCase

namespace InterviewExample.models
{
    class Post
    {
        public int id { get; set; }
        public String subject { get; set; }
        public int userId { get; set; }

        // These field names could be more descriptive i.e. PostedDate & UpdatedDate
        public DateTime posted { get; set; }
        public DateTime updated { get; set; }
    }
}
