using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sun.Basic.Dtos
{
    public class WeChatPublishData
    {
        [JsonPropertyName("base_resp")]
        public BaseResp base_resp { get; set; }

        [JsonPropertyName("publish_page")]
        public string publish_page { get; set; }
    }
    public class BaseResp
    {
        [JsonPropertyName("err_msg")]
        public string err_msg { get; set; }
        [JsonPropertyName("ret")]
        public int ret { get; set; }
    }

    public class PublishPage
    {
        [JsonPropertyName("total_count")]
        public int total_count { get; set; }

        [JsonPropertyName("publish_count")]
        public int publish_count { get; set; }

        [JsonPropertyName("masssend_count")]
        public int masssend_count { get; set; }

        [JsonPropertyName("publish_list")]
        public List<PublishItem> publish_list { get; set; }

        [JsonPropertyName("featured_count")]
        public int featured_count { get; set; }
    }

    public class PublishItem
    {
        [JsonPropertyName("total_count")]
        public int publish_type { get; set; }
        [JsonPropertyName("publish_info")]
        public string publish_info { get; set; } // 解析后的对象
    }

    public class PublishInfo
    {
        [JsonPropertyName("type")]
        public int type { get; set; }

        [JsonPropertyName("msgid")]
        public long msgid { get; set; }

        [JsonPropertyName("sent_info")]
        public SentInfo sent_info { get; set; }

        [JsonPropertyName("sent_status")]
        public SentStatus sent_status { get; set; }

        [JsonPropertyName("sent_result")]
        public SentResult sent_result { get; set; }

        [JsonPropertyName("appmsg_info")]
        public List<AppMsgInfo> appmsg_info { get; set; }

        [JsonPropertyName("appmsgex")]
        public List<AppMsgEx> appmsgex { get; set; }

        [JsonPropertyName("copy_type")]
        public int copy_type { get; set; }

        [JsonPropertyName("copy_appmsg_id")]
        public int copy_appmsg_id { get; set; }

        [JsonPropertyName("new_publish")]
        public int new_publish { get; set; }
    }

    public class SentInfo
    {
        [JsonPropertyName("publish_info")]
        public long time { get; set; }
        [JsonPropertyName("publish_info")]
        public int func_flag { get; set; }
        [JsonPropertyName("publish_info")]
        public bool is_send_all { get; set; }
        [JsonPropertyName("publish_info")]
        public int is_published { get; set; }
    }

    public class SentStatus
    {
        [JsonPropertyName("total")]
        public int total { get; set; }
        [JsonPropertyName("succ")]
        public int succ { get; set; }
        [JsonPropertyName("fail")]
        public int fail { get; set; }
        [JsonPropertyName("progress")]
        public int progress { get; set; }
        [JsonPropertyName("userprotect")]
        public int userprotect { get; set; }
    }

    public class SentResult
    {
        [JsonPropertyName("msg_status")]
        public int msg_status { get; set; }
        [JsonPropertyName("refuse_reason")]
        public string refuse_reason { get; set; }
        [JsonPropertyName("reject_index_list")]
        public List<int> reject_index_list { get; set; }
        [JsonPropertyName("update_time")]
        public long update_time { get; set; }
    }

    public class AppMsgInfo
    {
        [JsonPropertyName("share_type")]
        public int share_type { get; set; }
        [JsonPropertyName("appmsgid")]
        public long appmsgid { get; set; }
        [JsonPropertyName("vote_id")]
        public List<string> vote_id { get; set; }
        [JsonPropertyName("super_vote_id")]
        public List<string> super_vote_id { get; set; }
        [JsonPropertyName("smart_product")]
        public int smart_product { get; set; }
        [JsonPropertyName("appmsg_like_type")]
        public int appmsg_like_type { get; set; }
        [JsonPropertyName("itemidx")]
        public int itemidx { get; set; }
        [JsonPropertyName("is_pay_subscribe")]
        public int is_pay_subscribe { get; set; }
        [JsonPropertyName("is_from_transfer")]
        public int is_from_transfer { get; set; }
        [JsonPropertyName("open_fansmsg")]
        public int open_fansmsg { get; set; }
        [JsonPropertyName("share_imageinfo")]
        public List<object> share_imageinfo { get; set; } // 可根据实际数据细化类型
    }

    public class AppMsgEx
    {
        [JsonPropertyName("aid")]
        public string aid { get; set; }
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("cover")]
        public string cover { get; set; }
        [JsonPropertyName("link")]
        public string link { get; set; }
        [JsonPropertyName("digest")]
        public string digest { get; set; }
        [JsonPropertyName("update_time")]
        public long update_time { get; set; }
        [JsonPropertyName("appmsgid")]
        public long appmsgid { get; set; }
        [JsonPropertyName("itemidx")]
        public int itemidx { get; set; }

        /// <summary>
        /// 8是图文消息，0是短视频，5是公众号文章
        /// </summary>
        [JsonPropertyName("item_show_type")]
        public int item_show_type { get; set; }
        [JsonPropertyName("author_name")]
        public string author_name { get; set; }
        [JsonPropertyName("tagid")]
        public List<int> tagid { get; set; }
        [JsonPropertyName("create_time")]
        public long create_time { get; set; }
        [JsonPropertyName("is_pay_subscribe")]
        public int is_pay_subscribe { get; set; }
        [JsonPropertyName("has_red_packet_cover")]
        public int has_red_packet_cover { get; set; }
        [JsonPropertyName("album_id")]
        public string album_id { get; set; }
        [JsonPropertyName("checking")]
        public int checking { get; set; }
        [JsonPropertyName("media_duration")]
        public string media_duration { get; set; }
        [JsonPropertyName("mediaapi_publish_status")]
        public int mediaapi_publish_status { get; set; }
        [JsonPropertyName("copyright_type")]
        public int copyright_type { get; set; }
        [JsonPropertyName("appmsg_album_infos")]
        public List<AppMsgAlbumInfo> appmsg_album_infos { get; set; }
        [JsonPropertyName("pay_album_info")]
        public PayAlbumInfo pay_album_info { get; set; }
        [JsonPropertyName("is_deleted")]
        public bool is_deleted { get; set; }
        [JsonPropertyName("ban_flag")]
        public int ban_flag { get; set; }
        [JsonPropertyName("pic_cdn_url_235_1")]
        public string pic_cdn_url_235_1 { get; set; }
        [JsonPropertyName("pic_cdn_url_16_9")]
        public string pic_cdn_url_16_9 { get; set; }
        [JsonPropertyName("pic_cdn_url_3_4")]
        public string pic_cdn_url_3_4 { get; set; }
        [JsonPropertyName("pic_cdn_url_1_1")]
        public string pic_cdn_url_1_1 { get; set; }
        [JsonPropertyName("id")]
        public string cover_img { get; set; }
        [JsonPropertyName("cover_img_theme_color")]
        public ThemeColor cover_img_theme_color { get; set; }
        [JsonPropertyName("line_info")]
        public LineInfo line_info { get; set; }
        [JsonPropertyName("copyright_stat")]
        public int copyright_stat { get; set; }
        [JsonPropertyName("is_rumor_refutation")]
        public int is_rumor_refutation { get; set; }
        [JsonPropertyName("multi_picture_cover")]
        public int multi_picture_cover { get; set; }
        [JsonPropertyName("share_imageinfo")]
        public List<object> share_imageinfo { get; set; } // 可根据实际数据细化类型

    }

    public class AppMsgAlbumInfo
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("title")]
        public string title { get; set; }
        [JsonPropertyName("album_id")]
        public long album_id { get; set; }
        [JsonPropertyName("appmsg_album_infos")]
        public List<object> appmsg_album_infos { get; set; } // 可根据实际数据细化类型
        [JsonPropertyName("tagSource")]
        public int tagSource { get; set; }
    }

    public class PayAlbumInfo
    {
        [JsonPropertyName("appmsg_album_infos")]
        public List<object> appmsg_album_infos { get; set; } // 可根据实际数据细化类型
    }

    public class ThemeColor
    {
        [JsonPropertyName("r")]
        public int r { get; set; }
        [JsonPropertyName("g")]
        public int g { get; set; }
        [JsonPropertyName("b")]
        public int b { get; set; }
    }

    public class LineInfo
    {
        [JsonPropertyName("use_line")]
        public int use_line { get; set; }
        [JsonPropertyName("line_count")]
        public int line_count { get; set; }
        [JsonPropertyName("is_appmsg_flag")]
        public int is_appmsg_flag { get; set; }
        [JsonPropertyName("is_use_flag")]
        public int is_use_flag { get; set; }
    }
}
