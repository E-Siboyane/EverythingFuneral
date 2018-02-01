using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EverythingFuneral.Web.Models.DatabaseModels {
    public class ClientDetail {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientDetailId { get; set; }
        public Guid RecordUniqueId { get; set; }
        public string MemebershipReference { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int FuneralCategoryId { get; set; }
        public virtual FuneralCategory FuneralOption { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string IdNumber { get; set; }
        [Required]
        public string CountryOfIssue { get; set; }
        [Required]
        public string HighestQualification { get; set; }
        [Required]
        public string HomeLanguage { get; set; }
        [Required]
        public string ResidentialAddressStreetAddress { get; set; }
        [Required]
        public string ResidentialAddressSurbub { get; set; }
        [Required]
        public string ResidentialAddressCity { get; set; }
        [Required]
        public string ResidentialAddressProvince { get; set; }
        [Required]
        public string ResidentialAddressCountry { get; set; }
        [Required]
        public string ResidentialAddressPostalCode { get; set; }
        [Required]
        public string PostalAddressSameResidential { get; set; }
        public string PostalAddressLineOne { get; set; }
        public string PostalAddressLineTwo { get; set; }
        public string PostalAddressCity { get; set; }
        public string PostalAddressCode { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public int NumberOfDependent { get; set; }
        [Required]
        public string EmployerName { get; set; }
        [Required]
        public string Industry { get; set; }
        [Required]
        public int YearsWorkingAtEmployer { get; set; }
        [Required]
        public decimal GrossSalary { get; set; }
        [Required]
        public decimal NetIncome { get; set; }
        [Required]
        public string CellNumber { get; set; }
        public string NextOfKinCellNumber { get; set; }
        public string NextOfKinName { get; set; }
        public string AllowMarketing { get; set; }
        public string GraveyardType { get; set; }
        public string GraveyardTypeOtherSpecify { get; set; }
        public string Municipality { get; set; }
        public string GraveyardDigging { get; set; }
        public string GraveyardClosingAndClearing { get; set; }
        public string GraveyardOtherDetails { get; set; }
        public string MortuaryLocation { get; set; }
        public string MortuaryName { get; set; }
        public string MortuaryOther { get; set; }
        public string CoufinType { get; set; }
        public string TransportationHearse { get; set; }
        public string TransportationFamily { get; set; }
        public string TransportationMinibus { get; set; }
        public string TransportationBus { get; set; }
        public string AccomodationHotelQuestNumber { get; set; }
        public string AccomodationBandBQuestNumber { get; set; }
        public string HallName { get; set; }
        public string TentType { get; set; }
        public string Toilet { get; set; }
        public string TypeofDecoration { get; set; }
        public string AffiliationTheme { get; set; }
        public string TyepOfChair { get; set; }
        public string NumberOfChair { get; set; }
        public string CateringNumberOfCow { get; set; }
        public string CateringNumberOfSheep { get; set; }
        public string CateringNumberOfGoat { get; set; }
        public string CateringNumberOfPeople { get; set; }
        public string CateringDayLeadingToFuneral { get; set; }
        public string CateringAdditionalWater { get; set; }
        public string CateringOther { get; set; }
        public string Tombstone { get; set; }
        public string TombstoneOtherSpecify { get; set; }
        public string FlowerType { get; set; }
        public string FlowerSpecify { get; set; }
        public string ArtistMusic { get; set; }
        public string ArtistSoundMic { get; set; }
        public string OtherServiceMinister { get; set; }
        public string OtherServiceProgrammeCard { get; set; }
        public string OtherServiceInvitationCommunication { get; set; }
        public string OtherServiceFamilyTracing { get; set; }
        public string OtherServicesCommunication { get; set; }
        public string OtherServiceGestureHospitality { get; set; }
        public string OtherServiceSpecialQuests { get; set; }
        public string OtherServiceMouners { get; set; }
        public string PostFuneralAssestSharing { get; set; }
        public string PostFuneralOtherbeneficiaryDistribution { get; set; }
        public string PostFuneralCounselingService { get; set; }
        public string PostFuneralRecoveryProcess { get; set; }
        public string PostFuneralGiftManagement { get; set; }
        public string PostFuneralCleansing { get; set; }
        public string PostFuneralClosure { get; set; }
        public string PostFuneralAfterTears { get; set; }
        public string SpecialRequestSpecify { get; set; }
        public string RecordCode { get; set; }
        public virtual RecordStatus RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? LastModified { get; set; }
    }
}