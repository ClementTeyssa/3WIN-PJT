using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupReader.Classes
{
    public class Source
    {
        public Boolean Active { get; set; } = true;
        public String Titre { get; set; }
        public String Lien { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Source))
                return false;
            return (obj as Source).Lien == this.Lien;
        }
    }

    public class Article
    {
        public String Titre { get; set; }
        public String Lien { get; set; }
        public String Description { get; set; }
        public DateTime pubDate { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Article))
                return false;
            return (obj as Article).Lien == this.Lien;
        }
    }
}
