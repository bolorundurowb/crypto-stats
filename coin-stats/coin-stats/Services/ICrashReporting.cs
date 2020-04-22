namespace coin_stats.Services
{
    public interface ICrashReporting
    {
        bool CrashReportingInit();
        
        bool CrashReportingMisc();
        
        void ForceCrash();
    }
}