using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MzansiJobs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Province = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    logoUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Industry = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CVUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    phoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    bio = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Candidates_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    phoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    surname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    jobTitle = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employers_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    isUsed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    JobID = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidateID = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dateApplied = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 512, nullable: false),
                    coverLetter = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Applications_Candidates_CandidateID",
                        column: x => x.CandidateID,
                        principalTable: "Candidates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmploymentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyID = table.Column<Guid>(type: "uuid", nullable: false),
                    salaryRange = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    postedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    JobSkillID = table.Column<Guid>(type: "uuid", nullable: true),
                    SkillID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    JobID = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobID",
                        column: x => x.JobID,
                        principalTable: "Jobs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    JobSkillID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Skills_JobSkills_JobSkillID",
                        column: x => x.JobSkillID,
                        principalTable: "JobSkills",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CandidateID",
                table: "Applications",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobID_CandidateID",
                table: "Applications",
                columns: new[] { "JobID", "CandidateID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_UserID",
                table: "Candidates",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employers_CompanyID",
                table: "Employers",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employers_UserID",
                table: "Employers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyID",
                table: "Jobs",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobSkillID",
                table: "Jobs",
                column: "JobSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_SkillID",
                table: "Jobs",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobID_SkillID",
                table: "JobSkills",
                columns: new[] { "JobID", "SkillID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillID",
                table: "JobSkills",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserID",
                table: "RefreshTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobSkillID",
                table: "Skills",
                column: "JobSkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Jobs_JobID",
                table: "Applications",
                column: "JobID",
                principalTable: "Jobs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobSkills_JobSkillID",
                table: "Jobs",
                column: "JobSkillID",
                principalTable: "JobSkills",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Skills_SkillID",
                table: "Jobs",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkills_Skills_SkillID",
                table: "JobSkills",
                column: "SkillID",
                principalTable: "Skills",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkills_Jobs_JobID",
                table: "JobSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobSkills_JobSkillID",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
