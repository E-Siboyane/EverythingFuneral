namespace EverythingFuneral.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientDetail",
                c => new
                    {
                        ClientDetailId = c.Int(nullable: false, identity: true),
                        MemebershipReference = c.String(),
                        Username = c.String(nullable: false),
                        FuneralCategoryId = c.Int(nullable: false),
                        FullName = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        IdNumber = c.String(nullable: false),
                        CountryOfIssue = c.String(nullable: false),
                        HighestQualification = c.String(nullable: false),
                        HomeLanguage = c.String(nullable: false),
                        ResidentialAddressStreetAddress = c.String(nullable: false),
                        ResidentialAddressSurbub = c.String(nullable: false),
                        ResidentialAddressCity = c.String(nullable: false),
                        ResidentialAddressProvince = c.String(nullable: false),
                        ResidentialAddressCountry = c.String(nullable: false),
                        ResidentialAddressPostalCode = c.Int(nullable: false),
                        PostalAddressSameResidential = c.String(nullable: false),
                        PostalAddressLineOne = c.String(),
                        PostalAddressLineTwo = c.String(),
                        PostalAddressCity = c.String(),
                        PostalAddressCode = c.String(),
                        MaritalStatus = c.String(nullable: false),
                        NumberOfDependent = c.Int(nullable: false),
                        EmployerName = c.String(nullable: false),
                        Industry = c.String(nullable: false),
                        YearsWorkingAtEmployer = c.Int(nullable: false),
                        GrossSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CellNumber = c.String(nullable: false),
                        NextOfKinCellNumber = c.String(),
                        NextOfKinName = c.String(),
                        AllowMarketing = c.String(),
                        GraveyardType = c.String(),
                        GraveyardTypeOtherSpecify = c.String(),
                        Municipality = c.String(),
                        GraveyardDigging = c.String(),
                        GraveyardClosingAndClearing = c.String(),
                        GraveyardOtherDetails = c.String(),
                        MortuaryLocation = c.String(),
                        MortuaryName = c.String(),
                        MortuaryOther = c.String(),
                        CoufinType = c.String(),
                        TransportationHearse = c.String(),
                        TransportationFamily = c.String(),
                        TransportationMinibus = c.String(),
                        TransportationBus = c.String(),
                        AccomodationHotelQuestNumber = c.String(),
                        AccomodationBandBQuestNumber = c.String(),
                        HallName = c.String(),
                        TentType = c.String(),
                        Toilet = c.String(),
                        TypeofDecoration = c.String(),
                        AffiliationTheme = c.String(),
                        TyepOfChair = c.String(),
                        NumberOfChair = c.String(),
                        CateringNumberOfCow = c.String(),
                        CateringNumberOfSheep = c.String(),
                        CateringNumberOfGoat = c.String(),
                        CateringNumberOfPeople = c.String(),
                        CateringDayLeadingToFuneral = c.String(),
                        CateringAdditionalWater = c.String(),
                        CateringOther = c.String(),
                        Tombstone = c.String(),
                        TombstoneOtherSpecify = c.String(),
                        FlowerType = c.String(),
                        FlowerSpecify = c.String(),
                        ArtistMusic = c.String(),
                        ArtistSoundMic = c.String(),
                        OtherServiceMinister = c.String(),
                        OtherServiceProgrammeCard = c.String(),
                        OtherServiceInvitationCommunication = c.String(),
                        OtherServiceFamilyTracing = c.String(),
                        OtherServicesCommunication = c.String(),
                        OtherServiceGestureHospitality = c.String(),
                        OtherServiceSpecialQuests = c.String(),
                        OtherServiceMouners = c.String(),
                        PostFuneralAssestSharing = c.String(),
                        PostFuneralOtherbeneficiaryDistribution = c.String(),
                        PostFuneralCounselingService = c.String(),
                        PostFuneralRecoveryProcess = c.String(),
                        PostFuneralGiftManagement = c.String(),
                        PostFuneralCleansing = c.String(),
                        PostFuneralClosure = c.String(),
                        PostFuneralAfterTears = c.String(),
                        SpecialRequestSpecify = c.String(),
                        RecordCode = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.ClientDetailId)
                .ForeignKey("dbo.FuneralCategory", t => t.FuneralCategoryId)
                .ForeignKey("dbo.RecordStatus", t => t.RecordCode)
                .Index(t => t.FuneralCategoryId)
                .Index(t => t.RecordCode);
            
            CreateTable(
                "dbo.FuneralCategory",
                c => new
                    {
                        FuneralCategoryId = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        DisplayName = c.String(nullable: false),
                        PayoutAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                        LastModified = c.DateTime(),
                        RecordCode = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FuneralCategoryId)
                .ForeignKey("dbo.RecordStatus", t => t.RecordCode)
                .Index(t => t.RecordCode);
            
            CreateTable(
                "dbo.RecordStatus",
                c => new
                    {
                        RecordCode = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        DeletedDate = c.DateTime(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.RecordCode);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ClientDetail", "RecordCode", "dbo.RecordStatus");
            DropForeignKey("dbo.ClientDetail", "FuneralCategoryId", "dbo.FuneralCategory");
            DropForeignKey("dbo.FuneralCategory", "RecordCode", "dbo.RecordStatus");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.FuneralCategory", new[] { "RecordCode" });
            DropIndex("dbo.ClientDetail", new[] { "RecordCode" });
            DropIndex("dbo.ClientDetail", new[] { "FuneralCategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RecordStatus");
            DropTable("dbo.FuneralCategory");
            DropTable("dbo.ClientDetail");
        }
    }
}
