using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TutorialApp.Business.Common.Constants;

[AttributeUsage(AttributeTargets.Field)]
public class StatusCodeAttribute : Attribute
{
    public int StatusCode { get; set; }

    public StatusCodeAttribute(int statusCode)
    {
        StatusCode = statusCode;
    }
}

public static class Message
{
    #region RFTA AND LANDLORD 0 -200

    [StatusCode(001)]
    public const string UserAlreadyExists = "The Email already exists in our system, please use different one";

    [StatusCode(002)] public const string TNumberRequired = "The Voucher number  must not be null or empty.";

    [StatusCode(003)]
    public const string NotAffordableAmount = "The rental unit is not affordable. Please adjust the rent amount.";

    [StatusCode(004)] public const string AffordableAmount = "The rental unit is now affordable.";

    [StatusCode(005)] public const string RentAdjusted = "The rent adjusted succssfully";

    [StatusCode(006)] public const string RentAdjustmentFailed = "There was some problem adjusting rent";

    [StatusCode(007)]
    public const string TNumberNotFound = "The voucher number you provided is not available in our system";

    [StatusCode(008)] public const string RFTASaved = "RFTA was added successfully";

    [StatusCode(009)] public const string RFTAFailure = "There was some problem to save RFTA in our system";

    [StatusCode(010)]
    public const string UnitNotFound = "Sorry we could not find any Unit matching the unit Id you provided";

    [StatusCode(011)] public const string DisclosureSaved = "Disclosure was saved successfully";

    [StatusCode(012)]
    public const string DisclosureSaveFailed = "There was some problem while saving disclosures in our system";

    [StatusCode(013)] public const string UnitExists = "A unit with the same name already exists in this property.";

    [StatusCode(014)] public const string AlertNotificationSectionFailure =
        "There was some problem to save Alert Notification Section in our system";

    [StatusCode(015)]
    public const string TNumberAlreadyAssociatedWithOtherLandlord = "Tenant already in talks with other landlord";

    [StatusCode(016)] public const string VoucherExpired =
        "The Voucher number you provided has expired , please use different voucher";

    [StatusCode(017)] public const string InvalidRentAmount = "Invalid Rent Amount";

    [StatusCode(018)] public const string NotAuthorised = "You are not authorised for this";

    [StatusCode(019)] public const string InvalidLandLordPropertyId = "Invalid LandLordProperty Id";

    [StatusCode(020)] public const string NoApplicant = "No applicant was found with the voucher number you provided";

    [StatusCode(021)] public const string InvalidLandLordId = "Invalid LandLord Id provided";

    [StatusCode(022)] public const string NoRftaFoundForLandLord = "No rfta found for specified landlord";

    [StatusCode(023)] public const string RftaStatusUpdated = "Rfta status Updated successfully";

    [StatusCode(024)] public const string InvalidRftaDisclosureId = "Invalid rfta disclosure Id";

    [StatusCode(025)] public const string DocumentRftaAdded = "Documets for rfta disclosures added successfully";

    [StatusCode(026)] public const string InvalidRftaId = "Invalid rfta Id provided";

    [StatusCode(027)] public const string InvalidDocumentname = "No documents  found for this name";

    [StatusCode(028)] public const string InspectionScheduled = "Inspection was scheduled successfully";

    [StatusCode(029)]
    public const string NoInspectionFound = "No inspection can be found with this search term or for this landLord";

    [StatusCode(030)] public const string InvalidInspectionScheduleId = "Invalid inspection schedule Id provided";

    [StatusCode(031)] public const string InspectionResultFailed = "Problem in adding result for the given inspection";

    [StatusCode(032)] public const string InvalidScheduleSlotId = "Invalid slot id provided for scheduling inspection";

    [StatusCode(033)] public const string VoucherAndLastName = "Voucher Number does not match with provided last name";

    [StatusCode(034)] public const string ProblemInSavinUnit = "There was problem in Saving Unit";
    [StatusCode(035)] public const string NoPropertiesFoundForLandLord = "No Properties present for the LandLord";
    [StatusCode(036)] public const string NoPropertyFound = "No Property found";
    [StatusCode(037)] public const string NoUnitsFoundForProperty = "No Units found for the property.";
    [StatusCode(038)] public const string NoUnitOrPropertyFound = "No Units or property found for the LandLord";
    [StatusCode(039)] public const string UnitStructureTypeNotfound = "No Unit Structure Type present in the database.";
    [StatusCode(040)] public const string UnitSubsidyTypeNotfound = "No Unit Subsidy Type present in the database.";
    [StatusCode(041)] public const string UnitAmenityTypeNotfound = "No Unit Amenity Type present in the database.";

    [StatusCode(042)]
    public const string UnitAppliancesTypeNotfound = "No Unit Appliances Type present in the database.";

    [StatusCode(043)] public const string NoDirectDepositForm = "No Data Available";
    [StatusCode(044)] public const string LlProfileAdded = "Landlord profile added sucessfully.";
    [StatusCode(045)] public const string LlProfileAlreadyThere = "Landlord profile already there";
    [StatusCode(046)] public const string InvalidState = "The State name you provided is not available in our system";

    [StatusCode(047)]
    public const string UnitAssociatedWithRfta = "Unable to delete unit , because associated with rfta";

    [StatusCode(048)] public const string ProblemInSavinProperty = "There was problem in Saving Property.";
    [StatusCode(049)] public const string UpdateUnitDetails = "Unit Details Updated successfully.";
    [StatusCode(050)] public const string UpdateUnitDetailsFailed = "There was problem in  Update Unit Details.";
    [StatusCode(051)] public const string UnitUtilityNotfound = "No Unit Utility present in the database.";
    [StatusCode(052)] public const string DisclosureNotSaved = "Problem in saving disclosure for this rfta";
    [StatusCode(053)] public const string Unitdeleted = "Unit deleted successfully";
    [StatusCode(054)] public const string NoDisclosureDocumentFound = "No disclosure document found match this name";

    [StatusCode(055)] public const string ComparableUnits = "Comparable Units added";
    [StatusCode(056)] public const string RequiredField = "This field is required";
    [StatusCode(057)] public const string InvalidUnitId = "Invalid Unit Id provided";
    [StatusCode(058)] public const string InvalidRftaStatusId = "Invalid number provided for rfta status";
    [StatusCode(059)] public const string InvalidRftaDisclosureDetailId = "Invalid rfta disclosure detail Id";

    [StatusCode(060)]
    public const string UnAssisstedProperties = "Un Assissted Properties for this rfta was updated successfully";

    [StatusCode(061)] public const string RftaWithdrawnSuccess = "Rfta was withdrawn successfully";
    [StatusCode(062)] public const string ProblemInDeleting = "Problem in deleting ";

    [StatusCode(063)] public const string RftaStatusUpdatedFailure =
        "Problem in updating rfta status, either rfta is withdrawn or affordbaility is not met";

    [StatusCode(064)] public const string InvalidDocumentType = "Invalid Document Type provided";
    [StatusCode(065)] public const string NoDocumentFound = "No Data Available";
    [StatusCode(066)] public const string NoRftaFound = "No Data Available";
    [StatusCode(067)] public const string DocumentDeleted = "Document Deleted successfully !!";
    [StatusCode(068)] public const string TaxClassificationNotFound = "TaxClassification types not found!!";
    [StatusCode(069)] public const string SlotDeleted = "Slot was deleted successfully";
    [StatusCode(070)] public const string SlotSelected = "Slot was selected successfully";
    [StatusCode(071)] public const string InspectionDeleted = "Inspection was deleted successfully";
    [StatusCode(072)] public const string OneSlot = "Please provide at least one slot for inspection";

    [StatusCode(073)] public const string ThreeSlotMax = "Please provide at least one slot for inspection";
    [StatusCode(074)] public const string IncomeSourcesNotfound = "No Data Available";
    [StatusCode(075)] public const string InvalidNoOfBedRooms = "No of bedrooms should greater then 0.";
    [StatusCode(076)] public const string InvalidUnitAreaSft = "UnitAreaSft should be greater then 0.";
    [StatusCode(077)] public const string InvalidUnitRentAmount = "UnitRentAmount should be greater then 0.";

