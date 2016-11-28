using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WebserviceDemo
{
    public partial class WebserviceDemo : Form
    {
        Webservice.SDKService sdkService = new global::WebserviceDemo.Webservice.SDKService();
        public WebserviceDemo()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("序列号注册返回结果:" + sdkService.registEx(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtPWD.Text.Trim().ToString()));
            MessageBox.Show("企业信息注册返回结果:" + sdkService.registDetailInfo(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtEName.Text.Trim().ToString(), txtUName.Text.Trim().ToString(), txtTel.Text.Trim().ToString(), txtMobile.Text.Trim().ToString(), txtEmail.Text.Trim().ToString(), txtFax.Text.Trim().ToString(), txtAddress.Text.Trim().ToString(), txtPostcode.Text.Trim().ToString()));
        }

        private void btnUnReg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("序列号注销返回结果:" + sdkService.logout(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString()));
        }

        private void btnReCharge_Click(object sender, EventArgs e)
        {
            MessageBox.Show("序列号充值返回结果:" + sdkService.chargeUp(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtCardNo.Text.Trim().ToString(), txtCardPwd.Text.Trim().ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("序列号密码更改返回结果:" + sdkService.serialPwdUpd(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtNewPwd.Text.Trim().ToString()));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtSmsId.Text.Trim() == "")
            {
                MessageBox.Show("短信发送返回结果:" + sdkService.sendSMS(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtSchTime.Text.Trim().ToString(), txtPhone.Text.Trim().ToString().Split(new char[] { ',' }), txtContent.Text.Trim().ToString(), txtAddSerial.Text.Trim().ToString(), "GBK", 3, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff"))));
            }
            else
            {
                MessageBox.Show("短信发送返回结果:" + sdkService.sendSMS(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtSchTime.Text.Trim().ToString(), txtPhone.Text.Trim().ToString().Split(new char[] { ',' }), txtContent.Text.Trim().ToString(), txtAddSerial.Text.Trim().ToString(), "GBK", 3, Convert.ToInt64(txtSmsId.Text.Trim().ToString())));
            }
        }

        private void txtGetMO_Click(object sender, EventArgs e)
        {
            Webservice.mo[] list = sdkService.getMO(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString());
            if (list != null)
            {
                foreach (Webservice.mo model in list)
                {
                    txtMO.Text += "手机号:" + model.mobileNumber.Trim() + "\t" + "短信内容:" + model.smsContent + "\t" + "接收时间:" + model.sentTime + "\t" + "扩展码:" + model.addSerialRev + "\r\n";
                }
            }
            MessageBox.Show("短信接收完成!");
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sdkService.getVersion());
        }

        private void btnGetBalance_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sdkService.getBalance(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString()).ToString());
        }

        private void btnGetPrice_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sdkService.getEachFee(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString()).ToString());
        }
        /// <summary>
        /// 接受状态报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Webservice.statusReport[] list = sdkService.getReport(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString());
            if (list != null)
            {
                MessageBox.Show("");
            }
            MessageBox.Show("状态报告接受完成");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sdkService.sendVoice(txtSN.Text.Trim().ToString(), txtPWD.Text.Trim().ToString(), txtSchTimeVoice.Text.Trim().ToString(), txtPhoneVoice.Text.Trim().ToString().Split(new char[] { ',' }), txtVoiceCode.Text.Trim().ToString(), null, "GBK", 5, Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssfff"))));
        }
    }
}