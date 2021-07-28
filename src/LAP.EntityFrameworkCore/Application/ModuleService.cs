﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LAP.Common;
using LAP.EntityFrameworkCore.Entity;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.EntityFrameworkCore.Application
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public class ModuleService
    {
        private static readonly DapperHelper DapperHelper = new DapperHelper();

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

            var sql = @"SELECT `id`, `name`, `code`, `created_by`, `created_time` FROM `modules` WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                sql += " and name LIKE CONCAT('%',@searchKey,'%')";
                parameters.Add("@searchKey", searchKey);
            }

            pagedList.total = (await DapperHelper.QueryAsync<ModuleEntity>(sql, parameters)).Count();

            sql += " order by created_time desc limit @pageIndex,@pageSize";

            pagedList.dataList = await DapperHelper.QueryAsync<ModuleEntity>(sql, parameters);
            return pagedList;
        }

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<ModuleEntity> Find(int id)
        {
            var sql = @"SELECT `id`, `name`, `code`, `created_by`, `created_time` FROM `modules` WHERE id=@id;";
            return await DapperHelper.QueryFirstAsync<ModuleEntity>(sql, new { id });
        }

        /// <summary>
        /// 添加Log
        /// </summary>
        /// <param name="input">Module模型</param>
        /// <returns></returns>
        public async Task<bool> InsterModule(ModuleEntity model)
        {
            using var conn = DapperHelper.Connection();
            using var transaction = conn.BeginTransaction();
            try
            {
                var sql = @"INSERT INTO `modules` ( `name`, `code`, `created_by`, `created_time` )
                        VALUES (@name, code, @created_by, @created_time);";

                var code = await DapperHelper.ExecuteScalarAsync<int>("SELECT IFNULL(MAX(id),1)+100 AS 'max_id' FROM modules;");
                var param = new
                {
                    model.name,
                    code,
                    model.created_by,
                    create_time = DateTime.Now
                };
                await DapperHelper.ExecuteAsync(sql, param);
                transaction.Commit();
                return true;
            }
            finally
            {
                transaction.Rollback();
            }
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="name">模块名称</param>
        /// <returns></returns>
        public async Task<bool> UpdateModule(int id, string name)
        {
            var sql = @"UPDATE `modules` SET `name` = @name WHERE `id` = @id;";
            return await DapperHelper.ExecuteAsync(sql, new { id, name });
        }

        /// <summary>
        /// 模块名称验证
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="name">模块名称</param>
        /// <returns></returns>
        public async Task<bool> VerifyName(int id, string name)
        {
            if (id > 0)
            {
                var sql = @"SELECT COUNT(id) AS 'id' FROM modules WHERE id!=@id AND `name`=@name;";
                var row = await DapperHelper.ExecuteScalarAsync<int>(sql, new { id, name });
                return row > 0;
            }
            else
            {
                var sql = @"SELECT COUNT(id) AS 'id' FROM modules WHERE `name`=@name;";
                var row = await DapperHelper.ExecuteScalarAsync<int>(sql, new { name });
                return row > 0;
            }
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<bool> DeleteModule(int id)
        {
            var sql = @"DELETE FROM `modules` WHERE `id` = @id;";
            return await DapperHelper.ExecuteAsync(sql, new { id });
        }
    }
}
