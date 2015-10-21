using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Datalogic.API;

namespace DatalogicSjorpioLaserTesting
{
    public partial class FormLaser : Form
    {
        private Datalogic.API.DecodeEvent dcdEvent;
        private Datalogic.API.DecodeHandle hDcd;
        private Datalogic.API.DecodeRequest reqType = (Datalogic.API.DecodeRequest)1 | Datalogic.API.DecodeRequest.PostRecurring;

        public FormLaser()
        {
            InitializeComponent();
            hDcd = new DecodeHandle(DecodeDeviceCap.Exists | DecodeDeviceCap.Barcode);
            hDcd.SoftTrigger(DecodeInputType.Barcode, 60000);

            Log("[BEGIN] Starting laser with 60 sec timeout." + Environment.NewLine);

            dcdEvent = new DecodeEvent(hDcd, reqType, this);
            dcdEvent.TimeOut += new DecodeTimeOut(dcdEvent_Timeout);
        }
        private void dcdEvent_Timeout(object sender, Datalogic.API.DecodeEventArgs e)
        {
            hDcd.SoftTrigger(DecodeInputType.Barcode, 60000);
            Log("[TIMEOUT] Restarting laser" + Environment.NewLine);
        }

        private void Log(string text)
        {
            logBox.Text += text;
            logBox.Select(logBox.TextLength, 0);
            logBox.ScrollToCaret();
        }
    }
}