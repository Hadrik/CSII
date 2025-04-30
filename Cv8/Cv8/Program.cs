using System.Net.Http.Json;
using MailKit.Net.Smtp;
using MimeKit;

namespace Cv8;

class Program
{
    private const string Url = "https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0";
    
    static async Task Main(string[] args)
    {
        /*
        using var client = new HttpClient();
        using var response = await client.GetAsync(Url);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<ResponseData>();

        foreach (var timepoint in data!.Dataseries)
        {
            Console.WriteLine($"Timepoint: {timepoint.Timepoint}\tTemperature: {timepoint.Temp2M}");
        }
        */

        var msg = new MimeMessage();
        
        msg.To.Add(new MailboxAddress("Pepa Labus", "richard.travnicek.st@vsb.cz"));
        msg.From.Add(new MailboxAddress("Standa Rafika", "atnet2019@seznam.cz"));
        msg.Subject = "Ahoj kundo";
        var bd = new BodyBuilder();
        bd.TextBody = "Text";
        bd.HtmlBody = "<h1>HTML</h1>";
        msg.Body = bd.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.seznam.cz", 465, true);
        await client.AuthenticateAsync("atnet2019@seznam.cz", "cviceni-C#2025");
        await client.SendAsync(msg);
        await client.DisconnectAsync(true);
    }
}

class ResponseData
{
    public List<Datapoint> Dataseries { get; set; }

    public class Datapoint
    {
        public int Timepoint { get; set; }
        public int Temp2M { get; set; }
    }
}