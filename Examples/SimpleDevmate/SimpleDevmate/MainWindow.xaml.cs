using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevMateKit.DMActivation.API;
using DevMateKit.DMActivation.UI;
using DevMateKit.DMFeedback.API;
using DevMateKit.DMFeedback.UI;
using DevMateKit.DMFramework;
using DevMateKit.DMUpdates.API;
using DevMateKit.DMUpdates.UI;

namespace SimpleDevmate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //-------------------------------------------------------------------
        // 
        //  Constructors 
        //
        //------------------------------------------------------------------- 

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
        }
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

        #endregion



        //-------------------------------------------------------------------
        // 
        //  Private Methods
        //
        //-------------------------------------------------------------------

        #region Private Methods

        #region Report
        private void FeedBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationData.SetApplicationData();
          //  DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowDialog(FeedbackTypes.Feedback);
        }

        private void BugReportBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationData.SetApplicationData();
            DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowDialog(FeedbackTypes.BugReport);
        }

        private void SupportRequestBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ApplicationData.SetApplicationData();
            DMFeedbackWindowController.BigIconSource = "pack://application:,,,/Images/icon.png";
            DMFeedbackWindowController.ShowDialog(FeedbackTypes.SupportRequest);
        }
        private void CrashMeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new Exception("test Crash");
        }



        #endregion

        #region Activation
        private void ActivationBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DMActivationWindow window = new DMActivationWindow();
            window.BigIconSource = new BitmapImage(new Uri("pack://application:,,,/Images/icon.png"));
            window.ShowDialog();
        }
        private void GetActivationInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var x = ActivationFramework.Current.FindDelegate((int)DelegateType.IsActivated) as Func<int>;
            var res = x.Invoke();

            if (res == 1)
            {
                ActStatusTextBlock.Text = "Activated";
                ActStatusTextBlock.Foreground = (Brush)new BrushConverter().ConvertFromString("Green");
            }
            else if (res == 0)
            {
                ActStatusTextBlock.Text = "Not Activated";
                ActStatusTextBlock.Foreground = (Brush)new BrushConverter().ConvertFromString("Red");
            }
            else if (res == 2)
            {
                ActStatusTextBlock.Text = "Expired";
                ActStatusTextBlock.Foreground = (Brush)new BrushConverter().ConvertFromString("Red");
            }

            var func = ActivationFramework.Current.FindDelegate((int)DelegateType.LicenseInformation) as Func<int, object>;
            List<string> infos = new List<string>();


            object ActivationIdKey = func.Invoke((int)LicenseInformationKeys.ActivationIdKey);
            if (ActivationIdKey != null)
                infos.Add("ActivationIdKey: " + ActivationIdKey.ToString());

            object UserName = func.Invoke((int)LicenseInformationKeys.UserNameKey);
            if (UserName != null)
                infos.Add("User Name: " + UserName.ToString());

            object UserEmail = func.Invoke((int)LicenseInformationKeys.UserEmailKey);
            if (UserEmail != null)
                infos.Add("User Email: " + UserEmail.ToString());

            object ActivationDate = func.Invoke((int)LicenseInformationKeys.ActivationDateKey);
            if (ActivationDate != null)
            {
                long expDateUnix = Convert.ToInt64(ActivationDate);
                //  ActivationTimeController.ActivationStarted = expDateUnix;
                DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expDateUnix);
                infos.Add("Activation Date: " + expDateTime.ToString());
            }

            object ExpirationDate = func.Invoke((int)LicenseInformationKeys.ExpirationDateKey);
            if (ExpirationDate != null)
            {
                long expDateUnix = Convert.ToInt64(ExpirationDate);
                if (expDateUnix == 0)
                {
                    // ActivationTimeController.ActivationExpire = expDateUnix;
                    infos.Add("Expiration Date: Unlimited");
                }
                else
                {
                    //    ActivationTimeController.ActivationExpire = expDateUnix;

                    DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expDateUnix);
                    infos.Add("Expiration Date: " + expDateTime.ToString());
                }
            }

            object ExpirationVersion = func.Invoke((int)LicenseInformationKeys.ExpirationVersionKey);
            if (ExpirationVersion != null)
                infos.Add("Expiration Version: " + ExpirationVersion.ToString());

            object ActivationTag = func.Invoke((int)LicenseInformationKeys.ActivationTagKey);
            if (UserEmail != null)
                infos.Add("Activation Tag: " + ActivationTag.ToString());

            object IsBetaOnly = func.Invoke((int)LicenseInformationKeys.BetaOnlyKey);
            if (UserEmail != null)
                infos.Add("IsBetaOnly: " + IsBetaOnly.ToString());

            object ApplicationBundleId = func.Invoke((int)LicenseInformationKeys.ApplicationBundleId);
            if (ApplicationBundleId != null)
                infos.Add("Application Bundle Id: " + ApplicationBundleId.ToString());

            InfosListBox.ItemsSource = infos;
        }
        private void InvalidateActivationBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var x = ActivationFramework.Current.FindDelegate((int)DelegateType.Deactivate) as Func<long>;
            var res = x.Invoke();

        }


        #endregion

        #region Trial
        private void StartTrailBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DMFrameworkSettings.TrialType = new TrialType() { DMTrialDay = true };

            DMActivationWindow window = new DMActivationWindow(true);
            window.BigIconSource = new BitmapImage(new Uri("pack://application:,,,/Images/icon.png"));
            window.ShowDialog();
        }

        private void GetTrailBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var getTrialDelegate = ActivationFramework.Current.FindDelegate(DelegateType.GetRawTrialValue) as Func<Tuple<long, long>>;
            List<string> infos = new List<string>();

            if (getTrialDelegate != null)
            {
                var trialInfo = getTrialDelegate.Invoke();
                if (trialInfo.Item1 == -1 ||
                    trialInfo.Item2 == -1)
                {
                    ActStatusTextBlock.Text = "Not Started";
                }
                else
                {
                    var TrialPeriod = trialInfo.Item2;
                    var EndValue = trialInfo.Item1;
                    var StartValue = EndValue - TrialPeriod;

                    DateTime startDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(StartValue);
                    infos.Add("Trial Activation Date: " + startDateTime.ToString());

                    DateTime expDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(EndValue);
                    infos.Add("Trial Expire Date: " + expDateTime.ToString());

                    TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                    long currentUnixTime = (long)span.TotalSeconds;

                    long left = EndValue - currentUnixTime;
                    long PassedTrial = TrialPeriod - left;

                    if (PassedTrial >= TrialPeriod || PassedTrial <= 0)
                    {
                        ActStatusTextBlock.Text = "Expired";
                    }
                    else
                    {
                        ActStatusTextBlock.Text = "Active";
                        infos.Add(String.Format("Seconds To Expire: {0}", TrialPeriod - PassedTrial));
                        infos.Add(String.Format("Hours To Expire: {0}", TrialPeriod / 3600 - PassedTrial / 3600));
                        infos.Add(String.Format("Days To Expire: {0}", TrialPeriod / 3600 / 24 - PassedTrial / 3600 / 24));
                    }


                }
            }

            var getTrialRemDelegate = ActivationFramework.Current.FindDelegate(DelegateType.GetTrialTimeRemained) as Func<long>;

            if (getTrialRemDelegate != null)
            {
                var trialInfo = getTrialRemDelegate.Invoke();
                infos.Add(String.Format("Seconds To Expire trial: {0}", trialInfo));

            }


            InfosListBox.ItemsSource = infos;

        }


        #endregion


        #region Update
        private void CheckUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DMUpdatesFrameworkSettings.UpdateArguments = "/silent";
            if (BetaUpdatesCb.IsChecked != null)
                DMUpdatesFrameworkSettings.CheckForBetaUpdates = BetaUpdatesCb.IsChecked.Value;

            if (TestUpdatesCb.IsChecked != null)
                DMUpdatesFrameworkSettings.CheckForTestUpdates = TestUpdatesCb.IsChecked.Value;
            DMUpdatesWindow window = new DMUpdatesWindow();
            window.BigIconSource = new BitmapImage(new Uri("pack://application:,,,/Images/icon.png"));
            window.ShowDialog();
        }
        #endregion
        #endregion


    }
}
