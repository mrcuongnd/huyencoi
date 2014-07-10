using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataConvert.OPS9000.DataClass
{
    class OPS9000_T_BD_KIKI_KEIKI
    {
        public string tgno_c { get; set; }
        public short yobi1_s { get; set; }
        public string tgnm_c { get; set; }
        public short yobi2_s { get; set; }
        //public string[,] onnm_c { get; set; }
        //public string[,] ofnm_c { get; set; }
        //public string[,] mvnm_c { get; set; }
        public string onnm_c { get; set; }
        public string ofnm_c { get; set; }
        public string mvnm_c { get; set; }

        public int rofg_i { get; set; }
        public short fid_s { get; set; }
        public string ifg_uc { get; set; }
        public string ofg_uc { get; set; }
        public string pntfg_uc { get; set; }
        public string modfg_uc { get; set; }
        public string onfg_uc { get; set; }
        public string offfg_uc { get; set; }
        public string lev_uc { get; set; }
        public string blk_uc { get; set; }
        public short[] voice_s { get; set; }
        public short[] guid_s { get; set; }
        public string way_uc { get; set; }
        public string amod_uc { get; set; }
        public short[] llid_s { get; set; }
        public string lsid_uc { get; set; }
        public string lpnt_uc { get; set; }
        public string pamid_uc { get; set; }
        public string mamid_uc { get; set; }
        public string move_uc { get; set; }
        public short cdmd_s { get; set; }
        public short gdid_s { get; set; }
        public short sdid_s { get; set; }
        public short tdid_s { get; set; }
        public short[,] alid_s { get; set; }
        public string asid_uc { get; set; }
        public string apnt_uc { get; set; }
        public short[] acnt_s { get; set; }

        public OPS9000_T_BD_KIKI_KEIKI()
        {
            tgno_c = "";
            yobi1_s = 0;
            tgnm_c = "";
            yobi2_s = 0;
            //onnm_c = new string[8, 18];
            //ofnm_c = new string[8, 18];
            //mvnm_c = new string[8, 14];
            onnm_c = "";
            ofnm_c = "";
            mvnm_c = "";
            rofg_i = 0;
            fid_s = 0;
            ifg_uc = "";
            ofg_uc = "";
            pntfg_uc = "";
            modfg_uc = "";
            onfg_uc = "";
            offfg_uc = "";
            lev_uc = "";
            blk_uc = "";
            voice_s = new short[8];
            guid_s = new short[8];
            way_uc = "";
            amod_uc = "";
            llid_s = new short[2];
            lsid_uc = "";
            lpnt_uc = "";
            pamid_uc = "";
            mamid_uc = "";
            move_uc = "";
            cdmd_s = 0;
            gdid_s = 0;
            sdid_s = 0;
            tdid_s = 0;
            alid_s = new short[8, 2];
            asid_uc = "";
            apnt_uc = "";
            acnt_s = new short[8];
        }
    }
}
