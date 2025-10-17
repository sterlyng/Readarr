using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using NLog;
using NUnit.Framework;
using Readarr.Common.EnvironmentInfo;
using Readarr.Common.Extensions;
using Readarr.Common.Processes;
using Readarr.Common.Serializer;
using Readarr.Core.Configuration;
using Readarr.Core.Datastore;
using RestSharp;

namespace Readarr.Test.Common
{
    public class ReadarrRunner
    {
        private readonly IProcessProvider _processProvider;
        private readonly IRestClient _restClient;
        private Process _ReadarrProcess;

        public string AppData { get; private set; }
        public string ApiKey { get; private set; }
        public PostgresOptions PostgresOptions { get; private set; }
        public int Port { get; private set; }

        public ReadarrRunner(Logger logger, PostgresOptions postgresOptions, int port = 8989)
        {
            _processProvider = new ProcessProvider(logger);
            _restClient = new RestClient($"http://localhost:{port}/api/v3");

            PostgresOptions = postgresOptions;
            Port = port;
        }

        public void Start(bool enableAuth = false)
        {
            AppData = Path.Combine(TestContext.CurrentContext.TestDirectory, "_intg_" + TestBase.GetUID());
            Directory.CreateDirectory(AppData);

            GenerateConfigFile(enableAuth);

            string consoleExe;
            if (OsInfo.IsWindows)
            {
                consoleExe = "Readarr.Console.exe";
            }
            else
            {
                consoleExe = "Readarr";
            }

            if (BuildInfo.IsDebug)
            {
                Start(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "_output", "net8.0", consoleExe));
            }
            else
            {
                Start(Path.Combine(TestContext.CurrentContext.TestDirectory, "bin", consoleExe));
            }

            while (true)
            {
                _ReadarrProcess.Refresh();

                if (_ReadarrProcess.HasExited)
                {
                    Assert.Fail("Process has exited");
                }

                var request = new RestRequest("system/status");
                request.AddHeader("Authorization", ApiKey);
                request.AddHeader("X-Api-Key", ApiKey);

                var statusCall = _restClient.Get(request);

                if (statusCall.ResponseStatus == ResponseStatus.Completed)
                {
                    TestContext.Progress.WriteLine($"Readarr {Port} is started. Running Tests");
                    return;
                }

                TestContext.Progress.WriteLine("Waiting for Readarr to start. Response Status : {0}  [{1}] {2}", statusCall.ResponseStatus, statusCall.StatusDescription, statusCall.ErrorException.Message);

                Thread.Sleep(500);
            }
        }

        public void Kill()
        {
            try
            {
                if (_ReadarrProcess != null)
                {
                    _ReadarrProcess.Refresh();
                    if (_ReadarrProcess.HasExited)
                    {
                        var log = File.ReadAllLines(Path.Combine(AppData, "logs", "Readarr.trace.txt"));
                        var output = log.Join(Environment.NewLine);
                        TestContext.Progress.WriteLine("Process has exited prematurely: ExitCode={0} Output:\n{1}", _ReadarrProcess.ExitCode, output);
                    }

                    _processProvider.Kill(_ReadarrProcess.Id);
                }
            }
            catch (InvalidOperationException)
            {
                // May happen if the process closes while being closed
            }

            TestBase.DeleteTempFolder(AppData);
        }

        public void KillAll()
        {
            try
            {
                if (_ReadarrProcess != null)
                {
                    _processProvider.Kill(_ReadarrProcess.Id);
                }

                _processProvider.KillAll(ProcessProvider.Readarr_CONSOLE_PROCESS_NAME);
                _processProvider.KillAll(ProcessProvider.Readarr_PROCESS_NAME);
            }
            catch (InvalidOperationException)
            {
                // May happen if the process closes while being closed
            }

            TestBase.DeleteTempFolder(AppData);
        }

        private void Start(string outputReadarrConsoleExe)
        {
            StringDictionary envVars = new();
            if (PostgresOptions?.Host != null)
            {
                envVars.Add("Readarr__Postgres__Host", PostgresOptions.Host);
                envVars.Add("Readarr__Postgres__Port", PostgresOptions.Port.ToString());
                envVars.Add("Readarr__Postgres__User", PostgresOptions.User);
                envVars.Add("Readarr__Postgres__Password", PostgresOptions.Password);
                envVars.Add("Readarr__Postgres__MainDb", PostgresOptions.MainDb);
                envVars.Add("Readarr__Postgres__LogDb", PostgresOptions.LogDb);

                TestContext.Progress.WriteLine("Using env vars:\n{0}", envVars.ToJson());
            }

            TestContext.Progress.WriteLine("Starting instance from {0} on port {1}", outputReadarrConsoleExe, Port);

            var args = "-nobrowser -nosingleinstancecheck -data=\"" + AppData + "\"";
            _ReadarrProcess = _processProvider.Start(outputReadarrConsoleExe, args, envVars, OnOutputDataReceived, OnOutputDataReceived);
        }

        private void OnOutputDataReceived(string data)
        {
            TestContext.Progress.WriteLine($" [{Port}] > " + data);

            if (data.Contains("Press enter to exit"))
            {
                _ReadarrProcess.StandardInput.WriteLine(" ");
            }
        }

        private void GenerateConfigFile(bool enableAuth)
        {
            var configFile = Path.Combine(AppData, "config.xml");

            // Generate and set the api key so we don't have to poll the config file
            var apiKey = Guid.NewGuid().ToString().Replace("-", "");

            var xDoc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ConfigFileProvider.CONFIG_ELEMENT_NAME,
                             new XElement(nameof(ConfigFileProvider.ApiKey), apiKey),
                             new XElement(nameof(ConfigFileProvider.LogLevel), "trace"),
                             new XElement(nameof(ConfigFileProvider.AnalyticsEnabled), false),
                             new XElement(nameof(ConfigFileProvider.AuthenticationMethod), enableAuth ? "Forms" : "None"),
                             new XElement(nameof(ConfigFileProvider.AuthenticationRequired), "DisabledForLocalAddresses"),
                             new XElement(nameof(ConfigFileProvider.Port), Port)));

            var data = xDoc.ToString();

            File.WriteAllText(configFile, data);

            ApiKey = apiKey;
        }
    }
}
