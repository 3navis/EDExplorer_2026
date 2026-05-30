using System;
using System.Collections.Generic;

namespace EDExplorer
{
    class JournalEventStatsReader
    {
        public List<Interes> Interest { get; private set; }

        private readonly Alertas alertas;
        private readonly LogMonitor logMonitor;

        public JournalEventStatsReader(Base b)
        {
            logMonitor = b.logMonitor;
            alertas = b.alertas;
            Interest = new List<Interes>();
        }

        public bool hayAlertas()
        {
            return Interest.Count > 0;
        }

        private double GetFrequency(JournalEventStat stat)
        {
            if (logMonitor.EventStats == null || logMonitor.EventStats.TotalEvents == 0)
                return 0;

            return (double)stat.Count / logMonitor.EventStats.TotalEvents;
        }

        private int GetDaysSinceLastSeen(JournalEventStat stat)
        {
            return Math.Max(0, (int)(DateTime.Now - stat.LastSeen).TotalDays);
        }
    }
}
