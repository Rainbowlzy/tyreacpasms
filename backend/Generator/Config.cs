﻿namespace Generator
{
    public class Config
    {
        public static string ConnectionString;

        public static string DbDatabase;

        static Config()
        {
            var dbName = "T";

            DbDatabase = dbName;

            ConnectionString = string.Format("server=.;database={0};User ID=sa;Password=Sa123456;", dbName);
        }
    }
}