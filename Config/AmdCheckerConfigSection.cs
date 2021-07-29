using System;
using System.Collections.Generic;
using System.Configuration;

namespace StockCheckerBot.Config
{
    public class AmdCheckerConfigSection : BaseCheckerConfigSection
    {
        #region Attribute Names
        private static readonly string s_CheckBaseUrl = "checkBaseUrl";
        private static readonly string s_OpenBaseUrl = "openBaseUrl";
        #endregion

        #region Configuration Properties
        private static readonly ConfigurationProperty _CheckBaseUrl =
            new ConfigurationProperty(
                s_CheckBaseUrl
                , typeof(string)
                , ""
                , ConfigurationPropertyOptions.None
            );

        private static readonly ConfigurationProperty _OpenBaseUrl =
            new ConfigurationProperty(
                s_OpenBaseUrl
                , typeof(string)
                , ""
                , ConfigurationPropertyOptions.None
            );
        #endregion

        public AmdCheckerConfigSection() : base()
        {
            _Properties.Add(_CheckBaseUrl);
            _Properties.Add(_OpenBaseUrl);
        }

        #region Properties
        public string CheckBaseUrl
        {
            get
            {
                return (string)this[s_CheckBaseUrl];
            }
            set
            {
                this[s_CheckBaseUrl] = value;
            }
        }

        public string OpenBaseUrl
        {
            get
            {
                return (string)this[s_OpenBaseUrl];
            }
            set
            {
                this[s_OpenBaseUrl] = value;
            }
        }
        #endregion
    }
}
