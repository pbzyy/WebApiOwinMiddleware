﻿namespace WebApiOwinMiddleware.Configuration
{
    public static class SettingsProvider
    {
        public static bool TokenHeaderFilteringEnabled => bool.Parse(System.Configuration.ConfigurationManager.AppSettings["TokenHeaderFilteringEnabled"]);

        public static string TokenHeaderName => System.Configuration.ConfigurationManager.AppSettings["TokenHeaderName"];

        public static string TokenHeaderValue => System.Configuration.ConfigurationManager.AppSettings["TokenHeaderValue"];

        public static string ApiUserName => System.Configuration.ConfigurationManager.AppSettings["ApiUserName"];

        public static string ApiPassword => System.Configuration.ConfigurationManager.AppSettings["ApiPassword"];
    }
}