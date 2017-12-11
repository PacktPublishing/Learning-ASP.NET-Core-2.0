﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using Amazon.Runtime;

namespace TicTacToe.Services
{
    public class AmazonWebServicesMonitoringService : IMonitoringService
    {
        readonly AmazonCloudWatchClient _telemetryClient = new AmazonCloudWatchClient();

        public void TrackEvent(string eventName, TimeSpan elapsed, IDictionary<string, string> properties = null)
        {            
            var dimension = new Dimension
            {
                Name = eventName,
                Value = eventName
            };

            var metric1 = new MetricDatum
            {
                Dimensions = new List<Dimension> { dimension },
                MetricName = eventName,
                StatisticValues = new StatisticSet(),
                Timestamp = DateTime.Today,
                Unit = StandardUnit.Count
            };

            if (properties?.ContainsKey("value") == true)
                metric1.Value = long.Parse(properties["value"]);
            else
                metric1.Value = 1;

            var request = new PutMetricDataRequest
            {
                MetricData = new List<MetricDatum>() { metric1 },
                Namespace = eventName
            };

            _telemetryClient.PutMetricDataAsync(request).Wait();
        }
    }
}
