using System.Text.RegularExpressions;
using System.Xml;

namespace Cv9;

class Program
{
    static void Main(string[] args)
    {
        var xdoc = new XmlDocument();

        xdoc.Load("https://www.lupa.cz/rss/clanky/");
        foreach (XmlNode article in xdoc.SelectNodes("//item"))
        {
            Console.WriteLine($"Title: {article.SelectSingleNode("title/text()").Value}");
            Console.WriteLine($"Date: {article.SelectSingleNode("pubDate/text()").Value}");
            Console.WriteLine("--------");
        }
        
        // xdoc.Load("test.xml");
        // foreach (XmlNode node in xdoc.SelectNodes("root/customer/name/text()"))
        // foreach (XmlNode node in xdoc.SelectNodes("//name/text()"))
        // foreach (XmlNode node in xdoc.SelectNodes("//customer[@id=2]/name/text()"))
        // {
        //     Console.WriteLine(node.Value);
        // }

        /*
        var root = xdoc.DocumentElement;
        foreach (XmlNode node in root!.ChildNodes)
        {
            var id = node.Attributes["id"].Value;
            if (id == "2")
            {
                node.ParentNode.RemoveChild(node);
            }
        }
        */

        /*
        var root = xdoc.CreateElement("root");
        xdoc.AppendChild(root);

        for (int i = 0; i < 4; i++)
        {
            var customer = xdoc.CreateElement("customer");
            root.AppendChild(customer);

            // var id = xdoc.CreateElement("id");
            // customer.AppendChild(id);
            // var idtxt = xdoc.CreateTextNode($"ID: {i}");
            // id.AppendChild(idtxt);

            var idattr = xdoc.CreateAttribute("id");
            idattr.Value = $"ID: {i}";
            customer.Attributes.Append(idattr);

            var name = xdoc.CreateElement("name");
            customer.AppendChild(name);
            name.AppendChild(xdoc.CreateTextNode($"NAME: {i}"));
        }

        xdoc.Save("test.xml");
        */

        /*
        var dict = new Dictionary<string, string>
        {
            {"name", "Pepa"},
            {"orderName", "Robertek"},
            {"price", "mocmocmoc"}
        };

        var regex = new Regex(@"\{([a-zA-Z\s]+)\}");
        var input = "Ahoj {name}. Tvá objednávka „{orderName}“ v ceně {price} byla úspěšně uhrazena.";

        var output = regex.Replace(input, m =>
        {
            var name = m.Groups[1].Value;
            return dict[name];
        });

        Console.WriteLine(output);

        */
        /*
        var regex = new Regex(@"^(https?):\/\/(?:([a-z]+)\.)?([a-z]+\.[a-z]{2,6})", RegexOptions.IgnoreCase);

        var input = """
                    https://katedrainformatiky.cz/cs/pro-uchazece/zamereni-studia
                    http://katedrainformatiky.cz/
                    https://katedrainformatiky.cz?page=5
                    https://page.katedrainformatiky.cz?url=http://test.cz/
                    """;

        string[] lines = input.Split('\n', StringSplitOptions.TrimEntries);

        foreach (var line in lines)
        {
            Console.WriteLine("----------------");
            Console.WriteLine($"Input: {line}");

            var match = regex.Match(line);
            if (!match.Success)
            {
                Console.WriteLine("Neni URL");
                continue;
            }

            Console.WriteLine($"Protocol: {match.Groups[1].Value}");
            Console.WriteLine($"Subdomain: {match.Groups[2].Value}");
            Console.WriteLine($"Domain: {match.Groups[3].Value}");
        }

        */
    }
}