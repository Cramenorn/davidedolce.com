using MyWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWebsite.BusinessLogic.Rss
{
    public class CreateRssFeed
    {
        public string GenerateXml(IEnumerable<Article> articleList)
        {
            StringBuilder sbXml = new StringBuilder("<?xml version='1.0' encoding='UTF-8'?>");

            sbXml.Append("<rss version='2.0' xmlns:atom='http://www.w3.org/2005/Atom' xmlns:dc='http://purl.org/dc/elements/1.1/' xmlns:content='http://purl.org/rss/1.0/modules/content/' xmlns:slash='http://purl.org/rss/1.0/modules/slash/'>")
                .Append("<channel>")
                .Append("<title>Blog</title>")
                .Append("<description>News for website</description>")
                .Append("<pubDate> Sat, 25 Nov 2017 23:00:00 + 0000 </pubDate>")
                .Append("<dc:language>en-gb</dc:language>")
                .Append("<dc:creator>email</dc:creator>")
                .Append("<dc:rights>Copyright 2010</dc:rights>")
                .Append("<atom:link rel = 'self' type = 'application/rss+xml' href = 'https://davidedevtest.azurewebsites.net/Home/Feed'/>");

            foreach (var item in articleList)
            {
                var feeder = "https://davidedevtest.azurewebsites.net/Articles/" + item.FileName;

                sbXml.Append("<item>")
                    .Append("<title>")
                    .Append(item.Title)
                    .Append("</title>")
                    .Append("<pubDate>Sat, 25 Nov 2017 23:00:00 +0000</pubDate>")
                    .Append("<link>")
                    .Append(feeder)
                    .Append("</link>")
                    .Append($"<content:encoded><![CDATA[<post date = '2017-11-26' title = '{item.Title}' sub = unknown>")
                    .Append("]]></content:encoded>")
                    .Append("</item>");
            }

            sbXml.Append("</channel>")
                .Append("</rss>");

            return sbXml.ToString();
        }
    }
}