    [StatusCode(078)]
    public const string NoIncomeSourcesAdded = "IncomeSources not added select atleaset one CheckBox .";

    [StatusCode(078)] public const string NoState = "No Data Available";
    [StatusCode(079)] public const string NoEft = "No Data Available";

    [StatusCode(080)]
    public const string InvalidAccountType = "OOPs there is invalid bank account type registered for you.";

    [StatusCode(081)] public const string NoAccountTypes = "No Data Available";
    [StatusCode(082)] public const string ProblemInUpdateUnit = "There was problem while Updateing the Unit";
    [StatusCode(083)] public const string UpdateLandlordPhoneNumber = "Phone Number updated successfully";

    [StatusCode(084)] public const string VoucherIsLocked =
        "We’re sorry, the family has already submitted an RFTA and is being reviewed by HOME. Please have the family contact our office to discuss their options of cancelling the current RFTA and submit a new one.";

    [StatusCode(085)]
    public const string UnitIsLocked = "This unit is already locked with other rfta, please try with different unit";

    [StatusCode(086)] public const string InvalidEftId = "The Eft Id you provided is Invalid";

    [StatusCode(087)]
    public const string NoAlertsFoundForLandLord = "No alerts or notification found for this landlord";

    [StatusCode(088)] public const string InvalidW9FormDetailId = "Invalid  w9 form details Id provided";
    [StatusCode(089)] public const string NoClientsFound = "No Data Available";
    [StatusCode(088)] public const string NoLedgersFound = "No Data Available";

    [StatusCode(089)] public const string NopreferedmodeofcommunicationAdded =
        "Preffered mode of communication is not added select atleaset one CheckBox .";

    [StatusCode(090)] public const string NoRaceAndEthinicityNotAdded =
        "Race and Ethnicity is not added select atleaset one CheckBox .";

    [StatusCode(091)] public const string NoDataFound = "No data found";

    [StatusCode(092)] public const string InvalidRentRequest =
        "There is an existing request already in process for the selected Unit";

    [StatusCode(093)] public const string InvalidEmail = "Email does not exist in our system";
    [StatusCode(094)] public const string OTPVerified = "OTP Verified Successfully";

    [StatusCode(095)] public const string InvalidOTP =
        "** Oops! It looks like this is the incorrect password or the password may have expired. Please verify the password sent to your email and try again or request a new one-time password.  ";

    [StatusCode(096)] public const string NoActiveTenant = "No Active Tenants found";

    [StatusCode(097)] public const string ValidCRIRequest =
        "The request must be made within 120 days before the contract or lease end date.";

    [StatusCode(098)] public const string PendingInspection = "There is already one pending inspection";

    #endregion

    #region Applicant 201-400

    [StatusCode(201)] public const string NoApplicationFound = "There's no application found.";
    [StatusCode(202)] public const string ApplicationAlreadyExist = "Application already there for this user.";
    [StatusCode(203)] public const string ProxyDetailsUpdatedSuccess = "Proxy details updated sucessfully.";
    [StatusCode(204)] public const string ApplicationInitiatedSuccess = "Application Initiated Successfully.";
    [StatusCode(205)] public const string ApplicationDetailsFound = "These are the application details.";
    [StatusCode(206)] public const string ApplicationAddressAdded = "Application address added sucessfully.";
    [StatusCode(207)] public const string ApplicationAddressUpdated = "Application address updated sucessfully.";

    [StatusCode(208)]
    public const string ApplicationContactOptionsUpdated = "Application contact options updated sucessfully.";

    [StatusCode(209)]
    public const string ApplicationAnyHouseholdsUpdated = "Application any household updated sucessfully.";

    [StatusCode(210)] public const string ApplicationHouseholdMemberAdded = "Household member added sucessfully.";
    [StatusCode(211)] public const string NoFamilyMembersForApplication = "There are no family members.";
    [StatusCode(212)] public const string ApplicationFamilyMembersList = "There are the List of family members.";
    [StatusCode(213)] public const string ApplicationSubmitted = "Application submitted sucessfully.";

    [StatusCode(214)]
    public const string ApplicationSourceOfIncomeUpdated = "Application source of income updated sucessfully.";

    [StatusCode(215)] public const string NoFamilyMemberFound = "There's no family member found.";
    [StatusCode(216)] public const string FamilyMemberEmploymentAdd = "Family member employment added sucessfully";
    [StatusCode(217)] public const string ApplicationAssetsUpdated = "Application assets updated sucessfully.";

    [StatusCode(218)]
    public const string ApplicationSpecialExpensesUpdated = "Application special expenses updated sucessfully.";

    [StatusCode(219)]
    public const string ApplicationAffordabilityUpdated = "Application affordability updated sucessfully.";

    [StatusCode(220)] public const string ApplicationAccessibilityDetailsUpdated =
        "Application accessibility details updated sucessfully.";

    [StatusCode(221)]
    public const string ApplicationShelterDetailsUpdated = "Application shelter details updated sucessfully.";

    [StatusCode(222)]
    public const string ApplicationMilitaryDetailsUpdated = "Application military details updated sucessfully.";

    [StatusCode(223)] public const string ApplicationOtherExpensesDetailsUpdated =
        "Application other expenses details updated sucessfully.";

    [StatusCode(224)] public const string PreEligibilitySucess =
        "Good news! It looks like you may be eligible for housing assistance.";

    [StatusCode(225)]
    public const string PreEligibilityFail = "Sorry! It looks like you may not be eligible for housing assistance.";

    [StatusCode(226)]
    public const string ProfileSetUpPrimaryDetailsUpdated = "Profile setup primary details update dsucessfully.";

    [StatusCode(227)] public const string ProfileSetUpSecondaryDetailsUpdated =
        "Profile setup secondary details update dsucessfully.";

    [StatusCode(228)] public const string ApplicationHOHNotFound = "Sorry! there's no HOH for this application.";
    [StatusCode(229)] public const string ApplicantHadEmergencyContactPerosn = "Had emergency contact person updated.";

    [StatusCode(230)]
    public const string ApplicantEmergencyContactPersonUpdated = "Applicant emergency contact person updated.";

    [StatusCode(231)]
    public const string ApplicantEmergencyContactPersonAdded = "Applicant emergency contact person created.";

    [StatusCode(232)] public const string SSNAlreadyExist = "SSN already exist.";
    [StatusCode(233)] public const string ListOfEligiblePrograms = "List of eligible programs.";

    [StatusCode(234)] public const string SubmitEligibleProgramsSelectedbyApplicant =
        "Submitted the list of eligible programs selected by applicant.";

    [StatusCode(235)] public const string ApplicationEligibilityRan = "Eligibility on Application ran sucessfully.";

    [StatusCode(236)]
    public const string NoAssetsBackAccount = "No household assets back account present in the database.";

    [StatusCode(237)]
    public const string AssetsBackAccountAdded = "Household assets back account details created successfully.";

    [StatusCode(238)]
    public const string AssetsBackAccountUpdated = "Household assets back account details updated successfully.";

    [StatusCode(239)] public const string NoAssetCashes = "No household assets cash details present in the database.";
    [StatusCode(240)] public const string AssetCashesSaved = "Household assets cash details saved successfully.";

    [StatusCode(241)]
    public const string NoAssetInvestment = "No household assets investment details present in the database.";

    [StatusCode(242)]
    public const string AssetInvestmentSaved = "Household assets investment details saved successfully.";

    [StatusCode(243)] public const string NoRecordFound = "There's no records found.";
    [StatusCode(244)] public const string SavedSuccessfully = "Saved successfully.";

    [StatusCode(245)] public const string FamilyMembersEmploymentDetailsNoRecordsFound =
        "There's no records found for family member employment details.";

    [StatusCode(246)] public const string GetFamilyMembersEmploymentDetails =
        "These are the details for list of family member employment details.";

