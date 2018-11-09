using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TEntities.CodeTemplates;

namespace Generator
{
    [Table("TableSchema")]
    public class VTableComments2 : V_Table_Comments, ICloneable
    {
        [Key] public int Id { get; set; }
        public List<V_Column> Columns { get; set; }
        public virtual List<VTableComments2> Children { get; internal set; }
        public VTableComments2 Parent { get; set; }

        public object Clone()
        {
            return new VTableComments2
            {
                Columns = Columns,
                table_name = table_name,
                table_name_ch = table_name_ch,
                table_name_en = table_name_en,
            };
        }
    }
}