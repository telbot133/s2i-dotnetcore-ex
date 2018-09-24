using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace app
{
  public class TaskManager
  {
    Timer timer = new Timer();

    public void OnStart(string[] args)
    {
      //handle Elapsed event
      timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

      //This statement is used to set interval to 2minute (= 60,000 milliseconds)

      timer.Interval = 60000;

      //enabling the timer
      timer.Enabled = true;
      timer.Start();

    }
    private void OnElapsedTime(object source, ElapsedEventArgs e)
    {
      try
      {
        HttpClient client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(58);
        var responseString = client.GetStringAsync("https://tel-bot.000webhostapp.com/updates.php");
      }
      catch (Exception ex) {
        Console.WriteLine(ex.Message);
      }
    
    }
  }
    public class Program
    {
        public static void Main(string[] args)
        {
             new TaskManager().OnStart(args);
             CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
