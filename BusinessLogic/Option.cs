using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class Option
    {
        public bool OptimizeSpecialSequencesInTranslation { get; set; }

        public string ConnectionString { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string IdColumn { get; set; }

        public string FromLanguage { get; set; }

        public string ToLanguage { get; set; }
    }
}
