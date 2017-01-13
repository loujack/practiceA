namespace practiceA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Parties",
                c => new
                    {
                        no = c.Int(nullable: false, identity: true),
                        shopName = c.String(),
                        phone = c.String(),
                        address = c.String(nullable: false),
                        note = c.String(),
                        time = c.String(nullable: false),
                        nickName = c.String(nullable: false, maxLength: 5),
                        color = c.String(),
                        partyName = c.String(nullable: false, maxLength: 10),
                        participants = c.String(),
                    })
                .PrimaryKey(t => t.no);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Parties");
        }
    }
}
