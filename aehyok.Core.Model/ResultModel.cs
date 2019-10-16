using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Model
{
    public class ResultModel
    {
        public string Code { get; set; }

        public string Msg { get; set; }


        public List<MenuModel> Data { get; set; }

        public ResultModel()
        {
            Data = new List<MenuModel>();
        }
    }
}
