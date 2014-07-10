using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class T_BU_trhis
    {
        public short newp_s { get; set; }
        public short oldp_s { get; set; }
        public string ntim_t { get; set; }
        public string dtim_t { get; set; }
        public short[] yb01_s { get; set; }
        public T_BU_trhis()
        {
            newp_s = 0;
            oldp_s = 0;
            ntim_t = "";
            dtim_t = "";
            for (int i = 0; i < 10; i++) 
            {
                yb01_s[i] = 0;
            }
        }
    }
}
