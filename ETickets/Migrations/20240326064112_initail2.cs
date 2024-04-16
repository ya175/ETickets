using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class initail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovies_Actors_ActorId",
                table: "ActorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovies_Movies_MovieId",
                table: "ActorMovies");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "ActorMovies",
                newName: "ActorsId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "ActorMovies",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorMovies_ActorId",
                table: "ActorMovies",
                newName: "IX_ActorMovies_ActorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovies_Actors_ActorsId",
                table: "ActorMovies",
                column: "ActorsId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovies_Movies_MoviesId",
                table: "ActorMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovies_Actors_ActorsId",
                table: "ActorMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_ActorMovies_Movies_MoviesId",
                table: "ActorMovies");

            migrationBuilder.RenameColumn(
                name: "ActorsId",
                table: "ActorMovies",
                newName: "ActorId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "ActorMovies",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_ActorMovies_ActorsId",
                table: "ActorMovies",
                newName: "IX_ActorMovies_ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovies_Actors_ActorId",
                table: "ActorMovies",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActorMovies_Movies_MovieId",
                table: "ActorMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
