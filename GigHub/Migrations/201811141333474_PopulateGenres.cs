namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, NAME) VALUES (1, 'Jaz')");
            Sql("INSERT INTO Genres (Id, NAME) VALUES (2, 'Blues')");
            Sql("INSERT INTO Genres (Id, NAME) VALUES (3, 'Rock')");
            Sql("INSERT INTO Genres (Id, NAME) VALUES (4, 'Country')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id in (1,2,3,4)");
        }
    }
}
