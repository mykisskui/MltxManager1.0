using MltxManager.Models.DBModel;

namespace MltxManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MltxManager.Models.DBModel.MltxDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MltxManager.Models.DBModel.MltxDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.usergroup.AddOrUpdate(
                a => a.GroupId, new Mltx_UserGroup { GroupId = 20150000, GroupName = "最高权限" }
                );
            context.userinfo.AddOrUpdate(
                a => a.GroupId, new Mltx_UserInfo { Enable = Status.可用, GroupId = 20150000, Password = "96E79218965EB72C92A549DD5A33112", UserId = 20158000, UserName = "admin" }
                );
            context.auth_basic.AddOrUpdate(
               a => a.Id,
               new Mltx_Authority_basic { Id = 1, Quan = Quanxian.查看 },
               new Mltx_Authority_basic { Id = 2, Quan = Quanxian.修改 },
               new Mltx_Authority_basic { Id = 3, Quan = Quanxian.删除 },
               new Mltx_Authority_basic { Id = 4, Quan = Quanxian.增加 }
                );
            context.model_basic.AddOrUpdate(
                a => a.Id,
                new Mltx_Model_basic { Id = 1, ModelName = "人员分组" },
                new Mltx_Model_basic { Id = 2, ModelName = "人员管理" },
                new Mltx_Model_basic { Id = 3, ModelName = "分组信息" },
                new Mltx_Model_basic { Id = 4, ModelName = "商品管理" },
                new Mltx_Model_basic { Id = 5, ModelName = "商品订单" },
                new Mltx_Model_basic { Id = 6, ModelName = "鲜果订单" },
                new Mltx_Model_basic { Id = 7, ModelName = "商品评论" },
                new Mltx_Model_basic { Id = 8, ModelName = "会员信息" },
                new Mltx_Model_basic { Id = 9, ModelName = "幻灯片" },
                new Mltx_Model_basic { Id = 10, ModelName = "短信设置" },
                new Mltx_Model_basic { Id = 11, ModelName = "鲜果分组信息" },
                new Mltx_Model_basic { Id = 12, ModelName = "鲜果商品管理" },
                new Mltx_Model_basic { Id = 13, ModelName = "试吃商品" },
                new Mltx_Model_basic { Id = 14, ModelName = "试吃报告" },
                new Mltx_Model_basic { Id = 15, ModelName = "提货券管理" }
                );
            context.model_auth.AddOrUpdate(
                a => a.Id,
                new Mltx_Model_Auth { Id = 1, AuthId = 1, ModelId = 1 },
                new Mltx_Model_Auth { Id = 2, AuthId = 2, ModelId = 1 },
                new Mltx_Model_Auth { Id = 3, AuthId = 3, ModelId = 1 },
                new Mltx_Model_Auth { Id = 4, AuthId = 4, ModelId = 1 },
                new Mltx_Model_Auth { Id = 5, AuthId = 1, ModelId = 2 },
                new Mltx_Model_Auth { Id = 6, AuthId = 2, ModelId = 2 },
                new Mltx_Model_Auth { Id = 7, AuthId = 3, ModelId = 2 },
                new Mltx_Model_Auth { Id = 8, AuthId = 4, ModelId = 2 },
                new Mltx_Model_Auth { Id = 9, AuthId = 1, ModelId = 3 },
                new Mltx_Model_Auth { Id = 10, AuthId = 2, ModelId = 3 },
                new Mltx_Model_Auth { Id = 11, AuthId = 3, ModelId = 3 },
                new Mltx_Model_Auth { Id = 12, AuthId = 4, ModelId = 3 },
                new Mltx_Model_Auth { Id = 13, AuthId = 1, ModelId = 4 },
                new Mltx_Model_Auth { Id = 14, AuthId = 2, ModelId = 4 },
                new Mltx_Model_Auth { Id = 15, AuthId = 3, ModelId = 4 },
                new Mltx_Model_Auth { Id = 16, AuthId = 4, ModelId = 4 },
                new Mltx_Model_Auth { Id = 17, AuthId = 1, ModelId = 5 },
                new Mltx_Model_Auth { Id = 18, AuthId = 2, ModelId = 5 },
                new Mltx_Model_Auth { Id = 19, AuthId = 3, ModelId = 5 },
                new Mltx_Model_Auth { Id = 20, AuthId = 4, ModelId = 5 },
                new Mltx_Model_Auth { Id = 21, AuthId = 1, ModelId = 6 },
                new Mltx_Model_Auth { Id = 22, AuthId = 2, ModelId = 6 },
                new Mltx_Model_Auth { Id = 23, AuthId = 3, ModelId = 6 },
                new Mltx_Model_Auth { Id = 24, AuthId = 4, ModelId = 6 },
                new Mltx_Model_Auth { Id = 25, AuthId = 1, ModelId = 7 },
                new Mltx_Model_Auth { Id = 26, AuthId = 2, ModelId = 7 },
                new Mltx_Model_Auth { Id = 27, AuthId = 3, ModelId = 7 },
                new Mltx_Model_Auth { Id = 28, AuthId = 4, ModelId = 7 },
                new Mltx_Model_Auth { Id = 29, AuthId = 1, ModelId = 8 },
                new Mltx_Model_Auth { Id = 30, AuthId = 2, ModelId = 8 },
                new Mltx_Model_Auth { Id = 31, AuthId = 3, ModelId = 8 },
                new Mltx_Model_Auth { Id = 32, AuthId = 4, ModelId = 8 },
                new Mltx_Model_Auth { Id = 33, AuthId = 1, ModelId = 9 },
                new Mltx_Model_Auth { Id = 34, AuthId = 2, ModelId = 9 },
                new Mltx_Model_Auth { Id = 35, AuthId = 3, ModelId = 9 },
                new Mltx_Model_Auth { Id = 36, AuthId = 4, ModelId = 9 },
                new Mltx_Model_Auth { Id = 37, AuthId = 1, ModelId = 10 },
                new Mltx_Model_Auth { Id = 38, AuthId = 2, ModelId = 10 },
                new Mltx_Model_Auth { Id = 39, AuthId = 3, ModelId = 10 },
                new Mltx_Model_Auth { Id = 40, AuthId = 4, ModelId = 10 },
                new Mltx_Model_Auth { Id = 41, AuthId = 1, ModelId = 11 },
                new Mltx_Model_Auth { Id = 42, AuthId = 2, ModelId = 11 },
                new Mltx_Model_Auth { Id = 43, AuthId = 3, ModelId = 11 },
                new Mltx_Model_Auth { Id = 44, AuthId = 4, ModelId = 11 },
                new Mltx_Model_Auth { Id = 45, AuthId = 1, ModelId = 12 },
                new Mltx_Model_Auth { Id = 46, AuthId = 2, ModelId = 12 },
                new Mltx_Model_Auth { Id = 47, AuthId = 3, ModelId = 12 },
                new Mltx_Model_Auth { Id = 48, AuthId = 4, ModelId = 12 },
                new Mltx_Model_Auth { Id = 49, AuthId = 1, ModelId = 13 },
                new Mltx_Model_Auth { Id = 50, AuthId = 2, ModelId = 13 },
                new Mltx_Model_Auth { Id = 51, AuthId = 3, ModelId = 13 },
                new Mltx_Model_Auth { Id = 52, AuthId = 4, ModelId = 13 },
                new Mltx_Model_Auth { Id = 53, AuthId = 1, ModelId = 14 },
                new Mltx_Model_Auth { Id = 54, AuthId = 2, ModelId = 14 },
                new Mltx_Model_Auth { Id = 55, AuthId = 3, ModelId = 14 },
                new Mltx_Model_Auth { Id = 56, AuthId = 4, ModelId = 14 },
                new Mltx_Model_Auth { Id = 57, AuthId = 1, ModelId = 15 },
                new Mltx_Model_Auth { Id = 58, AuthId = 2, ModelId = 15 },
                new Mltx_Model_Auth { Id = 59, AuthId = 3, ModelId = 15 },
                new Mltx_Model_Auth { Id = 60, AuthId = 4, ModelId = 15 }
                );
        }
    }
}
