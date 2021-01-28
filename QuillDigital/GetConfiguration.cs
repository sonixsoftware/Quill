﻿using System.Configuration;

namespace QuillDigital
{
    internal class GetConfiguration
    {

        public static string GetConfigurationValueClientID()

        {
            string id = ConfigurationManager.AppSettings["Client_ID"];

            return id;
        }

        public static string GetConfigurationValueSaveLocation()

        {
            string id = ConfigurationManager.AppSettings["Save_Folder"];

            return id;
        }

        public static void ConfigurationValueClientID(string ClientID)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["Client_ID"].Value = ClientID;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ConfigurationValueSaveLocation(string SaveLocation)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["Save_Folder"].Value = SaveLocation;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}