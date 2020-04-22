using coin_stats.Services;

namespace coin_stats.iOS.Services
{
    public class CrashReporting : ICrashReporting
    {
        public Boolean CrashReportingInit()
        {
            try
            {
                Firebase.Core.App.Configure();

                Crashlytics.Configure();
                //Fabric.Fabric.SharedSdk.Debug = true;

                Fabric.Fabric.With(typeof(Crashlytics));
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CrashReportingInit - " +
                                                   " failed - " + exception.Message);
                return true;
            }

            return false;
        }

        public Boolean CrashReportingMisc()
        {
            return false;
        }

        public Void ForceCrash()
        {
            throw new ApplicationException("This is a forced crash - iOS");
        }
    }
}