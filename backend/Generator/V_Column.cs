﻿using System.ComponentModel.DataAnnotations;

namespace Generator
{
    public class V_Column
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string column_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string column_description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dbtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public short max_length { get; set; }

        public string pascal_column_name { get; set; }
    }
}