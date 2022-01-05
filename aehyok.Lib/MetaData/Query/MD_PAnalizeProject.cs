using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    public class MD_PAnalizeProject
    {

        public string ID { get; set; }

        public string DisplayTitle { get; set; }
        public string Description { get; set; }
        public string PAType { get; set; }
        public string CreateUserID { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public int DisplayOrder { get; set; }

        public MD_PAnalizeProject(string _id, string _title, string _descript, string _type, string _userid, string _username, DateTime _createDate, int _order)
        {
            ID = _id;
            DisplayTitle = _title;
            Description = _descript;
            PAType = _type;
            CreateUserID = _userid;
            CreateUserName = _username;
            CreateDate = _createDate;
            DisplayOrder = _order;
        }

        public override string ToString()
        {
            return DisplayTitle;
        }

    }
}
