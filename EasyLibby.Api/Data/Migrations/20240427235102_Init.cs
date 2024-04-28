using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EasyLibby.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DateBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Librarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DateBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<decimal>(type: "decimal(13,0)", precision: 13, scale: 0, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CoverImageUri = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenewedTimes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "DateBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1948, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eckhart", "Tolle" },
                    { 2, new DateTime(1962, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jordan B.", "Peterson" },
                    { 3, new DateTime(1945, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paulo", "Coelho" },
                    { 4, new DateTime(1927, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel García", "Márquez" }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, "libJohn@gmail2.com", "LibJohn", "LibDoe", "pass123word" },
                    { 2, "Libjane@gmail2.com", "LibJane", "LibDoe", "pass678word" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "DateBirth", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John@gmail2.com", "John", "Doe", "pass123word" },
                    { 2, new DateTime(1985, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@gmail2.com", "Jane", "Doe", "pass45word" },
                    { 3, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice@gmail2.com", "Alice", "Smith", "pass67word" },
                    { 4, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@gmail2.com", "Bob", "Smith", "pass89word" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CoverImageUri", "ISBN", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 2, "https://images.isbndb.com/covers/60/47/9780345816047.jpg", 9780345816023m, new DateTime(1975, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 Rules for Life: An Antidote to Chaos" },
                    { 2, 1, "https://images.isbndb.com/covers/15/22/9781577311522.jpg", 9781577311522m, new DateTime(1985, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Power of Now: A Guide to Spiritual Enlightenment" },
                    { 3, 1, "https://images.isbndb.com/covers/94/11/9780141039411.jpg", 9780141039411m, new DateTime(1995, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A New Earth: Awakening to Your Life's Purpose" },
                    { 4, 3, "https://covers.openlibrary.org/b/id/45762-M.jpg", 9780062315007m, new DateTime(1989, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Alchemist" },
                    { 5, 3, "https://covers.openlibrary.org/b/id/31229-M.jpg", 9780060589288m, new DateTime(1997, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eleven Minutes" },
                    { 6, 4, "https://covers.openlibrary.org/b/id/40565-M.jpg", 9780140157512m, new DateTime(1967, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "One Hundred Years of Solitude" },
                    { 7, 4, "https://covers.openlibrary.org/b/id/10097251-M.jpg", 9780330255608m, new DateTime(1978, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Autumn of the Patriarch" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "BorrowedDate", "DueDate", "MemberId", "RenewedTimes" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 4, 23, 0, 51, 0, 619, DateTimeKind.Local).AddTicks(8539), new DateTime(2024, 5, 23, 0, 51, 0, 619, DateTimeKind.Local).AddTicks(8599), 1, 1 },
                    { 2, 3, new DateTime(2024, 4, 21, 0, 51, 0, 619, DateTimeKind.Local).AddTicks(8603), new DateTime(2024, 5, 21, 0, 51, 0, 619, DateTimeKind.Local).AddTicks(8604), 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MemberId",
                table: "Loans",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Librarians");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
