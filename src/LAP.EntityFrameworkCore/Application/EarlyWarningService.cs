using LAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LAP.EntityFrameworkCore.Entity;

namespace LAP.EntityFrameworkCore.Application
{
    public class EarlyWarningService
    {
        private static readonly DapperHelper DapperHelper = new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="searchKey">查询key</param>
        /// <returns></returns>
        public async Task<PagedList<EarlyWarningEntity>> PageQuery(int pageIndex, int pageSize, string searchKey = null)
        {
            --pageIndex;
            var pagedList = new PagedList<EarlyWarningEntity>();

            var parameters = new DynamicParameters();
            parameters.Add("@pageIndex", pageIndex * pageSize);
            parameters.Add("@pageSize", pageSize);

            var sql = @"SELECT `id`, `name`, `host`, `notice_way`, `email`, `mobile`, `principal`, `status`, `created_time` FROM `early_warning` WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                sql += " AND name LIKE CONCAT('%',@searchKey,'%')";
                parameters.Add("@searchKey", searchKey);
            }

            pagedList.total = (await DapperHelper.QueryAsync<EarlyWarningEntity>(sql, parameters)).Count();

            sql += " ORDER BY created_time DESC LIMIT @pageIndex,@pageSize";

            pagedList.dataList = await DapperHelper.QueryAsync<EarlyWarningEntity>(sql, parameters);
            return pagedList;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EarlyWarningEntity>> GetList()
        {
            const string sql = @"SELECT `id`, `name`, `host`, `notice_way`, `email`, `mobile`, `principal`, `status`, `created_time` FROM `early_warning`;";
            return await DapperHelper.QueryAsync<EarlyWarningEntity>(sql);
        }

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<EarlyWarningEntity> Find(int id)
        {
            const string sql = @"SELECT `id`, `name`, `host`, `notice_way`, `email`, `mobile`, `principal`, `status`, `created_time` FROM `early_warning` WHERE id=@id;";
            return await DapperHelper.QueryFirstAsync<EarlyWarningEntity>(sql, new { id });
        }

        /// <summary>
        /// 添加Log
        /// </summary>
        /// <param name="model">Module模型</param>
        /// <returns></returns>
        public async Task<bool> Inster(EarlyWarningEntity model)
        {
            const string sql = @"INSERT INTO `early_warning` (`name`, `host`, `notice_way`, `email`, `mobile`, `principal`,`created_time`) 
                                 VALUES (@name, @host, @notice_way, @email, @mobile, @principal, @created_time);";
            var param = new
            {
                model.name,
                model.host,
                model.notice_way,
                model.email,
                model.mobile,
                model.principal,
                created_time = DateTime.Now
            };
            return await DapperHelper.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> Update(EarlyWarningEntity model)
        {
            const string sql = @"UPDATE `early_warning` SET `name` =@name, `host` =@host, `notice_way` =@notice_way, `email` =@email, `mobile` =@mobile, `principal` =@principal WHERE `id` =@id;";
            var param = new
            {
                model.id,
                model.name,
                model.host,
                model.notice_way,
                model.email,
                model.mobile,
                model.principal
            };
            return await DapperHelper.ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            const string sql = @"DELETE FROM `early_warning` WHERE `id` = @id;";
            return await DapperHelper.ExecuteAsync(sql, new { id });
        }
    }
}
