using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewExample.models
{
    class Post
    {
        public int id { get; set; }
        public String subject { get; set; }
        public int userId { get; set; }
        public DateTime posted { get; set; }
        public DateTime updated { get; set; }
    }
}
