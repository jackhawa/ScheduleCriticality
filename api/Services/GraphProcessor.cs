﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchedulePath.Models;

namespace SchedulePath.Services
{
    public class GraphProcessor : IGraphProcessor
    {
        public GraphConfig ProcessGraph(bool withCriticalPath, Schedule upperResult, Schedule lowerResult)
        {
            GraphConfig upwardGraph = null;
            GraphConfig downwardGraph = null;
            List<Series> allSeries = new List<Series>();

            if (upperResult != null)
            {
                upwardGraph = BuildConfig().AddActivities(upperResult.Activities);
                if (withCriticalPath)
                {
                    upwardGraph.AddCriticalPath(upperResult.CriticalPath.ActivityDirections)
                        .AddProjectBuff(upperResult.CriticalPath.ProjectBuffer)
                        .AddFeedingBuffers(upperResult.FeedingBuffers);
                }
                allSeries = upwardGraph.series.ToList();
            }

            if (lowerResult != null)
            {
                downwardGraph = BuildConfig().AddActivities(lowerResult.Activities);
                if (withCriticalPath)
                {
                    downwardGraph.AddCriticalPath(lowerResult.CriticalPath.ActivityDirections)
                        .AddProjectBuff(lowerResult.CriticalPath.ProjectBuffer)
                        .AddFeedingBuffers(lowerResult.FeedingBuffers);
                }

                foreach (var serie in downwardGraph.series)
                {
                    serie.data.ToList().ForEach(d => d[1] = d[1] * -1);
                    allSeries.Add(serie);
                }
            }
            
            if(upwardGraph != null) upwardGraph.series = allSeries.ToArray();
            return upwardGraph;
        }

        private GraphConfig BuildConfig()
        {
            return new GraphConfig
            {
                chart = new Chart
                {
                    type = "line"
                },
                title = new Title
                {
                    text = ""
                },
                xAxis = new XAxis
                {
                    title = new Title
                    {
                        text = "Duration (days)"
                    }
                },
                yAxis = new YAxis
                {
                    title = new Title
                    {
                        text = "Units"
                    }
                },
                plotOptions = new PlotOptions
                {
                    line = new Line
                    {
                        dataLabels = new DataLabels
                        {
                            enabled = true
                        }
                    }
                },
                legend = new Legend
                {
                    layout = "vertical",
                    align = "right",
                    verticalAlign = "middle",
                    borderWidth = 0
                },
                series = new List<Series>().ToArray()
            };
        }
    }
}
