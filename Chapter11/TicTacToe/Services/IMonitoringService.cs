using System;
using System.Collections.Generic;

namespace TicTacToe.Services
{
    public interface IMonitoringService
    {
        void TrackEvent(string eventName, TimeSpan elapsed, IDictionary<string, string> properties = null);
    }
}