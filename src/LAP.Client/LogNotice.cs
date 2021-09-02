using System;
using System.Linq;
using System.Threading.Tasks;
using LAP.EntityFrameworkCore.Application;
using LAP.Common;

namespace LAP.Client
{
    public class LogNotice
    {
        private static readonly ModuleService ModuleService = new();

        public static async Task Notice(int moduleCode, int logLevel)
        {
            var key = $"{moduleCode}_{logLevel}";
            var getCount = Redis.Cli.Get<int>(key);
            if (getCount > 100) return;

            var module = await ModuleService.Find(moduleCode);
            if (module == null) return;

            if (module.is_notice && module.log_level != null)
            {
                var moduleLogLevelList = module.log_level?.Split(',');
                if (!moduleLogLevelList.Contains(logLevel.ToString())) return;

                var sendText = $"项目：{module.name} 有产生错误日志，请及时处理。";
                switch (module.notice_way)
                {
                    case 1:
                        // 邮件通知
                        await MailHelper.SendMail(module.email, sendText, "错误日志通知");
                        Redis.Cli.Set(key, getCount + 1, TimeSpan.FromDays(1));
                        break;
                    case 2:
                        // 短信通知
                        break;
                }
            }
        }
    }
}
