using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EnumsNET;
using LAP.EntityFrameworkCore.Entity;
using LAP.EntityFrameworkCore.ViewModel;

namespace LAP.EntityFrameworkCore.Application
{
    public class DashboardService
    {
        private static readonly DapperHelper DapperHelper = new();

        public async Task<Dictionary<string, int>> Statistics()
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    var dic = new Dictionary<string, int>();

                    var requestNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `statistic_logs`;");
                    dic.Add("requestNum", requestNum);

                    var logNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `logs`;");
                    dic.Add("logNum", logNum);

                    var monitorNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `early_warning`;");
                    dic.Add("monitorNum", monitorNum);

                    var moduleNum = await conn.ExecuteScalarAsync<int>("SELECT COUNT(id) AS 'rows' FROM `modules`;");
                    dic.Add("moduleNum", moduleNum);

                    transaction.Commit();
                    return dic;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                }
            }
        }

        public async Task<LogChartDto> LogChart(string startDate, string endDate)
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    var chartDto = new LogChartDto();

                    var moduleList = await conn.QueryAsync<ModuleEntity>("SELECT `id`, `name`, `code`, `created_by`, `created_time` FROM `modules`;");
                    chartDto.module = moduleList.Select(p => p.name).ToArray();

                    var logLevelList = Enums.GetMembers<Enum.LogLevel>();
                    chartDto.xAxis = logLevelList.Select(p => p.Name).ToArray();

                    var dataList = new List<LogChartDataDto>();
                    foreach (var item in moduleList)
                    {
                        var dto = new LogChartDataDto()
                        {
                            name = item.name
                        };
                        var array = new List<int>();
                        foreach (var logLevel in logLevelList)
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@module_code", item.code);
                            parameters.Add("@level", logLevel.ToInt32());

                            var sql = "SELECT COUNT(id) AS 'rows' FROM `logs` WHERE module_code=@module_code AND `level`=@level";
                            if (!string.IsNullOrWhiteSpace(startDate))
                            {
                                sql += " AND DATE(log_create_time)>=@startDate";
                                parameters.Add("@startDate", startDate);
                            }
                            if (!string.IsNullOrWhiteSpace(endDate))
                            {
                                sql += " AND DATE(log_create_time)<=@endDate";
                                parameters.Add("@endDate", endDate);
                            }
                            if (DateTime.Compare(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)) == 0)
                            {
                                sql += " AND DATE(log_create_time)=@startDate";
                                parameters.Add("@startDate", startDate);
                            }
                            if (string.IsNullOrWhiteSpace(startDate) && string.IsNullOrWhiteSpace(endDate))
                            {
                                sql += " AND DATE(log_create_time)=@startDate";
                                parameters.Add("@startDate", DateTime.Now.ToString("yyyy-MM-dd"));
                            }
                            int rows = await conn.ExecuteScalarAsync<int>(sql, parameters);
                            array.Add(rows);
                        }
                        dto.data = array.ToArray();
                        dataList.Add(dto);
                    }
                    chartDto.data = dataList;

                    transaction.Commit();
                    return chartDto;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                    conn.Dispose();
                }
            }
        }

        public async Task<StatisticLogChartDto> StatisticLogChart(string startDate, string endDate)
        {
            using var conn = DapperHelper.Connection();
            {
                using var transaction = conn.BeginTransaction();
                try
                {
                    var chartDto = new StatisticLogChartDto();

                    var moduleList = await conn.QueryAsync<ModuleEntity>("SELECT `id`, `name`, `code`, `created_by`, `created_time` FROM `modules`;");
                    chartDto.module = moduleList.Select(p => p.name).ToArray();

                    var serieTag = new[] { 1, 2, 3 };

                    var seriesData = new List<StatisticLogChartDataDto>();
                    var data = new List<StatisticLogChartDataDto>();
                    foreach (var item in moduleList)
                    {
                        var model = new StatisticLogChartDataDto()
                        {
                            name = item.name
                        };

                        var parameters = new DynamicParameters();
                        parameters.Add("@module_code", item.code);

                        var sql = "SELECT COUNT(id) AS 'rows' FROM statistic_logs WHERE module_code=@module_code";
                        if (!string.IsNullOrWhiteSpace(startDate))
                        {
                            sql += " AND DATE(request_time)>=@startDate";
                            parameters.Add("@startDate", startDate);
                        }
                        if (!string.IsNullOrWhiteSpace(endDate))
                        {
                            sql += " AND DATE(request_time)<=@endDate";
                            parameters.Add("@endDate", endDate);
                        }
                        if (DateTime.Compare(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)) == 0)
                        {
                            sql += " AND DATE(request_time)=@startDate";
                            parameters.Add("@startDate", startDate);
                        }
                        if (string.IsNullOrWhiteSpace(startDate) && string.IsNullOrWhiteSpace(endDate))
                        {
                            sql += " AND DATE(request_time)=@startDate";
                            parameters.Add("@startDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        }

                        model.value = await conn.ExecuteScalarAsync<int>(sql, parameters);

                        if (serieTag.Contains(item.code))
                        {
                            seriesData.Add(model);
                        }
                        else
                        {
                            data.Add(model);
                        }
                    }
                    chartDto.series_data = seriesData;
                    chartDto.data = data;

                    transaction.Commit();
                    return chartDto;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                    conn.Dispose();
                }
            }
        }
    }
}
