using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class T_BS_trash
    {
        public short gsts_s { get; set; }
        public short grtype_s { get; set; }
        public string unit_uc { get; set; }
        public string stim_uc { get; set; }
        public short[] yb11_s { get; set; }
        public string yb12_uc { get; set; }
        public string yb13_uc { get; set; }
        public short[] yb14_s { get; set; }
        public string yb15_uc { get; set; }
        public string yb16_uc { get; set; }
        public short pmax_s { get; set; }
        public string sc01_uc { get; set; }
        public string sc02_uc { get; set; }
        public string name_uc { get; set; }
        public short[] yb02_s { get; set; }
        public T_BS_trash_part[] parth { get; set; }

        public T_BS_trash()
        {
            gsts_s = 0;
            grtype_s = 0;
            unit_uc = "";
            stim_uc = "";
            yb11_s = new short[3];
            yb11_s[0] = 0;
            yb11_s[1] = 0;
            yb11_s[2] = 0;
            yb12_uc = "";
            yb13_uc = "";
            yb14_s = new short[3];
            yb14_s[0] = 0;
            yb14_s[1] = 0;
            yb14_s[2] = 0;
            yb15_uc = "";
            yb16_uc = "";
            pmax_s = 0;
            sc01_uc = "";
            sc02_uc = "";
            name_uc = "";
            yb02_s = new short[6];
            for (int i = 0; i < 6; i++)
            {
                yb02_s[i] = 0;
            }
            parth = new T_BS_trash_part[8];
            for (int i = 0; i < 8; i++)
            {
                parth[i] = new T_BS_trash_part();
            }
        }
    }
}
