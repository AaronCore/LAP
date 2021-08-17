using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.Server.Common;
using Quartz;
using Quartz.Impl;

namespace LAP.Server
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class JobTool
    {
        /// <summary>
        /// 任务调度的使用过程
        /// </summary>
        /// <returns></returns>
        public static async Task Run()
        {
            // 1.创建scheduler的引用
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = await schedFact.GetScheduler();

            //2.启动 scheduler
            await sched.Start();

            // 3.创建 job
            IJobDetail job = JobBuilder.Create<EarlyWarningJob>()
                .WithIdentity("LAP.EarlyWarning.Job", "LAP.EarlyWarning.Group")
                .Build();

            // 4.创建 trigger （创建 trigger 触发器）
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("LAP.EarlyWarning.Trigger", "LAP.EarlyWarning.Group")  //触发器 组
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                .Build();

            // 5.使用trigger规划执行任务job （使用触发器规划执行任务）
            await sched.ScheduleJob(job, trigger);
        }
    }
    /// <summary>
    /// 实现IJob,Execute方法里编写要处理的业务逻辑
    /// </summary>
    public class EarlyWarningJob : IJob
    {
        private static readonly EarlyWarningService EarlyWarningService = new();

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"EarlyWarningJob执行工作,在当前时间{DateTime.Now}---上一次执行时间：{DateTime.Now.AddMinutes(-5)}.");

            Ping ping = new Ping();
            var list = await EarlyWarningService.GetList();
            foreach (var item in list)
            {
                try
                {
                    var host = item.host;
                    host = host.Replace("https://", "http://");
                    host = host.StartsWith("http://") ? host : $"http://{host}";
                    host = new Uri(host).DnsSafeHost;

                    var status = ping.Send(host)?.Status;
                    if (status == IPStatus.Success)
                    {
                        await EarlyWarningService.UpdateStatusAndRemark(item.id, (int)IPStatus.Success, null);
                    }
                    else
                    {
                        if (status != null)
                        {
                            await EarlyWarningService.UpdateStatusAndRemark(item.id, (int)status, status.ToString());

                            switch (item.notice_way)
                            {
                                case 1:
                                    // 邮件通知
                                    await Task.Run(() => { MailHelper.SendMail(item.email, item.host); });
                                    break;
                                case 2:
                                    // 短信通知
                                    break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    var remark = e.InnerException?.Message;
                    await EarlyWarningService.UpdateStatusAndRemark(item.id, (int)IPStatus.Unknown, remark);

                    switch (item.notice_way)
                    {
                        case 1:
                            // 邮件通知
                            await Task.Run(() => { MailHelper.SendMail(item.email, item.host); });
                            break;
                        case 2:
                            // 短信通知
                            break;
                    }
                }
            }
        }
    }
}