    [StatusCode(247)] public const string DeletedSuccessfully = "Deleted successfully.";

    [StatusCode(248)]
    public const string FamilyMemberSelfEmploymentAdd = "Family member self employment added sucessfully";

    [StatusCode(249)] public const string FamilyMembersSelfEmploymentDetailsNoRecordsFound =
        "There's no records found for family member self employment details.";

    [StatusCode(250)] public const string GetFamilySelfMembersEmploymentDetails =
        "These are the details for list of family member self employment details.";

    [StatusCode(251)]
    public const string FamilyMemberOvertimeBonusAdd = "Family member Overtime Bonus added sucessfully";

    [StatusCode(252)] public const string FamilyMemberClaimsIncomeOvertimeBonusDetailsNoRecordsFound =
        "There's no records found for family member overtime bonus details.";

    [StatusCode(253)] public const string FamilyMemberClaimsIncomeOvertimeBonusDetails =
        "These are the details for list of family member overtime bonus details.";

    [StatusCode(254)] public const string FamilyMemberSocialSecurityIncomeAdd =
        "Family member social security income added sucessfully";

    [StatusCode(255)] public const string FamilyMemberClaimsIncomeSocialSecurityDetailsNoRecordsFound =
        "There's no records found for family member social security details.";

    [StatusCode(256)] public const string FamilyMemberClaimsIncomeSocialSecurityDetails =
        "These are the details for list of family member social security details.";

    [StatusCode(257)] public const string FamilyMemberSupplementalSecurityIncomeAdd =
        "Family member supplemental security income added sucessfully";

    [StatusCode(258)] public const string FamilyMemberClaimsIncomeSupplementalSecurityDetailsNoRecordsFound =
        "There's no records found for family member supplemental security income details.";

    [StatusCode(259)] public const string FamilyMemberClaimsIncomeSupplementalSecurityDetails =
        "These are the details for list of family member supplemental security details.";

    [StatusCode(260)] public const string FamilyMemberChildSupportIncomeAdd =
        "Family member child support income added sucessfully";

    [StatusCode(261)] public const string FamilyMemberClaimsIncomeChildSupportDetailsNoRecordsFound =
        "There's no records found for family member child support income details.";

    [StatusCode(262)] public const string FamilyMemberClaimsIncomeChildSupportDetails =
        "These are the details for list of family member child support details.";

    [StatusCode(263)]
    public const string FamilyMemberPensionIncomeAdd = "Family member pension income added sucessfully";

    [StatusCode(264)] public const string FamilyMemberClaimsIncomePensionDetailsNoRecordsFound =
        "There's no records found for family member pension income details.";

    [StatusCode(265)] public const string FamilyMemberClaimsIncomePensionDetails =
        "These are the details for list of family member pension details.";

    [StatusCode(266)] public const string FamilyMemberMilitaryPayAdd = "Family member military pay added sucessfully";

    [StatusCode(267)] public const string FamilyMemberClaimsIncomeMilitaryPayDetailsNoRecordsFound =
        "There's no records found for family member military pay income details.";

    [StatusCode(268)] public const string FamilyMemberClaimsIncomeMilitaryPayDetails =
        "These are the details for list of family member military pay details.";

    [StatusCode(269)]
    public const string FamilyMemberUnearnedBenifitsAdd = "Family member unearned benifits added sucessfully";

    [StatusCode(270)] public const string FamilyMemberClaimsIncomeUnearnedBenifitNoRecordsFound =
        "There's no records found for family member Unearned Benifit income details.";

    [StatusCode(271)] public const string FamilyMemberClaimsIncomeUnearnedBenifitDetails =
        "These are the details for list of family member Unearned Benifit details.";

    [StatusCode(272)]
    public const string FamilyMemberGiftContributionAdd = "Family member gift contribution added sucessfully";

    [StatusCode(273)] public const string FamilyMemberClaimsIncomeGiftContributionNoRecordsFound =
        "There's no records found for family member Gift Contribution income details.";

    [StatusCode(274)] public const string FamilyMemberClaimsIncomeGiftContributionDetails =
        "These are the details for list of family member Gift Contribution details.";

    [StatusCode(275)] public const string FamilyMemberPHAIncomeAdd = "Family member PHA income added sucessfully";

    [StatusCode(276)] public const string FamilyMemberClaimsIncomePhaNoRecordsFound =
        "There's no records found for family member pha income details.";

    [StatusCode(277)] public const string FamilyMemberClaimsIncomePhaDetails =
        "These are the details for list of family member Pha details.";

    [StatusCode(278)] public const string FamilyMemberWelfareBenifitIncomeAdd =
        "Family member welfare benifit income added sucessfully";

    [StatusCode(279)] public const string FamilyMemberClaimsIncomeWelfareBenifitNoRecordsFound =
        "There's no records found for family member Welfare Benifit income details.";

    [StatusCode(280)] public const string FamilyMemberClaimsIncomeWelfareBenifitDetails =
        "These are the details for list of family member Welfare Benifit details.";

    [StatusCode(281)] public const string FamilyMemberImputedWelfareIncomeAdd =
        "Family member imputed welfare income added sucessfully";

    [StatusCode(282)] public const string FamilyMemberClaimsIncomeImputedWelfareNoRecordsFound =
        "There's no records found for family member imputed Welfare income details.";

    [StatusCode(283)] public const string FamilyMemberClaimsIncomeImputedWelfareDetails =
        "These are the details for list of family member imputed Welfare details.";

    [StatusCode(284)] public const string FamilyMemberGeneralAssistanceIncomeAdd =
        "Family member general assistance income added sucessfully";

    [StatusCode(285)] public const string FamilyMemberClaimsIncomeGeneralAssistanceNoRecordsFound =
        "There's no records found for family member general assistance income details.";

    [StatusCode(286)] public const string FamilyMemberClaimsIncomeGeneralAssistanceDetails =
        "These are the details for list of family member general assistance details.";

    [StatusCode(287)]
    public const string FamilyMemberAlimonyIncomeAdd = "Family member alimony income added sucessfully";

    [StatusCode(289)] public const string FamilyMemberClaimsIncomeAlimonyNoRecordsFound =
        "There's no records found for family member alimony income details.";

    [StatusCode(290)] public const string FamilyMemberClaimsIncomeAlimonyDetails =
        "These are the details for list of family member alimony details.";

    [StatusCode(291)]
    public const string FamilyMemberFosterCareIncomeAdd = "Family member foster care added sucessfully";

    [StatusCode(292)] public const string FamilyMemberClaimsIncomeFosterCareNoRecordsFound =
        "There's no records found for family member fooster care income details.";

    [StatusCode(293)] public const string FamilyMemberClaimsIncomeFosterCareDetails =
        "These are the details for list of family member fooster care details.";

    [StatusCode(294)] public const string FamilyMemberStudentAssistanceIncomeAdd =
        "Family member student assistance added sucessfully";

    [StatusCode(295)] public const string FamilyMemberClaimsIncomeStudentAssistanceNoRecordsFound =
        "There's no records found for family member student assistance income details.";

    [StatusCode(296)] public const string FamilyMemberClaimsIncomeStudentAssistanceDetails =
        "These are the details for list of family member student assistance details.";

    [StatusCode(297)] public const string FamilyMemberMedicalReimbursementIncomeAdd =
        "Family member Medical Reimbursement added sucessfully";

    [StatusCode(298)] public const string FamilyMemberClaimsIncomeMedicalReimbursementNoRecordsFound =
        "There's no records found for family member Medical Reimbursement income details.";

    [StatusCode(299)] public const string FamilyMemberClaimsIncomeMedicalReimbursementDetails =
        "These are the details for list of family member Medical Reimbursement details.";

    [StatusCode(300)]
    public const string FamilyMemberIndianTrustIncomeAdd = "Family member indian trust added sucessfully";

    #region Applicant Module Model Validation

