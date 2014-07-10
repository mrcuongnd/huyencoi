using ExternalDataConvert.OPS9000.DataClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExternalDataConvert
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }



        private void btnSetting_Click(object sender, EventArgs e)
        {
            ReadData1();
        }

        public void ReadData1()
        {
            string pathfile = @"D:\Working\dotnet project\ConvertData\SVN\trunk\doc\reference\data\OPS9000\savedata\savedata\TREND\t2014051611.trd";
            T_BS_trash[] indexData = new T_BS_trash[481];
            byte[] OPSBDkesoByteArr = File.ReadAllBytes(pathfile);
            int menSize = 256;
            using (FileStream fs = new FileStream(pathfile, FileMode.Open, FileAccess.Read))
            {
                for (int menNo = 0; menNo < 481; menNo++)
                {
                    int startIndex = menNo * menSize;
                    indexData[menNo] = new T_BS_trash();
                    fs.Seek(startIndex, SeekOrigin.Begin);
                    byte[] b = new byte[256];
                    fs.Read(b, 0, 256);
                    int i = 0;
                    indexData[menNo].gsts_s = BitConverter.ToInt16(b, i);
                    indexData[menNo].grtype_s = BitConverter.ToInt16(b, i += 2);
                    indexData[menNo].unit_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 2, 1);
                    indexData[menNo].stim_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 1, 1);
                    indexData[menNo].yb11_s[0] = BitConverter.ToInt16(b, i += 1);
                    indexData[menNo].yb11_s[1] = BitConverter.ToInt16(b, i += 2);
                    indexData[menNo].yb11_s[2] = BitConverter.ToInt16(b, i += 2);
                    indexData[menNo].yb12_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 2, 1);
                    indexData[menNo].yb13_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 1, 1);
                    indexData[menNo].yb14_s[0] = BitConverter.ToInt16(b, i += 1);
                    indexData[menNo].yb14_s[1] = BitConverter.ToInt16(b, i += 2);
                    indexData[menNo].yb14_s[2] = BitConverter.ToInt16(b, i += 2);
                    indexData[menNo].yb15_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 2, 1);
                    indexData[menNo].yb16_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 1, 1);
                    indexData[menNo].pmax_s = BitConverter.ToInt16(b, i += 1);
                    indexData[menNo].sc01_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 2, 1);
                    indexData[menNo].sc02_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 1, 1);
                    indexData[menNo].name_uc = System.Text.Encoding.GetEncoding(932).GetString(b, i += 1, 26);
                    indexData[menNo].yb02_s[0] = BitConverter.ToInt16(b, i += 1);
                    for (int j = 1; j < 6; j++)
                    {
                        indexData[menNo].yb02_s[j] = BitConverter.ToInt16(b, i += 2);
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        indexData[menNo].parth[j].dpid_s[0] = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].dpid_s[1] = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].dpid_s[2] = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].dino_s = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].dtmb_s = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].dclct_s = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].mpbm_s = BitConverter.ToInt16(b, i += 2);
                        indexData[menNo].parth[j].smax_f = BitConverter.ToInt32(b, i += 2);
                        indexData[menNo].parth[j].smin_f = BitConverter.ToInt32(b, i += 4);
                        indexData[menNo].parth[j].yb04_s = BitConverter.ToInt16(b, i += 4);
                    }
                }
            }
            MessageBox.Show("read success");
        }

        public void ReadData(string ops9KHSTMFilePath)
        {
            string pathfile = @"D:\Working\dotnet project\ConvertData\OPS9000\load\OPSBDkiki";
            OPS9000_T_BD_kiki indexData = new OPS9000_T_BD_kiki();
            OPS9000_T_STATION stationdata = new OPS9000_T_STATION();
            byte[] OPSBDkesoByteArr = File.ReadAllBytes(pathfile);
            // 26624 = (192 + 10 + 3 + 3) * 128
            int partSize = 604;
            int menSize = 309852;


            using (FileStream fs = new FileStream(pathfile, FileMode.Open, FileAccess.Read))
            {
                for (int menNo = 0; menNo < 34; menNo++)
                {
                    for (int partNo = 0; partNo < 513; partNo++)
                    {
                        int startIndex = menNo * menSize + partNo * partSize;
                        fs.Seek(startIndex, SeekOrigin.Begin);
                        byte[] b = new byte[604];
                        fs.Read(b, 0, 604);

                        //OPS9000_T_CHOMB_KIJ temp = new OPS9000_T_CHOMB_KIJ();
                        //temp.lng_s = BitConverter.ToInt16(b, 0);
                        //temp.yobi_s = BitConverter.ToInt16(b, 2);
                        //temp.kij_c = BitConverter.ToString(b, 4);

                        //txtYobi.Text = BitConverter.ToInt16(b, 2).ToString();
                        //string s;
                        //s = Encoding.GetEncoding("shift_jis").GetString(b, 4, 124);
                        //txtKij_c.Text = s;
                        if (BitConverter.ToInt16(b, 0).ToString().Trim() != null)
                        {
                            stationdata.kiki_lst[menNo].keiki[partNo].tgno_c = BitConverter.ToString(b, 0);
                            stationdata.kiki_lst[menNo].keiki[partNo].yobi1_s = BitConverter.ToInt16(b, 10);
                            stationdata.kiki_lst[menNo].keiki[partNo].tgnm_c = BitConverter.ToString(b, 12);
                            stationdata.kiki_lst[menNo].keiki[partNo].yobi2_s = BitConverter.ToInt16(b, 38);
                            //stationdata.kiki_lst[menNo].keiki[partNo].onnm_c = BitConverter.ToInt16(b, 40).ToString().ToStringArray();
                            stationdata.kiki_lst[menNo].keiki[partNo].onnm_c = BitConverter.ToString(b, 40, 144);

                            //stationdata.kiki_lst[menNo].keiki[partNo].ofnm_c = BitConverter.ToInt16(b, 184).ToString().ToStringArray();
                            stationdata.kiki_lst[menNo].keiki[partNo].ofnm_c = BitConverter.ToString(b, 184, 144);
                            //stationdata.kiki_lst[menNo].keiki[partNo].mvnm_c = BitConverter.ToInt16(b, 328).ToString().ToStringArray();
                            stationdata.kiki_lst[menNo].keiki[partNo].mvnm_c = BitConverter.ToString(b, 328, 112);
                            stationdata.kiki_lst[menNo].keiki[partNo].rofg_i = BitConverter.ToInt32(b, 440);
                            stationdata.kiki_lst[menNo].keiki[partNo].fid_s = BitConverter.ToInt16(b, 444);
                            stationdata.kiki_lst[menNo].keiki[partNo].ifg_uc = BitConverter.ToString(b, 446);
                            stationdata.kiki_lst[menNo].keiki[partNo].ofg_uc = BitConverter.ToString(b, 447);
                            stationdata.kiki_lst[menNo].keiki[partNo].pntfg_uc = BitConverter.ToString(b, 448);
                            stationdata.kiki_lst[menNo].keiki[partNo].modfg_uc = BitConverter.ToString(b, 449);
                            stationdata.kiki_lst[menNo].keiki[partNo].onfg_uc = BitConverter.ToString(b, 450);
                            stationdata.kiki_lst[menNo].keiki[partNo].offfg_uc = BitConverter.ToString(b, 451);
                            stationdata.kiki_lst[menNo].keiki[partNo].lev_uc = BitConverter.ToString(b, 452, 1);
                            stationdata.kiki_lst[menNo].keiki[partNo].blk_uc = BitConverter.ToString(b, 460, 1);
                            for (int i = 0; i < 8; i++)
                                stationdata.kiki_lst[menNo].keiki[partNo].voice_s[i] = BitConverter.ToInt16(b, 468 + i * 2);
                            for (int i = 0; i < 8; i++)
                                stationdata.kiki_lst[menNo].keiki[partNo].guid_s[i] = BitConverter.ToInt16(b, 484 + i * 2);

                            stationdata.kiki_lst[menNo].keiki[partNo].way_uc = BitConverter.ToString(b, 500, 1);
                            stationdata.kiki_lst[menNo].keiki[partNo].amod_uc = BitConverter.ToString(b, 508, 1);
                            stationdata.kiki_lst[menNo].keiki[partNo].llid_s[0] = BitConverter.ToInt16(b, 516);
                            stationdata.kiki_lst[menNo].keiki[partNo].llid_s[1] = BitConverter.ToInt16(b, 518);
                            stationdata.kiki_lst[menNo].keiki[partNo].lsid_uc = BitConverter.ToString(b, 520);
                            stationdata.kiki_lst[menNo].keiki[partNo].lpnt_uc = BitConverter.ToString(b, 521);
                            stationdata.kiki_lst[menNo].keiki[partNo].pamid_uc = BitConverter.ToString(b, 522);
                            stationdata.kiki_lst[menNo].keiki[partNo].mamid_uc = BitConverter.ToString(b, 530);
                            stationdata.kiki_lst[menNo].keiki[partNo].move_uc = BitConverter.ToString(b, 531);
                            stationdata.kiki_lst[menNo].keiki[partNo].cdmd_s = BitConverter.ToInt16(b, 532);
                            stationdata.kiki_lst[menNo].keiki[partNo].gdid_s = BitConverter.ToInt16(b, 534);
                            stationdata.kiki_lst[menNo].keiki[partNo].sdid_s = BitConverter.ToInt16(b, 536);
                            stationdata.kiki_lst[menNo].keiki[partNo].tdid_s = BitConverter.ToInt16(b, 538);
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    stationdata.kiki_lst[menNo].keiki[partNo].alid_s[i, j] = BitConverter.ToInt16(b, 540 + i * 2 + j * 2);
                                }
                            }
                            stationdata.kiki_lst[menNo].keiki[partNo].asid_uc = BitConverter.ToString(b, 572);
                            stationdata.kiki_lst[menNo].keiki[partNo].apnt_uc = BitConverter.ToString(b, 580);
                            for (int i = 0; i < 8; i++)
                                stationdata.kiki_lst[menNo].keiki[partNo].acnt_s[i] = BitConverter.ToInt16(b, 588);


                        }


                    }
                }

            }
            MessageBox.Show("convert success");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowserSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtBrowserSource.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnBrowserDes_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtBrowserDes.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}

