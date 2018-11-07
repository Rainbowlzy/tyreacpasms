using System.Collections.Generic;

namespace TEntities.CodeTemplates
{
    public class DropdownDef
    {
        public string tableName { get; set; }
        public string tableNameEn { get; set; }
        public string fieldName { get; set; }
        public string fieldNameEn { get; set; }
        public List<string> vals { get; set; }
        public List<string> buttons { get; set; }
    }
}