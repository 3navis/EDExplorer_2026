using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EDExplorer
{
    public class JournalEventStats
    {
        public long TotalEvents { get; set; }
        public Dictionary<string, JournalEventStat> Events { get; set; } = new Dictionary<string, JournalEventStat>();

        public static JournalEventStats Load(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return new JournalEventStats();

            try
            {
                JournalEventStats stats = JsonConvert.DeserializeObject<JournalEventStats>(json);
                if (stats == null)
                    return new JournalEventStats();

                if (stats.Events == null)
                    stats.Events = new Dictionary<string, JournalEventStat>();

                return stats;
            }
            catch
            {
                return new JournalEventStats();
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void RegisterEvent(string eventName, DateTime timestamp)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return;

            //TotalEvents++;

            if (!Events.TryGetValue(eventName, out JournalEventStat stat))
            {
                // Primera aparicion, es un Nuevo evento
                System.Diagnostics.Debug.WriteLine(eventName);


                stat = new JournalEventStat
                {
                    FirstSeen = timestamp,
                    LastSeen = timestamp
                };

                Events[eventName] = stat;

                TotalEvents++;
                stat.Count++;
            }
            else 
            {
                if (timestamp < stat.FirstSeen || timestamp > stat.LastSeen) 
                {
                    // Solo contabilizar si es primera aparicion (evita relecturas de log en arranque)
                    TotalEvents++;
                    stat.Count++;

                    if (timestamp < stat.FirstSeen) stat.FirstSeen = timestamp;
                    if (timestamp > stat.LastSeen) stat.LastSeen = timestamp;
                }
                ;
            }
            ;

            //stat.Count++;
            //if (timestamp < stat.FirstSeen) stat.FirstSeen = timestamp;
            //if (timestamp > stat.LastSeen) stat.LastSeen = timestamp;
        }
    }

    public class JournalEventStat
    {
        public long Count { get; set; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
