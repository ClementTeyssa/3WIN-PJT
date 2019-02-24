using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPlex.Classes
{
    class Task
    {
        public double id { get; set; }
        public int project_id { get; set; }
        public string content { get; set; }
        public bool completed { get; set; }
        public int order { get; set; }
        public int indent { get; set; }
        public int priority { get; set; }
        public string url { get; set; }
        public int comment_ount { get; set; }
    }
}
