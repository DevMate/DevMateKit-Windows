using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DevMateKit.DMFeedback.API;
using DevMateKit.DMFeedback.UI;
using DevMateKit.DMFramework;
using DevMateKit.DMTracking.API;

namespace SimpleDevmate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        //-------------------------------------------------------------------
        // 
        //  Constructors 
        //
        //------------------------------------------------------------------- 

        #region Constructors

        #endregion

        //--------------------------------------------------------------------
        // 
        //  Public Methods
        // 
        //------------------------------------------------------------------- 

        #region Public Methods


        #endregion

        //--------------------------------------------------------------------
        // 
        //  Public Properties
        //
        //--------------------------------------------------------------------

        #region Public Properties

        #endregion

        //-------------------------------------------------------------------
        // 
        //  Public Event
        //
        //-------------------------------------------------------------------- 

        #region Public Event

        #endregion

        //-------------------------------------------------------------------
        // 
        //  Private Fields 
        //
        //-------------------------------------------------------------------- 

        #region Private Fields

        private bool _isFirstRun = true;

        #endregion

        //-------------------------------------------------------------------
        //
        //  Protected Methods
        // 
        //--------------------------------------------------------------------

        #region Protected Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            //Set Crash Report
            SetCrashReport();

            //Set properties
            DMFrameworkSettings.CompanyName = "Company Name";
            DMFrameworkSettings.ApplicationName = "Application Name";
            DMFrameworkSettings.ApplicationBundleId = "ApplicationBundleId";
            DMFrameworkSettings.ApplicationVersion = "1.0.0";
            DMFrameworkSettings.Localization = "en";
            DMFrameworkSettings.IsBeta = false;

            if (_isFirstRun)
                //Mark install on Dashboard
                DMFrameworkSettings.IsFirstRun = true;

            //initialize start app
            TrackingFramework.Current.ApplicationStarted();
        }


        #endregion

        //-------------------------------------------------------------------
        // 
        //  Private Methods
        //
        //-------------------------------------------------------------------

        #region Private Methods


        private void SetCrashReport()
        {
            ApplicationData.SetApplicationData();
            App.Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowCrashReportDialog(e.Exception);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowCrashReportDialog(e.ExceptionObject as Exception);
        }

        private void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowCrashReportDialog(e.Exception);
            e.Handled = true;
        }
        #endregion
    }
}
