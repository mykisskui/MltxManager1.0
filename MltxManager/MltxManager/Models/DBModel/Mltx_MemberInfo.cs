using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MltxManager.Models.DBModel
{
    /// <summary>
    /// 会员信息表
    /// </summary>
    public class Mltx_MemberInfo
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 微信标识
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 会员昵称
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 默认地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        [ForeignKey("RankId")]
        public Mltx_MemberRank Rank { get; set; }
        public Mltx_MemberRank rankAction(int id = 0) {

            using (var db = new MltxDbContext())
            {
                Rank = db.memberrank.Where(f => f.Id == id).FirstOrDefault();
            }

            return Rank;
        }
        public int RankId { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Consume { get; set; }
        /// <summary>
        /// 消费次数
        /// </summary>
        public int Consumenumber { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string UserPic { get; set; }
    }
}
