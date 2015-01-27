﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertFromIISLogFile
{
    public class Statistik
    {
        private const string Header = "DateTime;Method;Requests;SumServerReceivedBytes;SumServerSentBytes;AverageTimeTaken;";

        public static void CreateStatistik(List<LogEntry> logEntries, string resolution, Action<string> writeOutputCallback, Action<string> writeVerboseCallback, Func<bool> isStopRequested)
        {
            writeVerboseCallback.Invoke("Log entries count: " + logEntries.Count);
            writeVerboseCallback.Invoke("Sorting log entries ...");
            logEntries = logEntries.OrderBy(x => x.DateTime).ToList();

            TimeSpan resolutionTimeSpan = GetTimeSpanResolution(resolution);

            var first = logEntries.First();
            var last = logEntries.Last();

            writeVerboseCallback.Invoke(String.Format("Creating a csv from {0} to last {1} over {2} entries", first.DateTimeLocalTime, last.DateTimeLocalTime, logEntries.Count));
            writeOutputCallback.Invoke(Header);

            foreach (var groupByTime in logEntries.GroupBy(x=>(int)(x.DateTimeLocalTime.Ticks / resolutionTimeSpan.Ticks)* resolutionTimeSpan.Ticks))
            {
                if (isStopRequested.Invoke()) return;

                var dateTime = new DateTime(groupByTime.Key);
                writeOutputCallback.Invoke(string.Format("{0};{1};{2};{3};{4};{5};",
                      dateTime,
                      "All",
                      groupByTime.Count(),
                      groupByTime.Sum(x => x.ServerReceivedBytes),
                      groupByTime.Sum(x => x.ServerSentBytes),
                      groupByTime.Average(x => x.TimeTaken)
                      ));


                foreach (var groupByMethod in groupByTime.GroupBy(x=>x.UriStem))
                {
                    if (isStopRequested.Invoke()) return;

                    writeOutputCallback.Invoke(string.Format("{0};{1};{2};{3};{4};{5};", 
                        dateTime, 
                        groupByMethod.Key, 
                        groupByMethod.Count(),
                        groupByMethod.Sum(x=>x.ServerReceivedBytes),
                        groupByMethod.Sum(x=>x.ServerSentBytes),
                        groupByMethod.Average(x=>x.TimeTaken)
                        ));
                }
            }
        }

        private void Temp(Action<string> writeOutputCallback, DateTime timestamp, string name, IList<LogEntry> groupByMethod)
        {
            writeOutputCallback.Invoke(string.Format("{0};{1};{2};{3};{4};{5};",
                       timestamp,
                       name,
                       groupByMethod.Count(),
                       groupByMethod.Sum(x => x.ServerReceivedBytes),
                       groupByMethod.Sum(x => x.ServerSentBytes),
                       groupByMethod.Average(x => x.TimeTaken)
                       ));
        }

        private static TimeSpan GetTimeSpanResolution(string resolution)
        {
            switch (resolution)
            {
                case EportIISLogStats.ResolutionWeek:
                    return new TimeSpan(7, 0, 0, 0);
                case EportIISLogStats.ResolutionDay:
                    return new TimeSpan(1, 0, 0, 0);
                case EportIISLogStats.ResolutionHour:
                    return new TimeSpan(1, 0, 0);
                case EportIISLogStats.ResolutionMinute:
                    return new TimeSpan(0, 1, 0);
                default:
                    throw new Exception();
            }
        }
    }
}