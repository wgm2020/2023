using redisTest.Comm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redisTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RedisHandle redisHandle = new RedisHandle("assembly://redisTest/RedisConfig/Redis_Main.config");
            //string rs = redisHandle.GetValue("TEST");
            string rs = redisHandle.GetValue("k1");
        }
    }
}
