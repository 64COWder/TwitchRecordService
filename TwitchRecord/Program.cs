using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using AutoTwitchRecord;
using Newtonsoft.Json;

namespace TwitchRecord
{
    public class Program
    {
        public static readonly string ChannelName = ConfigurationManager.AppSettings["ChannelName"];
        public static readonly string SaveToFolder = ConfigurationManager.AppSettings["SaveToFolder"];

        public static void Main(string[] args)
        {
            StreamerIsLive();
            if (!IsLivestreamerRunning())
            {
                if (StreamerIsLive())
                {
                    StartLivestreamerRecording();
                }
            }
        }

        private static void StartLivestreamerRecording()
        {
            var command = @"C:\Program Files (x86)\Livestreamer\livestreamer.exe";
            var args = $@"twitch.tv/{ChannelName} best -o {SaveToFolder}" + ChannelName + "-" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".mp4";

            var process = new Process
            {
                StartInfo =
                {
                    FileName = command,
                    Arguments = args
                }
            };
            process.Start();
        }

        private static bool StreamerIsLive()
        {
            using (var webClient = new WebClient())
            {
                var stream = JsonConvert.DeserializeObject<RootObject>(webClient.DownloadString($"https://api.twitch.tv/kraken/streams/{ChannelName}"));
                return stream?.stream != null;
            }
        }

        public static bool IsLivestreamerRunning()
        {
            return Process.GetProcessesByName("livestreamer").Any();
        }
    }
}