    //For ProfileSetupDetailsDto.cs
    [StatusCode(301)] public const string SSNValidation =
        "Social security number should be maximum of 20 characters and minimum of 1 character.";

    [StatusCode(302)] public const string SSNRequired = "Social security number required.";
    [StatusCode(303)] public const string DOBRequired = "Date of birth required.";
    [StatusCode(304)] public const string GenderRequired = "Gender required.";
    [StatusCode(305)] public const string InvalidGenderId = "Invalid gender id";
    [StatusCode(306)] public const string RelationshipRequired = "Relationship  required.";
    [StatusCode(307)] public const string InvalidRelationshipId = "Invalid relationship id";
    [StatusCode(308)] public const string CitizenshippRequired = "Citizenship  required.";
    [StatusCode(309)] public const string InvalidCitizenshipId = "Invalid citizenship id";
    [StatusCode(310)] public const string IsDisabledRequired = "Is this Individual Disabled? required.";
    [StatusCode(311)] public const string IsFulltimeStudentRequired = "Is this Individual a Student? required.";

    [StatusCode(312)] public const string IsStudentNextyearRequired =
        "Will this Individual become a Student within the next year? required.";

    [StatusCode(313)] public const string LifetimeSexOffenderRegRequired =
        "Is this individual subject to a lifetime sex registration? required.";

    [StatusCode(314)]
    public const string RaceEthinicJSONRequired = "Check any races that apply for this individual required.";

    //For ApplicantAddressDetailsDto.cs
    [StatusCode(315)]
    public const string ApplicantMobileNumberLength = "Applicant mobile number must be a 10 characters.";

    [StatusCode(316)] public const string ApplicantMobileNumberformat = "check with the mobile number format.";

    [StatusCode(317)]
    public const string ApplicantlandlineNumberLength = "Applicant landline number must be a 10 characters.";

    [StatusCode(318)] public const string ApplicantlandlineNumberformat = "check with the landline number format.";

    [StatusCode(319)] public const string ApplicantAptNumberLength =
        "Applicant Apartment number must be a maximum of 50 characters.";

    [StatusCode(320)] public const string ApplicantStreetAddressLength =
        "Applicant Street Address must be a maximum of 100 characters.";

    [StatusCode(321)] public const string ApplicantCityLength =
        "Applicant city must be a maximum of 100 characters and minimum of 1 character.";

    [StatusCode(322)] public const string ApplicantCityRequired = "Applicant city field is required.";
    [StatusCode(323)] public const string ApplicantStateRequired = "Applicant state field is required.";
    [StatusCode(324)] public const string InvalidApplicantStateId = "Invalid Applicant State id";

    [StatusCode(325)] public const string ApplicantZipCodeLength =
        "Applicant zip code must be a maximum of 15 characters and minimum of 5 character.";

    [StatusCode(326)] public const string ApplicantZipCodeRequired = "Applicant Zip code field is required.";

    //For EmergencyContactPersonDetailsDto.cs
    [StatusCode(327)] public const string EmergencyContactPersonFirstNameLength =
        "Emergency contact person first name should be a maximum of 50 characters and minimum of 1 character.";

    [StatusCode(328)] public const string EmergencyContactPersonFirstNameRequired =
        "Emergency contact person first name is required.";

    [StatusCode(329)] public const string EmergencyContactPersonFirstNameformate =
        "Emergency contact person first name formate is invalid.";

    [StatusCode(330)] public const string EmergencyContactPersonLastNameLength =
        "Emergency contact person last name should be a maximum of 50 characters.";

    [StatusCode(331)] public const string EmergencyContactPersonRelationshipIdRequired =
        "Emergency contact person relationship is required.";

    [StatusCode(332)] public const string EmergencyContactPersonRelationshipIdRange =
        "Emergency contact person relationship id range.";

    [StatusCode(333)] public const string EmergencyContactPersonEmailLength =
        "Emergency contact person email should be a maximum of 200 characters and minimum of 7 characters.";

    [StatusCode(334)]
    public const string EmergencyContactPersonEmailRequired = "Emergency contact person email is required.";

    [StatusCode(335)]
    public const string EmergencyContactPersonEmailFormat = "Emergency contact person email format is invalid.";

    [StatusCode(336)] public const string EmergencyContactPersonTelephoneNumberLength =
        "Emergency contact person telephone number should be a maximum of 10 characters and minimum of 10 characters.";

    [StatusCode(337)] public const string EmergencyContactPersonTelephoneNumberRequired =
        "Emergency contact person telephone number is required.";

    [StatusCode(338)] public const string EmergencyContactPersonTelephoneNumberFormat =
        "Emergency contact person telephone number format is invalid.";

    [StatusCode(339)] public const string EmergencyContactPersonCommunicateToThisContactRequired =
        "Emergency contact person to communicate is required.";

    //For HouseholdDto.cs
    [StatusCode(340)] public const string HouseholdIsPrimaryHOHRequired = "House hold is primary Hoh is required.";

    [StatusCode(341)] public const string HouseholdFirstNameLength =
        "Household first name should be a maximum of 50 characters and minimum of 1 character.";

    [StatusCode(342)] public const string HouseholdFirstNameRequired = "Household first name is required.";
    [StatusCode(343)] public const string HouseholdFirstNameFormate = "Household first name formate is invalid.";

    [StatusCode(344)] public const string HouseholdlastNameLength =
        "Household last name should be a maximum of 50 characters and minimum of 1 character.";

    [StatusCode(345)] public const string HouseholdLastNameRequired = "Household last name is required.";
    [StatusCode(346)] public const string HouseholdLastNameFormate = "Household last name formate is invalid.";

    [StatusCode(347)]
    public const string HouseholdMiddleNameLength = "Household middle name should be a maximum of 50 characters.";

    [StatusCode(348)]
    public const string HouseholdMaidenNameLength = "Household maiden name should be a maximum of 20 characters.";

    [StatusCode(349)] public const string HouseholdAnyDocumentRequired = "Household any document required.";

    [StatusCode(350)] public const string HouseholdDobCertificateUrlLength =
        "Household dob certificate url should be a maximum of 200 characters.";

    [StatusCode(351)]
    public const string HouseholdDobCertificateUrlRequired = "Household dob certificate url required.";

    [StatusCode(352)] public const string HouseholdLicenseUrlLength =
        "Household license url should be a maximum of 200 characters.";

    [StatusCode(353)] public const string HouseholdLicenseUrlRequired = "Household license url required.";

    [StatusCode(354)] public const string HouseholdSocialSecurityCardUrlLength =
        "Household social security card url should be a maximum of 200 characters.";

    [StatusCode(355)]
    public const string HouseholdSocialSecurityCardUrlRequired = "Household social security card url required.";

    [StatusCode(356)]
    public const string HouseholdIsFileClearLegibleRequired = "Household file clear legible required.";

    //For HouseholdEmploymentDto.cs
    [StatusCode(357)] public const string HouseholdEmploymentFamilyMemberIdRequired =
        "Household employment family member id is required.";

    [StatusCode(358)] public const string HouseholdEmploymentFamilyMemberIdRange =
        "Household employment family member id range is invalid.";

    [StatusCode(359)] public const string HouseholdEmploymentIncomeAmountRequired =
        "Household employment income amount is required.";

    [StatusCode(360)] public const string HouseholdEmploymentPaymentFrequencyTypeIdRequired =
        "Household employment payment frequency type id is required.";

    [StatusCode(361)] public const string HouseholdEmploymentPaymentFrequencyTypeIdRange =
        "Household employment payment frequency type id range is invalid.";

    [StatusCode(362)] public const string HouseholdEmploymentEmployerNameLength =
        "Household employment employer name should be a maximum of 100 characters.";

    [StatusCode(363)] public const string HouseholdEmploymentEmployerNameRequired =
        "Household employment employer name is required.";

    [StatusCode(364)] public const string HouseholdEmploymentJobTitleLength =
        "Household employment job title should be a maximum of 100 characters.";

    [StatusCode(365)] public const string HouseholdEmploymentEmployerAddress1Length =
        "Household employment employer address 1 should be a maximum of 100 characters.";

