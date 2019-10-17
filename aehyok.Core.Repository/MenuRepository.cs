using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using aehyok.Core.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.Repository
{
    public class MenuRepository : AbstractBaseRepository,IMenuRepository
    {
        public MenuRepository(IDbAccossor dbAccossor) : base(dbAccossor)
        { }
        private IDbAccossor _dbAccossor => base.dbAccossor;


        private const string _sqlGetMenuList = @"select * from MD_MainMenu";
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetMenuList()
        {
            List<MenuItem> list = new List<MenuItem>();
            try
            {
                _dbAccossor.DbDefaultConnection.Using(dbConn =>
                {
                    using (IDataReader dr = dbConn.ExecuteReader(_sqlGetMenuList, null, null, null, CommandType.Text))
                    {
                        while (dr.Read())
                        {
                            MenuItem sinoMenu = new MenuItem(
                                        dr.IsDBNull(0) ? "" : dr.GetString(0),
                                        dr.IsDBNull(1) ? "" : dr.GetString(1),
                                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                                        dr.IsDBNull(3) ? "" : dr.GetString(3),
                                        dr.IsDBNull(4) ? 0 : Convert.ToInt32(dr.GetDecimal(4)),
                                        dr.IsDBNull(5) ? "0" : dr.GetString(5), true, 1, "", "");
                            list.Add(sinoMenu);
                        }
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
    }
}
