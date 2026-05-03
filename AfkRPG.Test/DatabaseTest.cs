using AfkRPG.Application.Infrastructure;
using AfkRPG.Application.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;

namespace AfkRPG.Test;

public class DatabaseTest : IDisposable {

    private readonly SqliteConnection _connection;
    protected readonly AfkRPGContext _db;

    public DatabaseTest() {

        _connection = new SqliteConnection("DataSource=AfkRPG.db");
        _connection.Open();

        var opt = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=AfkRPG.db")
            .Options;
        _db = new AfkRPGContext(opt);
    }

    public void Dispose() {

        _db.Dispose();
        _connection.Dispose();
    }
}