    [StatusCode(366)] public const string HouseholdEmploymentEmployerAddress1Required =
        "Household employment employer address 1 is required.";

    [StatusCode(367)] public const string HouseholdEmploymentEmployerAddress2Length =
        "Household employment employer address 2 should be a maximum of 100 characters.";

    [StatusCode(368)] public const string HouseholdEmploymentEmployerCityLength =
        "Household employment employer city should be a maximum of 100 characters.";

    [StatusCode(369)] public const string HouseholdEmploymentEmployerCityRequired =
        "Household employment employer city is required.";

    [StatusCode(370)] public const string HouseholdEmploymentEmployerStateIdRequired =
        "Household employment employer state id is required.";

    [StatusCode(371)] public const string HouseholdEmploymentEmployerStateIdRange =
        "Household employment employer state id range is invalid.";

    [StatusCode(372)] public const string HouseholdEmploymentEmployerEmployerZipcodeLength =
        "Household employment employer zipcode should be a maximum of 10 characters.";

    [StatusCode(373)] public const string HouseholdEmploymentEmployerZipcodeRequired =
        "Household employment employer zipcode is required.";

    [StatusCode(374)] public const string HouseholdEmploymentEmployerPhoneLength =
        "Household employment employer phone should be a maximum of 10 characters and minimum of 10 characters.";

    [StatusCode(375)] public const string HouseholdEmploymentEmployerFaxLength =
        "Household employment employer fax should be a maximum of 10 characters and minimum of 10 characters.";

    #endregion

    [StatusCode(376)] public const string NoActiveProgramFound = "There's no active programs found.";

    [StatusCode(377)] public const string FamilyMemberClaimsIncomeIndianTrustNoRecordsFound =
        "There's no records found for family member indian trust income details.";

    [StatusCode(378)] public const string FamilyMemberClaimsIncomeIndianTrustDetails =
        "These are the details for list of family member indian trust details.";

    [StatusCode(379)]
    public const string FamilyMemberFederalWageIncomeAdd = "Family member federal wage added sucessfully";

    [StatusCode(380)] public const string FamilyMemberClaimsIncomeFederalWageNoRecordsFound =
        "There's no records found for family member federal wage income details.";

    [StatusCode(381)] public const string FamilyMemberClaimsIncomeFederalWageDetails =
        "These are the details for list of family member federal wage details.";

    [StatusCode(382)] public const string FamilyMemberOtherIncomeAdd = "Family member other income added sucessfully";

    [StatusCode(383)] public const string FamilyMemberClaimsIncomeOtherIncomeNoRecordsFound =
        "There's no records found for family member other income details.";

    [StatusCode(384)] public const string FamilyMemberClaimsIncomeOtherIncomeDetails =
        "These are the details for list of family member other income details.";

    [StatusCode(385)]
    public const string ApplicationDetailsNoIncomeSources = "These are no income sources for this application.";

    [StatusCode(386)] public const string FamilyMembersIncomeDetailsNoRecordsFound =
        "There's no records found for family member income details.";

    [StatusCode(387)] public const string FamilyMembersIncomeDetails =
        "These are the details for list of family member income details.";

    [StatusCode(389)]
    public const string FamilyMemberSelfEmploymentUpdate = "Family member self employment updated sucessfully";

    [StatusCode(390)]
    public const string FamilyMemberSelfEmploymentNotfound = "Family member self employment not found";

    [StatusCode(391)]
    public const string FamilyMemberSelfEmploymentDelete = "Family member self employment deleted sucessfully";

    [StatusCode(392)] public const string FamilyMemberEmploymentUpdate = "Family member employment updated sucessfully";
    [StatusCode(393)] public const string FamilyMemberEmploymentNotfound = "Family member employment not found";
    [StatusCode(393)] public const string FamilyMemberEmploymentDelete = "Family member employment deleted sucessfully";
    [StatusCode(394)] public const string FamilyMemberHOHDetailsNotFound = "Family member HOH details not found";
    [StatusCode(399)] public const string FamilyMemberHOHDetails = "These are the Family member HOH details";
    [StatusCode(394)] public const string ApplicationProxyFlagUpdated = "Applicant Proxy flag updated sucessfully.";
    [StatusCode(395)] public const string ApplicationHouseholdMemberUpdated = "Household member updated sucessfully.";
    [StatusCode(396)] public const string ApplicationHouseholdMemberNotFound = "Household member didn't found.";
    [StatusCode(397)] public const string FamilyMemberNotfound = "Family member not found";
    [StatusCode(398)] public const string FamilyMemberDelete = "Family member  deleted sucessfully";
    [StatusCode(399)] public const string GoogleAddressAPIId = "Invalid Applicant Address Id.";
    [StatusCode(400)] public const string AddressAddressLine1 = "maximum of 500 characters.";

    #endregion

    #region User 401-500

    [StatusCode(401)] public const string EmailFormatNotValid = "Check with the email format.";
    [StatusCode(402)] public const string EmailRequired = "The Email field is required.";

    [StatusCode(403)] public const string EmailLengthNotValid =
        "The Email must be maximum of 100 characters and minimum of 5 characters.";

    [StatusCode(404)] public const string PasswordLengthNotValid =
        "The password must be maximum of 15 characters and minimum of 8 characters.";

    [StatusCode(405)] public const string PasswordRequired = "The Password field is required.";

    [StatusCode(406)] public const string PasswordFormatNotValid =
        "Check with the formate, should contain atleast one special, alphabetical, capital case and numerical character.";

    [StatusCode(404)] public const string PhonenumberLengthNotValid = "The phone number must be a 10 characters.";
    [StatusCode(405)] public const string PhonenumberRequired = "The phone number field is required.";
    [StatusCode(406)] public const string PhonenumberFormatNotValid = "check with the phone number format.";

    [StatusCode(407)] public const string FirstNameLengthNotValid =
        "The First Name must be maximum of 40 characters and minimum of 1 character.";

    [StatusCode(408)] public const string FirstNameRequired = "The First Name field is required.";

    [StatusCode(409)]
    public const string MiddleNameLengthNotValid = "The Middle Name must be maximum of 40 characters.";

    [StatusCode(410)] public const string LastNameLengthNotValid =
        "The Last Name must be maximum of 40 characters and minimum of 1 character.";

    [StatusCode(411)] public const string LastNameRequired = "The Last Name field is required.";

    [StatusCode(412)] public const string RoleLengthNotValid =
        "The Role Name must be maximum of 20 characters and minimum of 1 character.";

    [StatusCode(413)] public const string RoleRequired = "The Role field is required.";
    [StatusCode(414)] public const string LocaleLengthNotValid = "The Locale must be a single character.";
    [StatusCode(415)] public const string LocaleRequired = "The Locale field is required.";

    [StatusCode(416)]
    public const string ProfileSetUpPrimaryDetailsUpdateFail = "Profile setup primary details update Failed.";


    /// <summary>
    /// Common Format Constant
    /// </summary>
    [StatusCode(500)] public const string FormatNotValid = "Check with the format.";

    #endregion

    #region 501-600

    [StatusCode(501)]
    public const string FamilyMemberOvertimeBonusUpdate = "Family member Overtime Bonus updated sucessfully";

    [StatusCode(502)] public const string FamilyMemberOvertimeBonusNotfound = "Family member Overtime Bonus not found";

    [StatusCode(503)]
    public const string FamilyMemberOvertimeBonusDelete = "Family member Overtime Bonus deleted sucessfully";

    [StatusCode(504)] public const string FamilyMemberSocialSecurityIncomeUpdate =
        "Family member Social Security Income updated sucessfully";

    [StatusCode(505)] public const string FamilyMemberSocialSecurityIncomeNotfound =
        "Family member Social Security Income not found";

    [StatusCode(506)] public const string FamilyMemberSocialSecurityIncomeDelete =
        "Family member Social Security Income deleted sucessfully";

