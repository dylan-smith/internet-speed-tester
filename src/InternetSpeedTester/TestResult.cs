using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace InternetSpeedTester
{
    public class TestResult
    {
        public DateTime Timestamp { get; set; }
        public double Jitter { get; set; }
        public double Latency { get; set; }
        public int DownloadBandwidth { get; set; }
        public int DownloadBytes { get; set; }
        public int DownloadElapsed { get; set; }
        public int UploadBandwidth { get; set; }
        public int UploadBytes { get; set; }
        public int UploadElapsed { get; set; }
        public double PacketLoss { get; set; }
        public string Isp { get; set; }
        public string InternalIp { get; set; }
        public string InterfaceName { get; set; }
        public string MacAddress { get; set; }
        public bool IsVpn { get; set; }
        public string ExternalIp { get; set; }
        public int ServerId { get; set; }
        public string ServerName { get; set; }
        public string ServerLocation { get; set; }
        public string ServerCountry { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public string ServerIp { get; set; }
        public Guid ResultId { get; set; }
        public string ResultUrl { get; set; }

        public TestResult(string json)
        {
            var doc = JsonDocument.Parse(json).RootElement;
            JsonElement x;

            Timestamp = doc.GetProperty("timestamp").GetDateTime();

            if (doc.TryGetProperty("ping", out JsonElement ping))
            {
                if (ping.TryGetProperty("jitter", out x))
                {
                    Jitter = x.GetDouble();
                }

                if (ping.TryGetProperty("latency", out x))
                {
                    Latency = x.GetDouble();
                }
            }

            if (doc.TryGetProperty("download", out JsonElement download))
            {
                if (download.TryGetProperty("bandwidth", out x))
                {
                    DownloadBandwidth = x.GetInt32();
                }

                if (download.TryGetProperty("bytes", out x))
                {
                    DownloadBytes = x.GetInt32();
                }

                if (download.TryGetProperty("elapsed", out x))
                {
                    DownloadElapsed = x.GetInt32();
                }
            }

            if (doc.TryGetProperty("upload", out JsonElement upload))
            {
                if (upload.TryGetProperty("bandwidth", out x))
                {
                    UploadBandwidth = x.GetInt32();
                }

                if (upload.TryGetProperty("bytes", out x))
                {
                    UploadBytes = x.GetInt32();
                }

                if (upload.TryGetProperty("elapsed", out x))
                {
                    UploadElapsed = x.GetInt32();
                }
            }

            if (doc.TryGetProperty("packetLoss", out x))
            {
                PacketLoss = x.GetDouble();
            }

            if (doc.TryGetProperty("isp", out x))
            {
                Isp = x.GetString();
            }

            if (doc.TryGetProperty("interface", out JsonElement interf))
            {
                if (interf.TryGetProperty("internalIp", out x))
                {
                    InternalIp = x.GetString();
                }

                if (interf.TryGetProperty("name", out x))
                {
                    InterfaceName = x.GetString();
                }

                if (interf.TryGetProperty("macAddr", out x))
                {
                    MacAddress = x.GetString();
                }

                if (interf.TryGetProperty("isVpn", out x))
                {
                    IsVpn = x.GetBoolean();
                }

                if (interf.TryGetProperty("externalIp", out x))
                {
                    ExternalIp = x.GetString();
                }
            }

            if (doc.TryGetProperty("server", out JsonElement server))
            {
                if (server.TryGetProperty("id", out x))
                {
                    ServerId = x.GetInt32();
                }

                if (server.TryGetProperty("name", out x))
                {
                    ServerName = x.GetString();
                }

                if (server.TryGetProperty("location", out x))
                {
                    ServerLocation = x.GetString();
                }

                if (server.TryGetProperty("country", out x))
                {
                    ServerCountry = x.GetString();
                }

                if (server.TryGetProperty("host", out x))
                {
                    ServerHost = x.GetString();
                }

                if (server.TryGetProperty("port", out x))
                {
                    ServerPort = x.GetInt32();
                }

                if (server.TryGetProperty("ip", out x))
                {
                    ServerIp = x.GetString();
                }
            }

            if (doc.TryGetProperty("result", out JsonElement result))
            {
                if (result.TryGetProperty("id", out x))
                {
                    ResultId = x.GetGuid();
                }

                if (result.TryGetProperty("url", out x))
                {
                    ResultUrl = x.GetString();
                }
            }
        }
    }
}
