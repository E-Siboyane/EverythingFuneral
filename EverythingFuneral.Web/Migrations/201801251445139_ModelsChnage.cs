namespace EverythingFuneral.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsChnage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientDetail", "ResidentialAddressPostalCode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientDetail", "ResidentialAddressPostalCode", c => c.Int(nullable: false));
        }
    }
}