    [StatusCode(507)] public const string FamilyMemberSupplementalSecurityIncomeUpdate =
        "Family member supplemental Security Income updated sucessfully";

    [StatusCode(508)] public const string FamilyMemberSupplementalSecurityIncomeNotfound =
        "Family member supplemental Security Income not found";

    [StatusCode(509)] public const string FamilyMemberSupplementalSecurityIncomeDelete =
        "Family member supplemental Security Income deleted sucessfully";

    [StatusCode(510)] public const string FamilyMemberChildSupportIncomeUpdate =
        "Family member child support Income updated sucessfully";

    [StatusCode(511)]
    public const string FamilyMemberChildSupportIncomeNotfound = "Family member child support Income not found";

    [StatusCode(512)] public const string FamilyMemberChildSupportIncomeDelete =
        "Family member child support Income deleted sucessfully";

    [StatusCode(513)]
    public const string FamilyMemberPensionIncomeUpdate = "Family member Pension Income updated sucessfully";

    [StatusCode(514)] public const string FamilyMemberPensionIncomeNotfound = "Family member Pension Income not found";

    [StatusCode(515)]
    public const string FamilyMemberPensionIncomeDelete = "Family member Pension Income deleted sucessfully";

    [StatusCode(516)] public const string FamilyMemberMilitaryPayIncomeUpdate =
        "Family member military pay Income updated sucessfully";

    [StatusCode(517)]
    public const string FamilyMemberMilitaryPayIncomeNotfound = "Family member military pay Income not found";

    [StatusCode(518)] public const string FamilyMemberMilitaryPayIncomeDelete =
        "Family member military pay Income deleted sucessfully";

    [StatusCode(519)] public const string FamilyMemberUnearnedBenifitsIncomeUpdate =
        "Family member Unearned Benifits Income updated sucessfully";

    [StatusCode(520)] public const string FamilyMemberUnearnedBenifitsIncomeNotfound =
        "Family member Unearned Benifits Income not found";

    [StatusCode(521)] public const string FamilyMemberUnearnedBenifitsIncomeDelete =
        "Family member Unearned Benifits Income deleted sucessfully";

    [StatusCode(522)] public const string FamilyMemberGiftContributionIncomeUpdate =
        "Family member Gift contribution Income updated sucessfully";

    [StatusCode(523)] public const string FamilyMemberGiftContributionIncomeNotfound =
        "Family member Gift contribution Income not found";

    [StatusCode(524)] public const string FamilyMemberGiftContributionIncomeDelete =
        "Family member Gift contribution Income deleted sucessfully";

    [StatusCode(525)] public const string FamilyMemberPHAIncomeUpdate = "Family member PHA Income updated sucessfully";
    [StatusCode(526)] public const string FamilyMemberPHAIncomeNotfound = "Family member PHA Income not found";
    [StatusCode(527)] public const string FamilyMemberPHAIncomeDelete = "Family member PHA Income deleted sucessfully";

    [StatusCode(528)] public const string FamilyMemberWelfareBenifitsIncomeUpdate =
        "Family member Welfare Benifits Income updated sucessfully";

    [StatusCode(529)] public const string FamilyMemberWelfareBenifitsIncomeNotfound =
        "Family member Welfare Benifits Income not found";

    [StatusCode(530)] public const string FamilyMemberWelfareBenifitsIncomeDelete =
        "Family member Welfare Benifits Income deleted sucessfully";

    [StatusCode(528)] public const string FamilyMemberImputedWelfareIncomeUpdate =
        "Family member Imputed welfare Income updated sucessfully";

    [StatusCode(529)] public const string FamilyMemberImputedWelfareIncomeNotfound =
        "Family member Imputed welfare Income not found";

    [StatusCode(530)] public const string FamilyMemberImputedWelfareIncomeDelete =
        "Family member Imputed welfare Income deleted sucessfully";

    [StatusCode(531)] public const string FamilyMemberGeneralAssistanceIncomeUpdate =
        "Family member general assistance Income updated sucessfully";

    [StatusCode(532)] public const string FamilyMemberGeneralAssistanceIncomeNotfound =
        "Family member general assistance Income not found";

    [StatusCode(533)] public const string FamilyMemberGeneralAssistanceIncomeDelete =
        "Family member general assistance Income deleted sucessfully";

    [StatusCode(534)]
    public const string FamilyMemberAlimonyIncomeUpdate = "Family member alimony Income updated sucessfully";

    [StatusCode(535)] public const string FamilyMemberAlimonyIncomeNotfound = "Family member alimony Income not found";

    [StatusCode(536)]
    public const string FamilyMemberAlimonyIncomeDelete = "Family member alimony Income deleted sucessfully";

    [StatusCode(537)] public const string FamilyMemberFosterCareIncomeUpdate =
        "Family member FosterCare Income updated sucessfully";

    [StatusCode(538)]
    public const string FamilyMemberFosterCareIncomeNotfound = "Family member FosterCare Income not found";

    [StatusCode(539)] public const string FamilyMemberFosterCareIncomeDelete =
        "Family member FosterCare Income deleted sucessfully";

    [StatusCode(540)] public const string FamilyMemberStudentAssistanceIncomeUpdate =
        "Family member Student Assistance Income updated sucessfully";

    [StatusCode(541)] public const string FamilyMemberStudentAssistanceIncomeNotfound =
        "Family member Student Assistance Income not found";

    [StatusCode(542)] public const string FamilyMemberStudentAssistanceIncomeDelete =
        "Family member Student Assistance Income deleted sucessfully";

    [StatusCode(543)] public const string FamilyMemberMedicalReimbursementIncomeUpdate =
        "Family member Medical Reimbursement Income updated sucessfully";

    [StatusCode(544)] public const string FamilyMemberMedicalReimbursementIncomeNotfound =
        "Family member Medical Reimbursement Income not found";

    [StatusCode(545)] public const string FamilyMemberMedicalReimbursementIncomeDelete =
        "Family member Medical Reimbursement Income deleted sucessfully";

    [StatusCode(546)] public const string FamilyMemberIndianTrustIncomeUpdate =
        "Family member Indian Trust Income updated sucessfully";

    [StatusCode(547)]
    public const string FamilyMemberIndianTrustIncomeNotfound = "Family member Indian Trust Income not found";

    [StatusCode(548)] public const string FamilyMemberIndianTrustIncomeDelete =
        "Family member Indian Trust Income deleted sucessfully";

    [StatusCode(549)] public const string FamilyMemberFederalWageIncomeUpdate =
        "Family member federal wage Income updated sucessfully";

    [StatusCode(550)]
    public const string FamilyMemberFederalWageIncomeNotfound = "Family member federal wage Income not found";

    [StatusCode(551)] public const string FamilyMemberFederalWageIncomeDelete =
        "Family member federal wage Income deleted sucessfully";

    [StatusCode(552)]
    public const string FamilyMemberOtherIncomeUpdate = "Family member other Income updated sucessfully";

    [StatusCode(553)] public const string FamilyMemberOtherIncomeNotfound = "Family member oter Income not found";

    [StatusCode(554)]
    public const string FamilyMemberOtherIncomeDelete = "Family member other Income deleted sucessfully";

    [StatusCode(555)] public const string FamilyMembersClaimsListNoRecordsFound =
        "There's no records found for family member claims details.";

    [StatusCode(556)]
    public const string FamilyMembersClaimsList = "These are the details for list of family member claims.";

    [StatusCode(557)] public const string ProfileSummaryNotFound = "These are the profile summary found for the user.";


    [StatusCode(558)] public const string GetApplicationHasFamilyMembers = "Here's the result for Has Family members";
    [StatusCode(559)] public const string GetApplicationHasFamilyMembersNoData = " No data for Has Family members";
    [StatusCode(560)] public const string GetApplicantProfilSetupDone = " Profile Setup already Done.";

    [StatusCode(561)] public const string ApplicationProfileSetupFlagUpdated =
        "Applicant Profile Setup Done flag updated sucessfully.";

