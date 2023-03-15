using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccess.PortalModels
{
    public partial class CoreUsers
    {
        public CoreUsers()
        {
            CoreUserGroup = new HashSet<CoreUserGroup>();
            CoreUserPermission = new HashSet<CoreUserPermission>();
        }

        public int UserId { get; set; }
        public int? PortalId { get; set; }
        public int? GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool? Authorized { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? DisableUsingMailModule { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ViewOrder { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Sex { get; set; }
        public int? MenuId { get; set; }
        public string Description { get; set; }
        public string UserName1 { get; set; }
        public string UserName2 { get; set; }
        public string UserName3 { get; set; }
        public DateTime? LastPwdChanged { get; set; }
        public string TempTitle { get; set; }
        public string TempEmail { get; set; }
        public string TempMobile { get; set; }
        public string TempTelephone { get; set; }
        public string ForeignName { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string TempEmail2 { get; set; }
        public string ImagePath { get; set; }
        public int? JobTitleId { get; set; }
        public int? IpallowedSingleRestrictionId { get; set; }
        public int? IpallowedMaskRestrictionId { get; set; }
        public int? IpdeniedSingleRestrictionId { get; set; }
        public int? IpdeniedMaskRestrictionId { get; set; }
        public bool? IsIprestriction { get; set; }
        public int? JobTitleId2 { get; set; }
        public int? JobTitleId3 { get; set; }
        public int? GroupId2 { get; set; }
        public int? GroupId3 { get; set; }
        public int? PortalId2 { get; set; }
        public int? PortalId3 { get; set; }
        public string Title2 { get; set; }
        public string Title3 { get; set; }
        public string FullName { get; set; }
        public string LdapuserName { get; set; }
        public string AvatarPath { get; set; }
        public int? DefaultEmailAccountId { get; set; }
        public int? DefaultEmailAccountInboxId { get; set; }
        public int? DefaultEmailAccountDraftId { get; set; }
        public bool? DefaultEmailAccountEnabled { get; set; }
        public bool? DefaultEmailAccountDeleted { get; set; }
        public int? DefaultEmailAccountSentId { get; set; }
        public int? DefaultEmailAccountSpamId { get; set; }
        public int? DefaultEmailAccountTrashId { get; set; }
        public string DefaultEmailAccountEmail { get; set; }
        public string DefaultEmailAccountFriendlyName { get; set; }
        public int? JobTitleId4 { get; set; }
        public int? GroupId4 { get; set; }
        public int? PortalId4 { get; set; }
        public string Title4 { get; set; }
        public int? JobTitleId5 { get; set; }
        public int? GroupId5 { get; set; }
        public int? PortalId5 { get; set; }
        public string Title5 { get; set; }
        public bool? IsLockedOut { get; set; }
        public DateTime? LockoutTime { get; set; }
        public string ReversedUserName { get; set; }
        public string EmailHostName { get; set; }
        public string ReversedMobile { get; set; }
        public bool? NotificationSettingsUpdated { get; set; }
        public bool? PopupWhenNewPopmessages { get; set; }
        public bool? PopupWhenNewPrmsmessages { get; set; }
        public bool? DisplayNotifyMessagesInInbox { get; set; }
        public bool? PlaySoundWhenNewMessages { get; set; }
        public bool? PlaySoundWhenNewChatMessages { get; set; }
        public string Password { get; set; }
        public string PersonnelCode { get; set; }

        public virtual ICollection<CoreUserGroup> CoreUserGroup { get; set; }
        public virtual ICollection<CoreUserPermission> CoreUserPermission { get; set; }
    }
}
