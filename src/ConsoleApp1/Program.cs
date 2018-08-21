﻿using Paillave.Etl;
using System;
//using System.Reactive.Linq;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ConsoleApp1.StreamTypes;
using Paillave.Etl.Core;
using ConsoleApp1.Jobs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
           //new StreamProcessRunner<TestJob3, MyConfig>().GetDefinitionStructure().OpenVisNetworkStructure();
           var runner = new StreamProcessRunner<TestJob3, MyConfig>();
           StreamProcessDefinition<TraceEvent> traceStreamProcessDefinition = null;// new StreamProcessDefinition<TraceEvent>(traceStream => traceStream.Where("keep log info", i => i.Content.Level <= TraceLevel.Info).ToAction("logs to console", Console.WriteLine));
           var task = runner.ExecuteAsync(new MyConfig
           {
               InputFolderPath = @"C:\Users\sroyer\Source\Repos\Etl.Net\src\TestFiles\",
               InputFilesSearchPattern = "testin.*.txt",
               TypeFilePath = @"C:\Users\sroyer\Source\Repos\Etl.Net\src\TestFiles\ref - Copy.txt",
               DestinationFilePath = @"C:\Users\sroyer\Source\Repos\Etl.Net\src\TestFiles\outfile.csv"
           }, traceStreamProcessDefinition);
           task.Wait();

           task.Result.OpenVisNetworkStatistics();

           Console.WriteLine("Done");
           Console.WriteLine("Press a key...");
        }
        // static void Main(string[] args)
        // {
        //     //new StreamProcessRunner<TestJob3, MyConfig>().GetDefinitionStructure().OpenVisNetworkStructure();
        //     var runner = new StreamProcessRunner<TestJob5, MyConfig2>();
        //     StreamProcessDefinition<TraceEvent> traceStreamProcessDefinition = null;// new StreamProcessDefinition<TraceEvent>(traceStream => traceStream.Where("keep log info", i => i.Content.Level <= TraceLevel.Info).ToAction("logs to console", Console.WriteLine));
        //     var task = runner.ExecuteAsync(new MyConfig2
        //     {
        //         ConnectionString = new System.Data.SqlClient.SqlConnectionStringBuilder
        //         {
        //             DataSource = "localhost",
        //             InitialCatalog = "TestDB",
        //             IntegratedSecurity = true
        //         }.ToString(),
        //         Filter = "a"
        //     }, traceStreamProcessDefinition);
        //     task.Wait();

        //     task.Result.OpenD3SankeyStatistics();

        //     Console.WriteLine("Done");
        //     Console.WriteLine("Press a key...");
        //     Console.ReadKey();
        // }
    }
}
