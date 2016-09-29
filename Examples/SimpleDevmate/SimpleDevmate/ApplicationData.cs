using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DevMateKit.DMFeedback.API;
using DevMateKit.DMFramework;
using DevMateKit.DMUpdates.API;

namespace SimpleDevmate
{
    class ApplicationData
    {
        public static void SetApplicationData()
        {
            SetRsa();
            SetDsa();
            SetLogFilePath();
            SetAdditionalInformation();
            SetApplicationSettings();
        }
        private static void SetLogFilePath()
        {
            DMFeedbackFrameworkSettings.LogFilePath = @"Logs\logfile.log";
        }
        private static void SetAdditionalInformation()
        {
            DMFeedbackFrameworkSettings.AdditionalInformation = "Version with hidden feature";
        }
        private static void SetApplicationSettings()
        {
            TrySetApplicationSetting("Language", "en");
            TrySetApplicationSetting("AppRunCount", 0);
            TrySetApplicationSetting("Setting1", "Value1");
            TrySetApplicationSetting("Setting2", "Value2");
        }
        private static void SetRsa()
        {
            #region RSA
            DMFrameworkSettings.PublicRSAKey =
              @"-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAzRWGeVD5D1FDuRhpSKzU
P19hTLUNE0AHZHjdU05AZOUa8gxlCfMSHjsuM95dtk2Z+Pgd9Vq1OTGrYhav0jZP
xUMdtHcinyAnXjAz2hgSSb3/1qrXPxaSoG05x8LPTEN5RYxal6PorRDGOAvG8sgN
/nHIBmylZMpQOegWo8JppIpKHEfii23JcGuXkoo/oen33TQq5y/HhWWRKfxVbnGx
RnD25WAOcWpvg+H2gWVrfLwRAu9GK0yGBhOhbgKsfO056NAwfM02HhRAtwfC7Xhc
ID6kPWp/qThVLSfGhwXkUNwAxfCHh/5dH2rF/xfCAhvfBhxLYx7Njg3Ana/Ybo1L
5wIDAQAB
-----END PUBLIC KEY-----";
            #endregion
        }

        private static void SetDsa()
        {
            #region RSA
            DMUpdatesFrameworkSettings.PublicDSAKey =
              @"-----BEGIN PUBLIC KEY-----
MIIBtzCCASsGByqGSM44BAEwggEeAoGBAKjVCtuMMoFVi7Q00VrVRQgYn8By/5R3
QA8p1oj6pF8rpmgTda73glBX8sj/oQ5566zJDuPdjivrrNUz+8jad9X/Z/julm55
yF0OnUAk8eJeo3XXcijOo8MvtUYf/nqefRCDsFwMU/AND6WybARrdLPEFk1JOrbT
BIKFmNc2AIYrAhUAliHC+zopsyZ8pttPTt+lISLfEQkCgYAYDXQ1yrkdvR5gdh0I
FHRF7A7//9gSMtOJgh3JdvAmngUFSx7Ghv79ZP8t3jyHFbl3ZDPJyMRZn+CRZADf
EVHJMjUXQhNDFiTPyex/+0t3oEhUkM+YPHdAvAkXDyix4IpsWDeLU1UcZBwQ3IpP
ON/IyXb7qvYv+s25FGVslmX25gOBhQACgYEApj4JFZXaMEKVHpmLXpA0xy29En5D
RTTqNOxICOClmTRLHlVmm72zjDxTsCZhMDIENrXd7zG0h/8TY5rE0t7hme2Iwl3x
EIsiakOXv4E+UkSlGf88J2CFYJlfKCNtsd/TSyE744XOMCuEI5hEaWeK4i03p8dP
oekgW9GE29FPIXY=
-----END PUBLIC KEY-----
";
            #endregion
        }

        private static void TrySetApplicationSetting(string name, object value)
        {
            if (DMFeedbackFrameworkSettings.ApplicationSettings.ContainsKey(name))
                DMFeedbackFrameworkSettings.ApplicationSettings[name] = value;
            else
                DMFeedbackFrameworkSettings.ApplicationSettings.Add(name, value);

        }
    }
}
