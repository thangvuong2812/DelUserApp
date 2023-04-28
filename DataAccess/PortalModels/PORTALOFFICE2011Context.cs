using System;
using System.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.IdentityModel.Protocols;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccess.PortalModels
{
    public partial class PORTALOFFICE2011Context : DbContext
    {

        //https://stackoverflow.com/questions/53690820/how-to-create-a-loggerfactory-with-a-consoleloggerprovider
        //https://stackoverflow.com/questions/45893732/how-do-you-show-underlying-sql-query-in-ef-core
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => {
            builder.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Information);
            builder.AddDebug();
        });
        //public static readonly ILogger loggerFactory = new DebugLoggerProvider().CreateLogger("");
        //public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder.AddFilter("Microsoft.EntityFrameworkCore.*", LogLevel.Warning)

        //           .AddFilter("DelUserApp", LogLevel.Debug)
        //           .AddDebug();
        //});
        public PORTALOFFICE2011Context()
        {
        }

        public PORTALOFFICE2011Context(DbContextOptions<PORTALOFFICE2011Context> options)
            : base(options)
        {

            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public virtual DbSet<CoreUserGroup> CoreUserGroup { get; set; }
        public virtual DbSet<CoreUserPermission> CoreUserPermission { get; set; }
        public virtual DbSet<CoreUsers> CoreUsers { get; set; }
        public virtual DbSet<HrmEmployees> HrmEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                string currPath = Directory.GetCurrentDirectory();
                var connStr = new ConfigurationBuilder()
               .SetBasePath(currPath)
               .AddJsonFile("appsettings.json").Build().GetConnectionString("PortalDB");

                //Add LoggerFaco
                optionsBuilder.UseLoggerFactory(loggerFactory);
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoreUserGroup>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("Core_UserGroup");

                entity.HasIndex(e => new { e.IsManager, e.IsClerk, e.IsExtra, e.Title, e.JobTitleId, e.GroupId, e.UserId })
                    .HasName("IX_Core_UserGroup");

                entity.HasIndex(e => new { e.IsManager, e.IsClerk, e.IsExtra, e.Title, e.JobTitleId, e.UserId, e.GroupId })
                    .HasName("IX_Core_UserGroup_1");

                entity.Property(e => e.IsClerk).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsExtra).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsManager).HasDefaultValueSql("((0))");

                entity.Property(e => e.JobTitleId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoreUserGroup)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Core_UserGroup_Core_Users");
            });

            modelBuilder.Entity<CoreUserPermission>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("Core_UserPermission");

                entity.HasIndex(e => new { e.PermissionId, e.UserId })
                    .HasName("IX_Core_UserPermission_1");

                entity.HasIndex(e => new { e.UserId, e.PermissionId })
                    .HasName("IX_Core_UserPermission");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoreUserPermission)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Core_UserPermission_Core_Users");
            });

            modelBuilder.Entity<CoreUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Core_Users");

                entity.HasIndex(e => e.FirstName)
                    .HasName("IX_Core_Users_3");

                entity.HasIndex(e => e.ForeignName)
                    .HasName("IX_Core_Users_2");

                entity.HasIndex(e => e.PortalId)
                    .HasName("IX_Core_Users_4");

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_Core_Users_6");

                entity.HasIndex(e => new { e.ViewOrder, e.FirstName })
                    .HasName("IX_Core_Users_5");

                entity.HasIndex(e => new { e.GroupId, e.ViewOrder, e.FirstName })
                    .HasName("IX_Core_Users");

                entity.HasIndex(e => new { e.UserId, e.PortalId, e.GroupId, e.UserName, e.FullName, e.Authorized })
                    .HasName("IX_Core_Users_7");

                entity.Property(e => e.AvatarPath).HasMaxLength(50);

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DefaultEmailAccountDeleted).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultEmailAccountDraftId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DefaultEmailAccountEmail)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultEmailAccountEnabled).HasDefaultValueSql("((0))");

                entity.Property(e => e.DefaultEmailAccountFriendlyName)
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultEmailAccountId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DefaultEmailAccountInboxId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DefaultEmailAccountSentId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DefaultEmailAccountSpamId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DefaultEmailAccountTrashId).HasDefaultValueSql("((-1))");

                entity.Property(e => e.DisplayNotifyMessagesInInbox).HasDefaultValueSql("((1))");

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Email2).HasMaxLength(50);

                entity.Property(e => e.Email3).HasMaxLength(50);

                entity.Property(e => e.EmailHostName).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ForeignName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.GroupId2).HasDefaultValueSql("((-1))");

                entity.Property(e => e.GroupId3).HasDefaultValueSql("((-1))");

                entity.Property(e => e.GroupId4).HasDefaultValueSql("((-1))");

                entity.Property(e => e.GroupId5).HasDefaultValueSql("((-1))");

                entity.Property(e => e.ImagePath).HasMaxLength(50);

                entity.Property(e => e.IpallowedMaskRestrictionId).HasColumnName("IPAllowedMaskRestrictionId");

                entity.Property(e => e.IpallowedSingleRestrictionId).HasColumnName("IPAllowedSingleRestrictionId");

                entity.Property(e => e.IpdeniedMaskRestrictionId).HasColumnName("IPDeniedMaskRestrictionId");

                entity.Property(e => e.IpdeniedSingleRestrictionId).HasColumnName("IPDeniedSingleRestrictionId");

                entity.Property(e => e.IsIprestriction).HasColumnName("IsIPRestriction");

                entity.Property(e => e.IsLockedOut).HasDefaultValueSql("((0))");

                entity.Property(e => e.JobTitleId2).HasDefaultValueSql("((-1))");

                entity.Property(e => e.JobTitleId3).HasDefaultValueSql("((-1))");

                entity.Property(e => e.JobTitleId4).HasDefaultValueSql("((-1))");

                entity.Property(e => e.JobTitleId5).HasDefaultValueSql("((-1))");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.LastPwdChanged).HasColumnType("datetime");

                entity.Property(e => e.LdapuserName)
                    .HasColumnName("LDAPUserName")
                    .HasMaxLength(50);

                entity.Property(e => e.LockoutTime).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.NotificationSettingsUpdated).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComputedColumnSql("([PasswordHash])");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonnelCode).HasMaxLength(50);

                entity.Property(e => e.PlaySoundWhenNewChatMessages).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlaySoundWhenNewMessages).HasDefaultValueSql("((0))");

                entity.Property(e => e.PopupWhenNewPopmessages)
                    .HasColumnName("PopupWhenNewPOPMessages")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PopupWhenNewPrmsmessages)
                    .HasColumnName("PopupWhenNewPRMSMessages")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PortalId2).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PortalId3).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PortalId4).HasDefaultValueSql("((-1))");

                entity.Property(e => e.PortalId5).HasDefaultValueSql("((-1))");

                entity.Property(e => e.ReversedMobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReversedUserName).HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Street).HasMaxLength(150);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.TempEmail).HasMaxLength(50);

                entity.Property(e => e.TempEmail2).HasMaxLength(50);

                entity.Property(e => e.TempMobile).HasMaxLength(50);

                entity.Property(e => e.TempTelephone).HasMaxLength(50);

                entity.Property(e => e.TempTitle).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Title2).HasMaxLength(50);

                entity.Property(e => e.Title3).HasMaxLength(50);

                entity.Property(e => e.Title4).HasMaxLength(50);

                entity.Property(e => e.Title5).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName1).HasMaxLength(50);

                entity.Property(e => e.UserName2).HasMaxLength(50);

                entity.Property(e => e.UserName3).HasMaxLength(50);
            });

            modelBuilder.Entity<HrmEmployees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("HRM_Employees");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("IX_HRM_Employees");

                entity.HasIndex(e => e.PortalId)
                    .HasName("IX_HRM_Employees_1");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_HRM_Employees_4");

                entity.HasIndex(e => new { e.Status, e.EmployeeCode })
                    .HasName("IX_HRM_Employees_3");

                entity.HasIndex(e => new { e.ViewOrder, e.FirstName })
                    .HasName("IX_HRM_Employees_2");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BankAccount).HasMaxLength(50);

                entity.Property(e => e.BankAccount2).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BankName2).HasMaxLength(100);

                entity.Property(e => e.CardParty).HasMaxLength(50);

                entity.Property(e => e.CardUnion).HasMaxLength(50);

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.CpplaceOfIssue)
                    .HasColumnName("CPPlaceOfIssue")
                    .HasMaxLength(150);

                entity.Property(e => e.CudateOfIssue)
                    .HasColumnName("CUDateOfIssue")
                    .HasColumnType("datetime");

                entity.Property(e => e.CuplaceOfIssue)
                    .HasColumnName("CUPlaceOfIssue")
                    .HasMaxLength(150);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DateOfIssue).HasColumnType("datetime");

                entity.Property(e => e.DatePromotion).HasColumnType("datetime");

                entity.Property(e => e.Degree).HasMaxLength(30);

                entity.Property(e => e.EducationLevel).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Email2).HasMaxLength(50);

                entity.Property(e => e.Email3).HasMaxLength(50);

                entity.Property(e => e.EmailCompany)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode2).HasMaxLength(10);

                entity.Property(e => e.EthnicGroup).HasMaxLength(50);

                entity.Property(e => e.Family).HasMaxLength(1000);

                entity.Property(e => e.FileCv)
                    .HasColumnName("FileCV")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.FirstName2).HasMaxLength(50);

                entity.Property(e => e.ForeignName).HasMaxLength(50);

                entity.Property(e => e.FullName2).HasMaxLength(50);

                entity.Property(e => e.HealthInsurance).HasMaxLength(50);

                entity.Property(e => e.HidateOfIssue)
                    .HasColumnName("HIDateOfIssue")
                    .HasColumnType("datetime");

                entity.Property(e => e.Hobby).HasMaxLength(150);

                entity.Property(e => e.IdcardNumber)
                    .HasColumnName("IDCardNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.ImagePath).HasMaxLength(50);

                entity.Property(e => e.JobLevel).HasMaxLength(50);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Language1).HasMaxLength(30);

                entity.Property(e => e.Language2).HasMaxLength(30);

                entity.Property(e => e.LanguageDegree1).HasMaxLength(30);

                entity.Property(e => e.LanguageDegree2).HasMaxLength(30);

                entity.Property(e => e.LanguageRank1).HasMaxLength(30);

                entity.Property(e => e.LanguageRank2).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.LastName2).HasMaxLength(50);

                entity.Property(e => e.LeaveDay).HasColumnType("datetime");

                entity.Property(e => e.MarriageStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MobileCompany)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PartyDate).HasColumnType("datetime");

                entity.Property(e => e.PersonalTaxCode).HasMaxLength(30);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.PlaceOfIssue).HasMaxLength(100);

                entity.Property(e => e.PlaceOfOrigin).HasMaxLength(250);

                entity.Property(e => e.Rated).HasMaxLength(20);

                entity.Property(e => e.ReasonLeave).HasMaxLength(100);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.ResidentCity).HasMaxLength(50);

                entity.Property(e => e.ResidentDistrict).HasMaxLength(50);

                entity.Property(e => e.ResidentStreet).HasMaxLength(250);

                entity.Property(e => e.School).HasMaxLength(100);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SidateOfIssue)
                    .HasColumnName("SIDateOfIssue")
                    .HasColumnType("datetime");

                entity.Property(e => e.Skills).HasMaxLength(500);

                entity.Property(e => e.SocialInsurance).HasMaxLength(50);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TelephoneCompany)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.WorkingProfile).HasMaxLength(1000);

                entity.Property(e => e.YearOfGraduation).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
