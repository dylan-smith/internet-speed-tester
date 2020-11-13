using Dapper;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace InternetSpeedTester
{
    class Program
    {
        static async Task Main()
        {
            while (true)
            {
                RunSpeedTest();
                Console.WriteLine("Waiting for 20 mins before next run...");
                await Task.Delay(300000); // wait 20 mins
            }
        }

        private static void RunSpeedTest()
        {
            var toolPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "speedtest.exe");

            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = toolPath;
            p.StartInfo.Arguments = "-f json";

            Console.WriteLine($"Running Speed Test: {p.StartInfo.FileName} {p.StartInfo.Arguments}");
            p.Start();

            string jsonOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            Console.WriteLine(jsonOutput);

            Console.WriteLine("Saving results to database...");
            var results = new TestResult(jsonOutput);
            WriteToDatabase(results);

            Console.WriteLine("Results saved to database");
        }

        private static void WriteToDatabase(TestResult results)
        {
            var connectionString = "Data Source=localhost;Integrated Security=true;Initial Catalog=SpeedTestResults;";
            var sql = "INSERT INTO SpeedTests (SpeedTestId, SpeedTestTimestamp, Jitter, Latency, DownloadBandwidth, DownloadBytes, DownloadElapsed, UploadBandwidth, UploadBytes, UploadElapsed, PacketLoss, ISP, InternalIp, InterfaceName, MacAddress, IsVpn, ExternalIp, ServerId, ServerName, ServerLocation, ServerCountry, ServerHost, ServerPort, ServerIp, ResultId, ResultUrl) VALUES (@SpeedTestId, @SpeedTestTimestamp, @Jitter, @Latency, @DownloadBandwidth, @DownloadBytes, @DownloadElapsed, @UploadBandwidth, @UploadBytes, @UploadElapsed, @PacketLoss, @Isp, @InternalIp, @InterfaceName, @MacAddress, @IsVpn, @ExternalIp, @ServerId, @ServerName, @ServerLocation, @ServerCountry, @ServerHost, @ServerPort, @ServerIp, @ResultId, @ResultUrl)";

            using var conn = new SqlConnection(connectionString);
            conn.Execute(sql, new { SpeedTestId = Guid.NewGuid(), SpeedTestTimestamp = results.Timestamp, results.Jitter, results.Latency, results.DownloadBandwidth, results.DownloadBytes, results.DownloadElapsed, results.UploadBandwidth, results.UploadBytes, results.UploadElapsed, results.PacketLoss, results.Isp, results.InternalIp, results.InterfaceName, results.MacAddress, results.IsVpn, results.ExternalIp, results.ServerId, results.ServerName, results.ServerLocation, results.ServerCountry, results.ServerHost, results.ServerPort, results.ServerIp, results.ResultId, results.ResultUrl });
        }
    }
}
