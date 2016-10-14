using System;
using System.Diagnostics;
using App.Waes.ScalableWeb.WebApi.Server.Start;
using Microsoft.Owin.Hosting;

namespace App.Waes.ScalableWeb.WebApi
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var app = new WebApiStartup();
                var url = @"http://localhost:9001";
                using (WebApp.Start(url, appBuilder => app.Start(url, appBuilder)))
                {
                    Console.ReadLine();
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError($"Error: {exception}");
            }

            Console.ReadLine();
        }
    }
}