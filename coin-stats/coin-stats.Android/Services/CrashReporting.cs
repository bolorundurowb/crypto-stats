using System;
using Android.App;
using coin_stats.Services;

namespace coin_stats.Android.Services
{
    public class CrashReporting : ICrashReporting
    {
        public bool CrashReportingInit()
        {
            try
            {
                var context = Application.Context;
                Fabric.Fabric.With(context, new Crashlytics.Crashlytics());
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CrashReportingInit - " +
                                                   " failed - " + exception.Message);
                return true;
            }

            return false;
        }

        public bool CrashReportingMisc()
        {
            try
            {
                // Optional: Setup Xamarin / Mono Unhandled exception parsing / handling
                Crashlytics.Crashlytics.HandleManagedExceptions();
                // CrashReportingMiscDone = true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("CrashReportingMisc - " +
                                                   " failed - " + exception.Message);
                return true;
            }

            return false;
        }

        public void ForceCrash()
        {
            throw new ApplicationException("This is a forced crash");
        }
    }
}