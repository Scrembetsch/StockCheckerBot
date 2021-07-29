using System;
using System.Collections.Generic;
using System.Configuration;

namespace StockCheckerBot.Config
{
    public class BaseCheckerConfigSection : ConfigurationSection
    {
        #region Attribute Names
        private static readonly string s_CheckInterval = "CheckInterval";
        #endregion

        #region Configuration Properties
        private static readonly ConfigurationProperty _CheckInterval =
            new ConfigurationProperty(
                s_CheckInterval
                , typeof(int)
                , 30
                , ConfigurationPropertyOptions.None
            );
        #endregion

        protected ConfigurationPropertyCollection _Properties;

        public BaseCheckerConfigSection()
        {
            _Properties = new ConfigurationPropertyCollection();

            _Properties.Add(_CheckInterval);
        }

        #region Properties
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }

        [IntegerValidator(MinValue = 1, MaxValue = 300, ExcludeRange = false)]
        public int CheckInterval
        {
            get
            {
                return (int)this[s_CheckInterval];
            }
            set
            {
                this[s_CheckInterval] = value;
            }
        }
        #endregion
    }
}
