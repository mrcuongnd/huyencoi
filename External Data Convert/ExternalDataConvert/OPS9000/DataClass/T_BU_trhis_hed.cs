using OPS5000Retrofit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class T_BU_trhis_hed
    {
        public T_BU_trhis bhed { get; set; }
        public short[] osts_s { get; set; }
        public short[] yb03_s { get; set; }
        public float[] obnk_f { get; set; }
        public float[,] dbnk_f { get; set; }
        public short[] yb04_s { get; set; }
        public T_BU_trhis_hed()
        {
            bhed = new T_BU_trhis();

            for (int i = 0; i < Constants.CNF_TRD_PEN_N; i++)
            {
                osts_s[i] = 0;
            }
            for (int i = 0; i < 8; i++)
            {
                yb03_s[i] = 0;
            }
            for (int i = 0; i < Constants.CNF_TRD_PEN_N; i++)
            {
                obnk_f[i] = 0;
            }
            for (int j = 0; j < Constants.CNF_TRD_SPH_N; j++)
            {
                for (int i = 0; i < Constants.CNF_TRD_PEN_N; i++)
                {
                    dbnk_f[j, i] = 0;
                }
            }
            for (int i = 0; i < 16; i++)
            {
                yb04_s[i] = 0;
            }

        }
    }
}
