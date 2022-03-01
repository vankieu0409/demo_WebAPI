using System.Collections.Generic;

namespace DAL
{
    public class OPTIONS_VALUES
    {
        public int id_Option { get; set; }
        public int id_Values { get; set; }
        public string option_Values { get; set; }
        public bool status_Delete { get; set; }
        public OPTIONS Options { get; set; }
        public virtual ICollection<VARIANTS_VALUES> OptionValueses { get; set; }
    }
}