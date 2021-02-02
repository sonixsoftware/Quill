using System.Configuration;

namespace QuillDigital
{
    internal class GetConfiguration
    {

        public static string GetConfigurationValueClientID()

        {
            string id = ConfigurationManager.AppSettings["Client_ID"];

            return id;
        }

        public static string GetConfigurationValueMeta()

        {
            string id = ConfigurationManager.AppSettings["Meta_ID"];

            return id;
        }

        public static string GetConfigurationValueDPI()

        {
            string id = ConfigurationManager.AppSettings["DPI_ID"];

            return id;
        }

        public static string GetConfigurationValueOCR()

        {
            string id = ConfigurationManager.AppSettings["OCR_ID"];

            return id;
        }
        public static string GetConfigurationValueClauses()

        {
            string id = ConfigurationManager.AppSettings["ClausesToExtract"];

            return id;
        }
        public static string GetConfigurationValueFields()

        {
            string id = ConfigurationManager.AppSettings["FieldsToExtract"];

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

        public static string GetConfigurationValueGrayScale()

        {
            string id = ConfigurationManager.AppSettings["Gray_Scale"];

            return id;
        }

        public static void ConfigurationValueGrayScale(string GrayScale)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["Gray_Scale"].Value = GrayScale;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ConfigurationValueMeta(string MetaID)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["Meta_ID"].Value = MetaID;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ConfigurationValueDPI(string DPIID)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["DPI_ID"].Value = DPIID;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ConfigurationValueOCR(string OCRID)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["OCR_ID"].Value = OCRID;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ConfigurationValueClauses(string ClausesToExtract)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["ClausesToExtract"].Value = ClausesToExtract;

            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void ConfigurationValueFields(string FieldsToExtract)

        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            configuration.AppSettings.Settings["FieldsToExtract"].Value = FieldsToExtract;

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
