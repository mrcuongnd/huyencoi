using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class T_BS_trash_part
    {
        public short[] dpid_s { get; set; }
        public short dino_s { get; set; }
        public short dtmb_s { get; set; }
        public short dclct_s { get; set; }
        public short mpbm_s { get; set; }
        public float smax_f { get; set; }
        public float smin_f { get; set; }
        public short yb04_s { get; set; }
        public T_BS_trash_part()
        {
            dpid_s = new short[3];
            dpid_s[0] = 0;
            dpid_s[1] = 0;
            dpid_s[2] = 0;
            dino_s = 0;
            dtmb_s = 0;
            dclct_s = 0;
            mpbm_s = 0;
            smax_f = 0;
            smin_f = 0;
            yb04_s = 0;
        }
    }
}
