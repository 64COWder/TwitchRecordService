using System.ServiceProcess;
using System.Timers;

namespace TwitchRecordService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        public Service1()
        {
            ServiceName = "TwitchRecord";
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(30000D); // 30 seconds
            timer.AutoReset = true;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer = null;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TwitchRecord.Program.Main(null);
        }
    }
}