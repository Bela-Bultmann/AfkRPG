using Microsoft.EntityFrameworkCore;
using AfkRPG.Application.Model;

namespace AfkRPG.Application.Infrastructure;

public class AfkRPGContext : DbContext {
    public AfkRPGContext(DbContextOptions opt) : base(opt) { }

    public DbSet<users> users => Set<users>();
    public DbSet<units> units => Set<units>();
    public DbSet<battle_units> battle_units => Set<battle_units>();
    public DbSet<battles> battles => Set<battles>();
    public DbSet<floor> floor => Set<floor>();
    public DbSet<threat> threat => Set<threat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<threat>(e => {
            e.HasKey(t => t.Id);
        });

        modelBuilder.Entity<floor>(e => {
            e.HasKey(f => f.Id);
            e.HasOne(f => f.threat).WithMany().IsRequired();
        });

        modelBuilder.Entity<users>(e => {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.username).IsUnique();
            e.HasIndex(u => u.email).IsUnique();
            e.Property(u => u.username).IsRequired();
            e.Property(u => u.email).IsRequired();
            e.Property(u => u.password).IsRequired();
        });

        modelBuilder.Entity<units>(e => {
            e.HasKey(u => u.Id);
            e.HasOne(u => u.player).WithMany().IsRequired();
        });

        modelBuilder.Entity<battles>(e => {
            e.HasKey(b => b.Id);
            e.HasOne(b => b.player).WithMany().IsRequired();
            e.HasOne(b => b.floor).WithMany().IsRequired();
        });

        modelBuilder.Entity<battle_units>(e => {
            e.HasKey(bu => bu.Id);
            e.HasOne(bu => bu.battle).WithMany().IsRequired();
            e.HasOne(bu => bu.unit).WithMany().IsRequired();
        });
    }

    public void Seed()
    {
        var threats = SeedThreats();
        var floors = SeedFloors(threats);
        var users = SeedUsers();
        var units = SeedUnits(users);
        var battles = SeedBattles(users, floors);
        SeedBattleUnits(battles, units);
    }

    private List<threat> SeedThreats()
    {
        var data = new List<threat>
    {
        new threat("forest",  10, 5,  1.0m),
        new threat("desert",  20, 8,  1.2m),
        new threat("dungeon", 35, 12, 1.5m),
    };
        threat.AddRange(data);
        SaveChanges();
        return data;
    }

    private List<floor> SeedFloors(List<threat> threats)
    {
        var data = new List<floor>
    {
        new floor(1, threats[0], 50),
        new floor(2, threats[0], 75),
        new floor(3, threats[1], 120),
        new floor(4, threats[1], 150),
        new floor(5, threats[2], 300),
    };
        floor.AddRange(data);
        SaveChanges();
        return data;
    }

    private List<users> SeedUsers()
    {
        var data = new List<users>
    {
        new users("admin", new DateTime(2024, 1, 1), new DateTime(2024, 1, 1),true,   "admin@afkrpg.com",  "hash1"),
        new users("hero123", new DateTime(2024, 1, 2), new DateTime(2024, 1, 2),true, "hero@afkrpg.com",   "hash2"),
        new users("mage99", new DateTime(2024, 1, 3), new DateTime(2024, 1, 3),true,  "mage@afkrpg.com",   "hash3"),
    };
        users.AddRange(data);
        SaveChanges();
        return data;
    }

    private List<units> SeedUnits(List<users> users)
    {
        var data = new List<units>
    {
        new units(users[1], "Iron Golem",  "common",   120, 15, 20, "tank",     "earth", "construct", new DateTime(2024, 1, 2)),
        new units(users[1], "Fire Sprite", "uncommon",  70, 30, 10, "mage",     "fire",  "spirit",    new DateTime(2024, 1, 2)),
        new units(users[2], "Shadow Wolf", "rare",      90, 25, 15, "assassin", "dark",  "beast",     new DateTime(2024, 1, 3)),
        new units(users[2], "Storm Hawk",  "epic",      80, 40,  8, "archer",   "wind",  "beast",     new DateTime(2024, 1, 3)),
    };
        units.AddRange(data);
        SaveChanges();
        return data;
    }

    private List<battles> SeedBattles(List<users> users, List<floor> floors)
    {
        var data = new List<battles>
    {
        new battles(users[1], floors[0], true,  new DateTime(2024, 1, 3, 10, 0, 0), new DateTime(2024, 1, 3, 10, 5, 0)),
        new battles(users[1], floors[1], true,  new DateTime(2024, 1, 3, 11, 0, 0), new DateTime(2024, 1, 3, 11, 7, 0)),
        new battles(users[2], floors[2], false, new DateTime(2024, 1, 3, 12, 0, 0), new DateTime(2024, 1, 3, 12, 3, 0)),
        new battles(users[2], floors[3], true,  new DateTime(2024, 1, 4,  9, 0, 0), new DateTime(2024, 1, 4,  9, 8, 0)),
    };
        battles.AddRange(data);
        SaveChanges();
        return data;
    }

    private List<battle_units> SeedBattleUnits(List<battles> battles, List<units> units)
    {
        var data = new List<battle_units>
    {
        new battle_units(battles[0], units[0], 0.85m),
        new battle_units(battles[0], units[1], 0.92m),
        new battle_units(battles[1], units[0], 0.78m),
        new battle_units(battles[2], units[2], 0.45m),
        new battle_units(battles[3], units[3], 0.90m),
    };
        battle_units.AddRange(data);
        SaveChanges();
        return data;
    }
}

