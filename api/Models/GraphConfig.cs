using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchedulePath.Services;

namespace SchedulePath.Models
{
    public class GraphConfig
    {
        public Chart chart { get; set; }
        public Title title { get; set; }
        public XAxis xAxis { get; set; }
        public YAxis yAxis { get; set; }
        public PlotOptions plotOptions { get; set; }
        public Legend legend { get; set; }
        public Series[] series { get; set; }

        public GraphConfig AddCriticalPath(List<ActivityWithDirection> activityDirections)
        {
            var paths = new List<List<float[]>> { ConvertToPoints(activityDirections) };
            var newSeries = new List<Series>();
            foreach (var p in paths)
            {
                newSeries.Add(new Series
                {
                    name = "Critical Path",
                    data = p.ToArray(),
                    lineWidth = 3,
                    color = "green"
                });
            }
            series = series.Concat(newSeries).ToArray();
            return this;
        }


        public GraphConfig AddProjectBuff(ProjectBuffer projectBuffer)
        {
            var newSeries = new List<Series>();
            var projectBufferList = new List<float[]> {
                new float[] { projectBuffer.StartingDuration, projectBuffer.StartingUnit },
                new float[] { (float)(projectBuffer.StartingDuration + projectBuffer.Buffer), projectBuffer.StartingUnit }
            };

            newSeries.Add(new Series
            {
                name = "Project Buffer",
                data = projectBufferList.ToArray(),
                lineWidth = 3,
                color = "red"
            });
            series = series.Concat(newSeries).ToArray();
            return this;
        }

        internal GraphConfig AddFeedingBuffers(List<List<float[]>> feedingBuffers)
        {
            var newSeries = new List<Series>();
            feedingBuffers.ForEach(fb =>
            {
                newSeries.Add(new Series
                {
                    name = "Feeding Buffer",
                    data = fb.ToArray(),
                    lineWidth = 3,
                    color = "orange"
                });
            });
            series = series.Concat(newSeries).ToArray();
            return this;
        }

        public GraphConfig AddActivities(IEnumerable<Activity> activities)
        {
            var newSeries = new List<Series>();
            foreach (var act in activities)
            {
                newSeries.Add(new Series
                {
                    name = act.Name,
                    data = new float[][]
                    {
                        new float[]
                        {
                            act.StartingDuration + act.FeedingBuffer + act.LinkShift,
                            act.StartingUnit
                        },
                        new float[]
                        {
                            act.StartingDuration + act.Duration  + act.FeedingBuffer + act.LinkShift,
                            act.StartingUnit + act.Units
                        }
                    },
                    lineWidth = 1,
                    color = "blue"
                });
            }
            series = series.Concat(newSeries).ToArray();
            return this;
        }

        private List<float[]> ConvertToPoints(List<ActivityWithDirection> path)
        {
            var points = new List<float[]> { new float[] { 0 + path.First().LinkShift, 0 } };

            foreach (var actWithDir in path)
            {
                if (actWithDir.LinkDistance != null)
                {
                    if (actWithDir.FeedingBuffer != null)
                        points.Add(new float[] { (float)actWithDir.FeedingBuffer[0] + actWithDir.LinkShift,
                            (float)actWithDir.FeedingBuffer[1] });
                    points.Add(new float[] { (float)actWithDir.LinkDistance.StartingDuration +
                    actWithDir.LinkDistance.PreviousFeedingBuffers + 
                    actWithDir.LinkDistance.FeedingBuffer + 
                    actWithDir.LinkDistance.TimePeriod +
                    actWithDir.LinkShift,
                        (float)actWithDir.LinkDistance.StartingUnit * actWithDir.Flip });
                }
                else
                {
                    if (actWithDir.Direction == ActivityDirection.Normal)
                        points.Add(new float[] { actWithDir.Activity.ToDuration + actWithDir.Activity.FeedingBuffer +
                            actWithDir.Activity.LinkShift, actWithDir.Activity.ToUnit * actWithDir.Activity.Flip });
                    if (actWithDir.Direction == ActivityDirection.Reverse)
                        points.Add(new float[] { actWithDir.Activity.FromDuration + actWithDir.Activity.FeedingBuffer +
                            actWithDir.Activity.LinkShift, actWithDir.Activity.FromUnit * actWithDir.Activity.Flip });
                }
            }
            return points;
        }

        private List<List<float[]>> ConvertToPoints(List<List<ActivityWithDirection>> results)
        {
            var criticalPathsPoints = new List<List<float[]>>();

            foreach (var path in results)
            {
                var points = new List<float[]> { new float[] { 0, 0 } };
                criticalPathsPoints.Add(ConvertToPoints(path));
            }
            return criticalPathsPoints;
        }
    }

    public class Title
    {
        public string text { get; set; }
        public int x { get; set; }
    }

    public class XAxis
    {
        public Title title { get; set; }
    }

    public class YAxis
    {
        public Title title { get; set; }
    }

    public class PlotOptions
    {
        public Line line { get; set; }
    }

    public class Line
    {
        public DataLabels dataLabels { get; set; }
    }

    public class DataLabels
    {
        public bool enabled { get; set; }
        public string format { get; set; }
    }

    public class Legend
    {
        public string layout { get; set; }
        public string align { get; set; }
        public string verticalAlign { get; set; }
        public int borderWidth { get; set; }
    }

    public class Series
    {
        public string name { get; set; }
        public float[][] data { get; set; }
        public string color { get; set; }
        public int lineWidth { get; set; }
    }
}
