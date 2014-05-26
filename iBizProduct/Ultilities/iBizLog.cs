using System.Diagnostics;

namespace iBizProduct.Ultilities
{
    public class iBizLog
    {
        public static string EventLogName = "iBizProduct";
        public const string EventLogSource = "iBizProduct v3";

        private static readonly EventLog log;

        static iBizLog()
        {
            log = new EventLog(EventLogName)
            {
                Source = EventLogSource,
            };
        }

        public static void WriteInformation(string message, params object[] args)
        {
            log.WriteEntry(message.FormatCurrentCulture(args), EventLogEntryType.Information);
        }

        public static void WriteError(string message, params object[] args)
        {
            log.WriteEntry(message.FormatCurrentCulture(args), EventLogEntryType.Error);
        }

        public static void WriteWarning(string message, params object[] args)
        {
            log.WriteEntry(message.FormatCurrentCulture(args), EventLogEntryType.Warning);
        }

        internal static void CreateEventSource()
        {
            EventLog.CreateEventSource(EventLogSource, EventLogName);
        }
    }
}
