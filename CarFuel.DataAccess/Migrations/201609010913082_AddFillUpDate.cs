namespace CarFuel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFillUpDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblFillUp", "FillUpDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblFillUp", "FillUpDate");
        }
    }
}
