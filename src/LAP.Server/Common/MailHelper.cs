using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;

namespace LAP.Server.Common
{
    public class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="host">主机地址</param>
        public static void SendMail(string email, string host)
        {
            var receiverAccountList = email.Split(',').Where(item => !string.IsNullOrWhiteSpace(item)).ToList();
            var sendText = $"主机：{host} 发生故障，请及时处理。";
            SendMail("LAP日志分析系统", "aaroncnc@qq.com", "smtp.qq.com", 25, "客户端授权码", receiverAccountList, "LAP预警通知", null, sendText, null);
        }
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="sendName">发送者名称</param>
        /// <param name="sendAccountName">发送者账号</param>
        /// <param name="smtpHost">发送者服务器地址：例如：smtp.163.com</param>
        /// <param name="smtpPort">服务器端口号：例如：25</param>
        /// <param name="authenticatePassword">发送者登录邮箱账号的客户端授权码</param>
        /// <param name="receiverAccountNameList">接收者账号</param>
        /// <param name="mailSubject">邮件主题</param>
        /// <param name="sendHtml">文本html(与sendText参数互斥，传此值则 sendText传null)</param>
        /// <param name="sendText">纯文本(与sendHtml参数互斥，传此值则 sendHtml传null)</param>
        /// <param name="accessoryList">邮件的附件</param>
        public static void SendMail(string sendName, string sendAccountName, string smtpHost, int smtpPort, string authenticatePassword, List<string> receiverAccountNameList, string mailSubject, string sendHtml, string sendText, List<MimePart> accessoryList = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(sendName, sendAccountName));
            var mailboxAddressList = new List<MailboxAddress>();
            receiverAccountNameList.ForEach(f =>
            {
                mailboxAddressList.Add(new MailboxAddress(f));
            });
            message.To.AddRange(mailboxAddressList);

            message.Subject = mailSubject;

            var alternative = new Multipart("alternative");
            if (!string.IsNullOrWhiteSpace(sendText))
            {
                alternative.Add(new TextPart("plain")
                {
                    Text = sendText
                });
            }

            if (!string.IsNullOrWhiteSpace(sendHtml))
            {
                alternative.Add(new TextPart("html")
                {
                    Text = sendHtml
                });
            }

            var multipart = new Multipart("mixed");
            multipart.Add(alternative);
            if (accessoryList != null)
            {
                accessoryList?.ForEach(f =>
                {
                    multipart.Add(f);
                });

            }
            message.Body = multipart;
            using (var client = new SmtpClient())
            {
                client.Connect(smtpHost, smtpPort, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(sendAccountName, authenticatePassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
