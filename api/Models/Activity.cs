using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SchedulePath.Helper;

namespace SchedulePath.Models
{
    public class Activity: ICloneable<Activity>
    {
        public Activity()
        {
            ActivityDependencies = new List<Activity>();
            Flip = 1;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Units { get; set; }
        public float SafeProductivityRate { get; set; }
        public float AggressiveProductivityRate { get; set; }
        public float StartToFinish { get; set; }
        public float UnitDelta { get; set; }
        public string Dependencies { get; set; }
        public int ProcessId { get; set; }
        public bool inputProdRate { get; set; }
        public float Duration { get; set; }
        public float AggressiveDuration { get; set; }
        public DurationFunction? DurationFunction { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivitySection Section { get; set; }
        
        [NotMapped]
        public float StartingDuration { get; set; }
        [NotMapped]
        public float StartingDurationApprox
        {
            get
            {
                return (float) TruncateDecimal(StartingDuration, 2);
            }
        }
        [NotMapped]
        public float StartingUnit { get; set; }
        [NotMapped]
        public string ProcessName { get; set; }
        [NotMapped]
        public int? CalculatedProcessIndex { get; set; }
        [NotMapped]
        public IList<Activity> ActivityDependencies { get; set; }
        [NotMapped]
        public float FromDuration
        {
            get
            {
                return (float) TruncateDecimal(StartingDuration, 2);
            }
        }
        [NotMapped]
        public float FromUnit
        {
            get
            {
                return (float)TruncateDecimal(StartingUnit, 2);
            }
        }
        [NotMapped]
        public float ToDuration
        {
            get
            {
                return (float)TruncateDecimal(StartingDuration + Duration, 2);
            }
        }
        [NotMapped]
        public float ToUnit
        {
            get
            {
                return (float)TruncateDecimal(StartingUnit + Units, 2);
            }
        }
        [NotMapped]
        public int Flip { get; set; }
        [NotMapped]
        public float FeedingBuffer { get; set; }
        [NotMapped]
        public float LinkShift { get; set; }
        public decimal TruncateDecimal(double value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            var tmp = Math.Truncate(step * (decimal)value);
            return tmp / step;
        }

        public Activity Clone()
        {
            return new Activity
            {
                ActivityDependencies = ActivityDependencies,
                AggressiveDuration = AggressiveDuration,
                AggressiveProductivityRate = AggressiveProductivityRate,
                CalculatedProcessIndex = CalculatedProcessIndex,
                Dependencies = Dependencies,
                Duration = Duration,
                DurationFunction = DurationFunction,
                FeedingBuffer = FeedingBuffer,
                Flip = Flip,
                Id = Id,
                inputProdRate = inputProdRate,
                LinkShift = LinkShift,
                Name = Name,
                ProcessId = ProcessId,
                ProcessName = ProcessName,
                SafeProductivityRate = SafeProductivityRate,
                Section = Section,
                StartingDuration = StartingDuration,
                StartingUnit = StartingUnit,
                StartToFinish = StartToFinish,
                UnitDelta = UnitDelta,
                Units = Units
            };
        }
    }

    [NotMapped]
    public class ActivityRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Units { get; set; }
        public float SafeProductivityRate { get; set; }
        public float AggressiveProductivityRate { get; set; }
        public float Duration { get; set; }
        public float AggressiveDuration { get; set; }
        public bool InputProdRate { get; set; }
        public float StartToFinish { get; set; }
        public float UnitDelta { get; set; }
        public DurationFunction? DurationFunction { get; set; }
        public string Dependencies { get; set; }
        public int ProcessId { get; set; }
        public ActivitySection Section { get; set; }
    }
}
