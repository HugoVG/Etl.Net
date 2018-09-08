﻿using Paillave.Etl;
using System;
using System.IO;
using Paillave.Etl.Core;
using Paillave.Etl.TextFile.Core;
using Paillave.Etl.Core.Streams;

namespace SimpleQuickstart
{
    public class SimpleConfig
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
    }

    public class SimpleInputFileRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryCode { get; set; }
    }

    public class CategoryStatisticFileRow
    {
        public string CategoryCode { get; set; }
        public int Count { get; set; }
    }

    public class SimpleQuickstartJob : IStreamProcessDefinition<SimpleConfig>
    {
        public string Name => "Simple quickstart";

        public void DefineProcess(IStream<SimpleConfig> rootStream)
        {
            var outputFileS = rootStream.Select("open output file", i => new StreamWriter(i.OutputFilePath));
            rootStream
                .CrossApplyTextFile("read input file", new FileDefinition<SimpleInputFileRow>()
                    .MapColumnToProperty("#", i => i.Id)
                    .MapColumnToProperty("Label", i => i.Name)
                    .MapColumnToProperty("Category", i => i.CategoryCode)
                    .IsColumnSeparated('\t'), i => i.InputFilePath)
                .ToAction("Write input file to console", i => Console.WriteLine($"{i.Id}-{i.Name}-{i.CategoryCode}"))
                .Pivot("group and count", i => i.CategoryCode, i => new { Count = AggregationOperators.Count() })
                .Select("create output row", i => new CategoryStatisticFileRow { CategoryCode = i.Key, Count = i.Aggregation.Count })
                .Sort("sort output values", i => new { i.CategoryCode })
                .ToTextFile("write to text file", outputFileS, new FileDefinition<CategoryStatisticFileRow>());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var testFilesDirectory = @"C:\Users\sroyer\Source\Repos\Etl.Net\src\TestFiles";
            // var testFilesDirectory = @"C:\Users\paill\source\repos\Etl.Net\src\TestFiles";

            new StreamProcessRunner<SimpleQuickstartJob, SimpleConfig>().ExecuteAsync(new SimpleConfig
            {
                InputFilePath = Path.Combine(testFilesDirectory, "simpleinputfile.csv"),
                OutputFilePath = Path.Combine(testFilesDirectory, "simpleoutputfile.csv")
            }, null).Wait();
            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }
    }
}