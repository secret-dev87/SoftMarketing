using SoftMarketing.Model;
using System.Globalization;
namespace SoftMarketing.WebAPI.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
    public class NotSynced : Exception
    {
        public SyncedData _syncedData { get; set; }
        public NotSynced(SyncedData syncedData) : base() 
        {
            _syncedData = syncedData;
        }
    }
}
