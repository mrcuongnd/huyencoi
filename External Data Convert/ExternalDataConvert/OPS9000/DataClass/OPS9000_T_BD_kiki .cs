using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class OPS9000_T_BD_kiki
    {
        public OPS9000_T_BD_KIKI_KEIKI[] keiki { get; set; }
        public OPS9000_T_BD_kiki()
        {
            keiki = new OPS9000_T_BD_KIKI_KEIKI[Constants.CNF_KEIKI_N + 1];
            for (int i = 0; i < 513; i++)
            {
                keiki[i] = new OPS9000_T_BD_KIKI_KEIKI();
            }
        }
    }
}
