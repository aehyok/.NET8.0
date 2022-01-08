using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MDQuery_Task
    {


        public MDQuery_Task(string _tid, string _tname, DateTime _requestTime, DateTime _outTime, int _priority, int _taskState,
                DateTime? _finishedTime, string _userid, string _userName, string _postid, string _postName, string _postDwName,
                bool _lock, DateTime _clearTime, string _taskType)
        {
            TaskID = _tid;
            TaskName = _tname;
            RequestTime = _requestTime;
            OutTime = _outTime;
            Priority = _priority;
            TaskState = _taskState;
            FinishedTime = _finishedTime;
            RequestUserID = _userid;
            RequestUserName = _userName;
            RequestPostID = _postid;
            RequestPostName = _postName;
            RequestPostDwName = _postDwName;
            ClearTime = _clearTime;
            TaskType = _taskType;
            ResultLocked = _lock;
        }
        [DataMember]
        public bool ResultLocked { get; set; }

        [DataMember]
        public string TaskType { get; set; }
        [DataMember]
        public DateTime ClearTime { get; set; }
        [DataMember]
        public string RequestPostID { get; set; }
        [DataMember]
        public string RequestPostDwName { get; set; }
        [DataMember]
        public string RequestPostDWID { get; set; }

        [DataMember]
        public string RequestPostName { get; set; }
        [DataMember]
        public int TaskState { get; set; }
        [DataMember]
        public DateTime? FinishedTime { get; set; }
        [DataMember]
        public string RequestUserID { get; set; }
        [DataMember]
        public string RequestUserName { get; set; }
        [DataMember]
        public string TaskID { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public DateTime RequestTime { get; set; }
        [DataMember]
        public DateTime OutTime { get; set; }
        [DataMember]
        public int Priority { get; set; }

        public string PostName
        {
            get { return string.Format("{0}[{1}]", RequestPostName, RequestPostDwName); }
        }
    }
}
