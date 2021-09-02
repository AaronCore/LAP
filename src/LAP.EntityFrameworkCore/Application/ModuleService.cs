using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LAP.Common;
using LAP.EntityFrameworkCore.Entity;

namespace LAP.EntityFrameworkCore.Application
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class ModuleService
    {
        private static readonly DapperHelper DapperHelper = new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">分页下标</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="searchKey">查询key</param>
        /// <returns></returns>
        public async Task<PagedList<ModuleEntity>> PageQuery(int pageIndex, int pageSize, string searchKey = null)
        {
            --pageIndex;
            var pagedList = new PagedList<ModuleEntity>();

            var parameters = new DynamicParameters();
            parameters.Add("@pageIndex", pageIndex * pageSize);
            parameters.Add("@pageSize", pageSize);

            var sql = @"SELECT `id`, `name`, `code`, `is_notice`, `log_level`, `notice_way`, `email`, `mobile`, `created_by`, `created_time` FROM `modules` WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                sql += " AND name LIKE CONCAT('%',@searchKey,'%')";
                parameters.Add("@searchKey", searchKey);
            }

            pagedList.total = (await DapperHelper.QueryAsync<ModuleEntity>(sql, parameters)).Count();

            sql += " ORDER BY created_time DESC LIMIT @pageIndex,@pageSize";

            pagedList.dataList = await DapperHelper.QueryAsync<ModuleEntity>(sql, parameters);
            return pagedList;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleEntity>> GetList()
        {
            const string sql = @"SELECT `id`, `name`, `code`, `is_notice`, `log_level`, `notice_way`, `email`, `mobile`, `created_by`, `created_time` FROM `modules`;";
            return await DapperHelper.QueryAsync<ModuleEntity>(sql);
        }

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<ModuleEntity> Find(int id)
        {
            const string sql = @"SELECT `id`, `name`, `code`, `is_notice`, `log_level`, `notice_way`, `email`, `mobile`, `created_by`, `created_time` FROM `modules` WHERE id=@id;";
            return await DapperHelper.QueryFirstAsync<ModuleEntity>(sql, new { id });
        }
        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="code">模块code</param>
        /// <returns></returns>
        public async Task<ModuleEntity> Find(string code)
        {
            const string sql = @"SELECT `id`, `name`, `code`, `is_notice`, `log_level`, `notice_way`, `email`, `mobile`, `created_by`, `created_time` FROM `modules` WHERE code=@code;";
            return await DapperHelper.QueryFirstAsync<ModuleEntity>(sql, new { code });
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> Inster(ModuleEntity model)
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    const string sql = @"INSERT INTO `modules` (`name`, `code`, `is_notice`, `log_level`, `notice_way`, `email`, `mobile`, `created_by`, `created_time`)
                                         VALUES (@name, @code,@is_notice,@log_level,@notice_way,@email,@mobile,@created_by, @created_time);";

                    var code = await conn.ExecuteScalarAsync<int>("SELECT IFNULL(MAX(id),0)+1 AS 'max_id' FROM modules;");
                    var param = new
                    {
                        model.name,
                        code,
                        model.is_notice,
                        model.log_level,
                        model.notice_way,
                        model.email,
                        model.mobile,
                        model.created_by,
                        created_time = DateTime.Now
                    };
                    await conn.ExecuteAsync(sql, param);
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    transaction.Dispose();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public async Task<bool> Update(ModuleEntity model)
        {
            const string sql = @"UPDATE `modules` SET `name` = @name,`is_notice` =@is_notice, `log_level` =@log_level, `notice_way` =@notice_way, `email` =@email, `mobile` =@mobile WHERE `id` = @id;";
            var param = new
            {
                model.id,
                model.name,
                model.is_notice,
                model.log_level,
                model.notice_way,
                model.email,
                model.mobile,
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
            const string sql = @"DELETE FROM `modules` WHERE `id` = @id;";
            return await DapperHelper.ExecuteAsync(sql, new { id });
        }

        /// <summary>
        /// 名称验证
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="name">模块名称</param>
        /// <returns></returns>
        public async Task<bool> VerifyName(int id, string name)
        {
            if (id > 0)
            {
                const string sql = @"SELECT COUNT(id) AS 'id' FROM modules WHERE id!=@id AND `name`=@name;";
                var row = await DapperHelper.ExecuteScalarAsync<int>(sql, new { id, name });
                return row > 0;
            }
            else
            {
                const string sql = @"SELECT COUNT(id) AS 'id' FROM modules WHERE `name`=@name;";
                var row = await DapperHelper.ExecuteScalarAsync<int>(sql, new { name });
                return row > 0;
            }
        }
    }
}
