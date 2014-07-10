using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class OPS9000_T_STATION
    {
        /// <summary>
        /// station no
        /// </summary>
        public int station_no { get; set; }

        /// <summary>
        /// List keiki
        /// </summary>
        public OPS9000_T_BD_kiki[] kiki_lst { get; set; }

        /// <summary>
        /// class Station of OPS9000
        /// </summary>
        public OPS9000_T_STATION() 
        {
            //kiki_lst = new List<OPS9000_T_BD_kiki>();

            kiki_lst = new OPS9000_T_BD_kiki[Constants.CNF_STATION_N + 2];
            for (int i = 0; i < 34; i++)
            {
                kiki_lst[i] = new OPS9000_T_BD_kiki();
            }
        }
    }
}
