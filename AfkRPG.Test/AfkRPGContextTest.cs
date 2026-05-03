namespace AfkRPG.Test;

public class AfkRPGContextTest : DatabaseTest {

    [Fact]
    public void CreateDatabaseTest() {
    
        _db.Database.EnsureCreated();
        _db.Seed();
    }
}
