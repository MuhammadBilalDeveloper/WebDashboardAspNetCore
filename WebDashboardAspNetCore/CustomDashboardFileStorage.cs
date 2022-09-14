
using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.DashboardWeb;
using System.Xml.Linq;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Sql;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using WebDashboardAspNetCore.Entities;

public class CustomDashboardFileStorage : DashboardFileStorage {
    public Dictionary<string, string> RsNameToDisplayName = new Dictionary<string, string>();
    public Dictionary<string, string> DisplayNameToRsName = new Dictionary<string, string>();
    DataSet dataSet = new DataSet();
    public CustomDashboardFileStorage(string workingDirectory) : base(workingDirectory) { }
    protected override XDocument LoadDashboard(string dashboardID) {
        var doc = base.LoadDashboard(dashboardID);
        Dashboard dashboard = new Dashboard();
        List<QueryDescription> CurrentQueryDescription = new List<QueryDescription>();

        dashboard.LoadFromXDocument(doc);

        //OracleConnection _connection= new OracleConnection("Data Source=ORADB4;User Id=SAR_OPAL;Password=fiplan;");
        //_connection.Open();
        //decimal queryID = 1051;
        //decimal dsLevel=0;
        //DataTable dataTable = new DataTable();
        //OracleCommand cmd = new OracleCommand($"begin From_DB.get_dataset(:qry_id_, :ds_lvl, :is_cursor, :res_cursor, :ds_name); end;", _connection);

        //cmd.Parameters.Add(new OracleParameter()
        //{
        //    ParameterName = "qry_id_",
        //    OracleDbType = OracleDbType.Int32,
        //    Value = queryID,
        //    Direction = ParameterDirection.Input
        //});

        //cmd.Parameters.Add(new OracleParameter()
        //{
        //    ParameterName = "ds_lvl",
        //    OracleDbType = OracleDbType.Int32,
        //    Value = dsLevel,
        //    Direction = ParameterDirection.Input
        //});

        //var isCursor = cmd.Parameters.Add(new OracleParameter()
        //{
        //    ParameterName = "is_cursor",
        //    OracleDbType = OracleDbType.Int32,
        //    Direction = ParameterDirection.Output
        //});

        //var refcursor = cmd.Parameters.Add(new OracleParameter()
        //{
        //    ParameterName = "res_cursor",
        //    OracleDbType = OracleDbType.RefCursor,
        //    Direction = ParameterDirection.Output
        //});

        //var dsName = cmd.Parameters.Add(new OracleParameter()
        //{
        //    ParameterName = "ds_name",
        //    OracleDbType = OracleDbType.Varchar2,
        //    Size = 100,
        //    Direction = ParameterDirection.Output
        //});

        // cmd.ExecuteNonQuery();

        //if (((OracleDecimal)isCursor.Value).Value == 1)
        //{
        //    OracleDataReader reader = ((OracleRefCursor)refcursor.Value).GetDataReader();
        //    reader.SuppressGetDecimalInvalidCastException = true;
        //    dataTable.Load(reader);
        //    dataTable.TableName = ((OracleString)dsName.Value).Value;
        //}
        //string strQuery = @"select s.basic_attr_id BasicAttributeId, s.basic_attr_type BasicAttributeType, 
        //                        s.basic_attr_name BasicAttributeName, s.level_type LevelType, s.level_id LevelId,
        //                        s.level_name LevelName, s.ordno OrdNo, s.attr_lvl AttributeLevel,
        //                        i.attr_type_code AtrributeTypeCode, i.rs_name RsName, i.rs_display_name RsDisplayName,
        //                        i.display_format DisplayFormat                           
        //                        from show_qrys_sel s, TABLE(attr_info_type) i 
        //                        where s.QRY_ID = :id";


        //var queryDescription = _connection.Query<QueryDescription>(strQuery, new { id= queryID }).ToList();

        //dataSet.Tables.Add(dataTable);


        //PreProcessDatatable(dataSet, queryDescription);
        dataSet.ReadXml(@"C:\Users\muhammad\Downloads\asp-net-core-dashboard-get-started-21.2.5- (1)\asp-net-core-dashboard-get-started-21.2.5-\CS\WebDashboardAspNetCore\xml\testfile.xml");


        //_connection.Close();
        int i = dataSet.Tables.Count > 1 ? 1 : 0;
       
        for (; i < dataSet.Tables.Count; i++)
        {
            DashboardObjectDataSource dashboardDataSource = new DashboardObjectDataSource();
            dashboardDataSource.DataSource = dataSet.Tables[i];
            dashboardDataSource.Name = dataSet.Tables[i].TableName;
            dashboard.DataSources.Add(dashboardDataSource);
        }

        //var dataSource = new DashboardSqlDataSource("SQL Data Source", "OracleConnection");
        //var query = SelectQueryFluentBuilder
        //    .AddTable("QRYS")
        //    .SelectAllColumns()
        //    .Build("QRYS");
        //dataSource.Queries.Add(query);

        //dashboard.DataSources.Add(dataSource);

        //ChartDashboardItem chart = new ChartDashboardItem();
        //chart.DataSource = dataSource;
        //chart.DataMember = dataSource.Queries[0].Name;
        //chart.Arguments.Add(new Dimension("CR_USER"));
        //chart.Panes.Add(new ChartPane());

        //SimpleSeries unitPriceSeries = new SimpleSeries(SimpleSeriesType.Bar);
        //unitPriceSeries.Value = new Measure("CR_USER");
        //chart.Panes[0].Series.Add(unitPriceSeries);

        //dashboard.Items.Add(chart);

        //dashboard.RebuildLayout();

        return dashboard.SaveToXDocument();
    }
    private void PreProcessDatatable(DataSet dataSet, List<QueryDescription> fields)
    {
        foreach (DataTable dataTable in dataSet.Tables)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string displayName = fields?.FirstOrDefault(w => w.RsName == column.ColumnName)?.RsDisplayName.Trim();

                if (!string.IsNullOrWhiteSpace(displayName))
                {
                    RsNameToDisplayName[column.ColumnName.Trim()] = displayName;
                    DisplayNameToRsName[displayName] = column.ColumnName.Trim();
                    column.ColumnName = displayName;
                }

                if (column.ColumnName.Trim() == "RN1")
                {
                    column.ColumnName = "ID Flights Level";
                }
                else if (column.ColumnName.Trim() == "RN2")
                {
                    column.ColumnName = "ID Stops Level";
                }
            }
        }
    }
}