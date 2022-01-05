using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.RefCode
{
    public enum RefCodeType
    {
        Number,                 //	string类型的纯数字代码(允许空格)
        Alpha,                  //	string类型的纯字母代码(允许空格)
        AlphaNumber,            //	string类型的字母和数字混合的代码(允许空格)
    }
}