    [StatusCode(562)] public const string GetApplicantProfilSetupNotDone = " Profile Setup not yet completed.";


    [StatusCode(563)]
    public const string SkipClaimsSubTypeIdDataExist = "Still there's a data for this claim subtypeId.";

    [StatusCode(564)] public const string SkipClaimsSubTypeIdSucess = "Claim subtype id skipped sucessfully.";
    [StatusCode(565)] public const string SkipClaimsSubTypeIdNotFound = "Claim subtype id not found.";
    [StatusCode(566)] public const string ClaimsSubTypePKIdDeleted = "Claim subtype Pk id deleted sucessfully.";
    [StatusCode(567)] public const string NoApplicationAddress = "Application Address not found.";
    [StatusCode(568)] public const string ApplicationAddress = "These are the details of Application Address";
    [StatusCode(569)] public const string SMSSentSuccess = " SMS Sent Successfully .";
    [StatusCode(570)] public const string SMSnotSentSuccess = " SMS not Sent Successfully .";
    [StatusCode(571)] public const string OtpIncorrect = " Entered OTP  is not correct.";
    [StatusCode(572)] public const string Otpcorrect = " Login with OTP  is Success.";
    [StatusCode(573)] public const string OtpExpired = " OTP Expired.";
    [StatusCode(574)] public const string EmailnotSentSuccess = " Email not Sent.";
    [StatusCode(575)] public const string EmailSentSuccess = " Otp sent successfully to Email.";

    [StatusCode(576)]
    public const string usp_GetListofApplicantProgramWaitlistByApplicationId = "List of Applican Proram Waitlist";

    [StatusCode(577)] public const string usp_GetListofApplicantProgramWaitlistByApplicationIdNotFound =
        "List of Applican Proram Waitlist Not found";

    [StatusCode(578)] public const string VoucherDetailsNotFound = "Voucher Details Not Found";
    [StatusCode(579)] public const string ProgramWaitlistNotFound = "ProgramWaitlist Not Found ";
    [StatusCode(580)] public const string AddRequestforChange = "Add Request for Change sucess ";
    [StatusCode(581)] public const string usp_GetListofRFCRequestForChange = "List of Request for change";

    [StatusCode(582)]
    public const string usp_GetListofRFCRequestForChangeNotFound = "List of request for change Not found";

    [StatusCode(583)] public const string usp_InsertRFCDynamicallyDtoResponse = "Rfc record inserted sucessfully.";

    [StatusCode(584)]
    public const string RFCApplicationHouseholdMemberAdded = "RFC Household member added sucessfully.";

    [StatusCode(585)]
    public const string RFCApplicationHouseholdMemberUpdated = "RFC Household member updated sucessfully.";

    [StatusCode(586)]
    public const string usp_InsertRFCDeleteDynamicallyDtoResponse = "Rfc record inserted sucessfully.";

    [StatusCode(587)] public const string ApplicantProgramWaitlistStatusUpdatedSucess =
        "Applicant Program Waitlist Status updated.";

    [StatusCode(588)]
    public const string ApplicantProgramWaitlistStatusUpdatedFailed = "Applicant Program Waitlist Status Failed.";

    [StatusCode(589)] public const string ApplicantProgramWaitlistlistSucess = "Applicant Program Waitlist list.";

    [StatusCode(590)]
    public const string ApplicantProgramWaitlistlistFailed = "Applicant Program Waitlist list Failed.";

    [StatusCode(591)]
    public const string usp_InsertAssetRFCDynamicallyDtoResponse = "Rfc Asset record inserted sucessfully.";

    [StatusCode(592)]
    public const string FamilymemberClaimsMappingwithNoDocs = "Familymember Claims Mapping with No Docs.";

    [StatusCode(593)]
    public const string ZeroIncomeFormQuestion = "These are the details of Zero Income form questions";

    [StatusCode(594)] public const string ZeroIncomeFormQuestionNoData =
        "There is no data available for Zero Income Form Questions";

    [StatusCode(595)] public const string ZeroIncomeFormSaveResponse = "Zero income form submitted successfully.";
    [StatusCode(596)] public const string SubmissionConfirmResponse = "Applicant confirmed his details successfully";
    [StatusCode(597)] public const string RFTAInvitaionSent = "RFTA invitations sent successfully";
    [StatusCode(598)] public const string NoProgramsSelected = "There's no programs found.";

    #endregion

    #region Admin 1001-2000

    [StatusCode(1001)] public const string EmailTemplateCreate = "Email template created sucessfully.";
    [StatusCode(1002)] public const string EmailTemplateUpdate = "Email template updated sucessfully.";
    [StatusCode(1003)] public const string EmailTemplateDelete = "Email template deleted sucessfully.";
    [StatusCode(1004)] public const string EmailTemplateDetails = "Here's the email template details.";
    [StatusCode(1005)] public const string NoEmailTemplateFound = "Sorry! No Email template found.";

    [StatusCode(1006)] public const string EmailTemplateAlreadyThere =
        "Sorry! active email template already there for this Tag in this locale.";

    [StatusCode(1007)] public const string EmailTemplateList = "Here's the email template List.";
    [StatusCode(1008)] public const string SiteSettingsCreate = "Site Setting created sucessfully.";
    [StatusCode(1009)] public const string SiteSettingsUpdate = "Site Setting updated sucessfully.";
    [StatusCode(1010)] public const string SiteSettingsDelete = "Site Setting deleted sucessfully.";
    [StatusCode(1011)] public const string SiteSettingsDetails = "Here's the Site Setting details.";
    [StatusCode(1012)] public const string NoSiteSettingsFound = "Sorry! No Site Setting found.";
    [StatusCode(1013)] public const string SiteSettingsAlreadyThere = "Sorry! active Site Setting already there.";
    [StatusCode(1014)] public const string ProgramCreate = "Program created sucessfully.";
    [StatusCode(1015)] public const string ProgramUpdate = "Program updated sucessfully.";
    [StatusCode(1016)] public const string ProgramDelete = "Program deleted sucessfully.";
    [StatusCode(1017)] public const string ProgramDetails = "Here's the Program details.";
    [StatusCode(1018)] public const string NoProgramFound = "Sorry! No Program found.";
    [StatusCode(1019)] public const string ProgramAlreadyThere = "Sorry! active Program already there.";

    [StatusCode(1020)] public const string InvalidCaseWorker =
        "You are trying to update an rfta which is not assigned to you, sorry try again later";

    [StatusCode(1021)] public const string ChoseCaseWorkerOnly = "Please assign to a case worker only";
    [StatusCode(1022)] public const string ProgramWaitlistCreate = "Program Waitlist created sucessfully.";
    [StatusCode(1023)] public const string ProgramWaitlistUpdate = "Program Waitlist updated sucessfully.";
    [StatusCode(1024)] public const string ProgramWaitlistDelete = "Program Waitlist deleted sucessfully.";
    [StatusCode(1025)] public const string ProgramWaitlistDetails = "Here's the Program Waitlist details.";
    [StatusCode(1026)] public const string NoProgramWaitlistFound = "Sorry! No Program Waitlist found.";

    [StatusCode(1027)]
    public const string ProgramWaitlistAlreadyThere = "Sorry! active Program Waitlist already there.";

    [StatusCode(1028)] public const string ProgramList = "Here's the list of programs";
    [StatusCode(1029)] public const string NoProgramListFound = "No program  list found";
    [StatusCode(1030)] public const string ProgramWailtlistList = "Here's the list of Program Waitlist";
    [StatusCode(1031)] public const string NoProgramWaitlistListFound = "No Program Waitlist list found";
    [StatusCode(1032)] public const string ApplicationHeaderDetails = "Here's the Application Header Details";
    [StatusCode(1033)] public const string ApplicationHeaderDetailsNotFound = "No Application Header Details found";
    [StatusCode(1034)] public const string ApplicantProgramWaitlistNotFound = "No Applicant Program Waitlist found.";

    [StatusCode(1035)]
    public const string ApplicantProgramWaitlistStatusUpdated = "Applicant Program Waitlist Status Updated";

