using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aehyok.Core.Config
{
    public class ConfigurationManager
    {
        #region Initialize

        /// <summary>
        /// Lock to prevent concurrent operations
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// The configuration instance
        /// </summary>
        private static ConfigurationManager _instance = null;

        /// <summary>
        /// The configuration root
        /// </summary>
        private IConfigurationRoot Config { get; }

        /// <summary>
        /// Private ctor
        /// </summary>
        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        /// <summary>
        /// Get configuration instance
        /// </summary>
        /// <returns></returns>
        private static ConfigurationManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                }
            }

            return _instance;
        }

        #endregion Initialize

        #region APIs

        /// <summary>
        /// Get the value of the configuration section
        /// </summary>
        /// <param name="name">The configuration section's name</param>
        /// <returns></returns>
        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

        #endregion APIs
    }
}
