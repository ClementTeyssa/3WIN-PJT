using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace SupReader.Classes
{
    public class Rss
    {
        public Uri Url { get; set; }
        public Source Source { get; set; }
        public List<Article> Articles { get; set; }
        public bool Completed { get; private set; } = false;
        public async void Fetch()
        {
            try
            {
                XmlDocument doc = await XmlDocument.LoadFromUriAsync(Url);

                Source = new Source();
                Source.Titre = doc.SelectSingleNode("//rss/channel/title").InnerText;
                Source.Lien = doc.SelectSingleNode("//rss/channel/link").InnerText;
                Source.Description = doc.SelectSingleNode("//rss/channel/description").InnerText;



                Articles = new List<Article>();
                foreach(var i in doc.SelectNodes("//rss/channel/item")){
                    Article Article = new Article();
                    Article.Titre = i.SelectSingleNode("title").InnerText;
                    Article.Lien = i.SelectSingleNode("link").InnerText;
                    Article.Description = i.SelectSingleNode("description").InnerText;
                    Article.pubDate = DateTime.Parse(i.SelectSingleNode("pubDate").InnerText);
                }
                Completed = true;
            }
            catch (Exception e)
            {
                Completed = true;
            }
        }
    }

    public class RssRead
    {

        public async void LancerSources(List<Source> Sources)
        {
            
            //await 
        }
    }
}