    [StatusCode(1036)] public const string ApplicantProgramWaitlistAssignedUserUpdated =
        "Applicant Program Waitlist Assigned User Updated";

    [StatusCode(1037)] public const string ApplicationNotesAdded = "Application Notes Added.";
    [StatusCode(1038)] public const string ApplicationNotesList = "Application Notes List.";
    [StatusCode(1039)] public const string ApplicationNotesListNotFound = "Application Notes List not found.";
    [StatusCode(1040)] public const string FamilyMemberDetails = "FamilyMember Details.";
    [StatusCode(1041)] public const string FamilyMemberDetailsNotFound = "FamilyMember Details not found.";

    [StatusCode(1042)]
    public const string ApplicantProgramWailtlistList = "Here's the list of Applicant Program Waitlist";

    [StatusCode(1043)]
    public const string NoApplicantProgramWaitlistListFound = "No Applicant Program Waitlist list found";

    [StatusCode(1044)] public const string AdminUsersList = "Admin users list";
    [StatusCode(1045)] public const string AdminUsersListNotFound = "No Admin users Found.";
    [StatusCode(1046)] public const string ApplicationIncomeList = "Here's the list of Applicant income list";
    [StatusCode(1047)] public const string NoApplicationIncomeListFound = "No Applicant income list found";
    [StatusCode(1048)] public const string ApplicationAssignedUserUpdated = "Application Assigned User Updated";

    [StatusCode(1049)] public const string GetApplicantProgramWaitListStatusCountList =
        "Here's the list of Applicant Program Waitlist status count.";

    [StatusCode(1050)] public const string UpdateIsLocked = "Is Locked Updated Sucessfully.";
    [StatusCode(1051)] public const string GetTotalAmountForTheClaim = "Here's the total amount of a claim";

    [StatusCode(1052)]
    public const string GetTotalAmountForTheClaimFailed = "sorry failed! to get the total amount of a claim";

    [StatusCode(1053)] public const string AdminApplicationDocUploaded = "Admin docs uploaded Sucessfully.";
    [StatusCode(1054)] public const string FamilyMemberCHStatusUpdated = "Family member CH Status updated sucessfully";
    [StatusCode(1055)] public const string GetPreviouslyAssignedUser = "Here's the previously assigned user.";

    [StatusCode(1056)]
    public const string GetPreviouslyAssignedUserFailed = "sorry failed! to get the previously assigned user.";

    [StatusCode(1057)] public const string BatchNameAlreadyExist = "Batch name already exist.";

    [StatusCode(1058)]
    public const string AssignedtoBatchSucessfully = "Applications are assigned to batch sucessfully.";

    [StatusCode(1059)]
    public const string UnAssignedFromBatchSucessfully = "Applications are un-assigned from batch sucessfully.";

    [StatusCode(1060)] public const string UnAssignedFromBatchFailed = "Applications un-assigned from batch failed.";
    [StatusCode(1061)] public const string PaymentStandardForVoucherSucess = "Payment Standard For Voucher Sucess.";
    [StatusCode(1061)] public const string PaymentStandardForVoucherFail = "Payment Standard For Voucher failed.";

    [StatusCode(1062)]
    public const string SaveVoucherAffordabilityDetailsInsert = "Voucher Affordability Details Inserted.";

    [StatusCode(1063)]
    public const string SaveVoucherAffordabilityDetailsUpdate = "Voucher Affordability Details Updated";

    [StatusCode(1064)] public const string GetVoucherAffordabilityDetails = "To get Voucher Affordability Details";

    #endregion
}

public static class MessageExtensions
{
    public static int GetStatusCode(this string message)
    {
        var fieldInfo = typeof(Message).GetFields()
            .FirstOrDefault(f => f.GetValue(null).ToString() == message);
        if (fieldInfo != null)
        {
            var attribute = (StatusCodeAttribute)fieldInfo.GetCustomAttribute(typeof(StatusCodeAttribute));
            if (attribute != null)
            {
                return attribute.StatusCode;
            }
        }

        throw new ArgumentException("Unknown message value");
    }

    public static string GetMessage(int statusCode)
    {
        var fields = typeof(Message).GetFields();
        foreach (var field in fields)
        {
            var attribute = (StatusCodeAttribute)field.GetCustomAttribute(typeof(StatusCodeAttribute));
            if (attribute != null && attribute.StatusCode == statusCode)
            {
                return (string)field.GetValue(null);
            }
        }

        throw new ArgumentException("Unknown status code");
    }
}

public class StringLengthValidationAttribute : ValidationAttribute
{
    private readonly int _maxLength;
    private readonly int _statusCode;
    private readonly string _errorMessage;
    List<ValidationError> validationErrors = new List<ValidationError>();

    public StringLengthValidationAttribute(int maxLength, int statusCode, string errorMessage)
    {
        _maxLength = maxLength;
        _statusCode = statusCode;
        _errorMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue && stringValue.Length > _maxLength)
        {
            validationErrors.Add(new ValidationError(validationContext.MemberName, _statusCode,
                string.Format(_errorMessage, _maxLength)));
            return new CustomValidationResult(validationErrors);
        }

        return ValidationResult.Success;
    }
}

public class RegularExpressionValidationAttribute : ValidationAttribute
{
    private readonly string _pattern;
    private readonly int _statusCode;
    private readonly string _errorMessage;
    List<ValidationError> validationErrors = new List<ValidationError>();

    public RegularExpressionValidationAttribute(string pattern, int statusCode, string errorMessage)
    {
        _pattern = pattern;
        _statusCode = statusCode;
        _errorMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string regexValue && !Regex.IsMatch(regexValue, _pattern))
        {
            validationErrors.Add(new ValidationError(validationContext.MemberName, _statusCode,
                string.Format(_errorMessage)));
            return new CustomValidationResult(validationErrors);
        }

        return ValidationResult.Success;
    }
}

public class IntegerRangeValidationAttribute : ValidationAttribute
{
    private readonly int _minValue;
    private readonly int _maxValue;
    private readonly int _statusCode;
    private readonly string _errorMessage;
    List<ValidationError> validationErrors = new List<ValidationError>();

    public IntegerRangeValidationAttribute(int minValue, int maxValue, int statusCode, string errorMessage)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        _statusCode = statusCode;
        _errorMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue && (intValue < _minValue || intValue > _maxValue))
        {
            validationErrors.Add(new ValidationError(validationContext.MemberName, _statusCode,
                string.Format(_errorMessage)));
            return new CustomValidationResult(validationErrors);
        }

        return ValidationResult.Success;
    }
}

public class RequiredValidationAttribute : ValidationAttribute
{
    private readonly int _statusCode;
    private readonly string _errorMessage;
    List<ValidationError> validationErrors = new List<ValidationError>();

    public RequiredValidationAttribute(int statusCode, string errorMessage)
    {
        _statusCode = statusCode;
        _errorMessage = errorMessage;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            validationErrors.Add(new ValidationError(validationContext.MemberName, _statusCode,
                string.Format(_errorMessage)));
            return new CustomValidationResult(validationErrors);
        }

        return ValidationResult.Success;
    }
}

public class ValidationError
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Field { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int Code { get; set; }

    public string Message { get; set; }

    public ValidationError()
    {
    }

    public ValidationError(string field, int code, string message)
    {
        Field = field;
        Code = code;
        Message = message;
    }
}

public class ValidationErrorMessage
{
    public int Code { get; set; }
    public string Message { get; set; }
}

public class ValidationException : Exception
{
    public List<ValidationError> ValidationErrors { get; set; }

    public ValidationException(List<ValidationError> errors)
    {
        ValidationErrors = errors;
    }
}

public class CustomValidationResult : ValidationResult
{
    public static readonly CustomValidationResult? Success;
    public List<ValidationError> ValidationErrors { get; }

    public CustomValidationResult(List<ValidationError> validationErrors)
        : base("One or more validation errors occurred.")
    {
        ValidationErrors = validationErrors;
    }
}