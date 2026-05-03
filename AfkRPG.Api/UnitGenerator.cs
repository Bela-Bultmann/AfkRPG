using AfkRPG.Application.Model;

namespace AfkRPG.Api;

public static class UnitGenerator
{
    private static readonly Random Rng = Random.Shared;

    private static readonly string[] Rarities = { "common", "uncommon", "rare", "epic", "legendary" };
    private static readonly string[] Elements = { "fire", "water", "earth", "wind", "dark", "light" };
    private static readonly string[] Races = { "human", "beast", "spirit", "construct", "undead" };

    private record TypeProfile(int HpMin, int HpMax, int AtkMin, int AtkMax, int DefMin, int DefMax);

    private static readonly Dictionary<string, TypeProfile> Profiles = new() {
        { "tank",     new(140, 180,  5,  12, 25, 35) },
        { "knight",   new( 90, 120, 15,  25, 15, 25) },
        { "mage",     new( 55,  75, 35,  50,  3,  8) },
        { "assassin", new( 60,  80, 40,  55,  3,  7) },
        { "archer",   new( 60,  80, 25,  38, 18, 28) },
        { "healer",   new( 70,  90, 10,  18, 10, 18) },
    };

    public static units Generate(users player, string rarity)
    {
        if (Rarities.Any().Equals(rarity))
            throw new ArgumentException("Invalid rarity.");
        var mult = rarity switch
        {
            "uncommon" => 1.2,
            "rare" => 1.5,
            "epic" => 2.0,
            "legendary" => 3.0,
            _ => 1.0
        };

        var type = Profiles.Keys.ElementAt(Rng.Next(Profiles.Count));
        var p = Profiles[type];
        var hp = (int)(Rng.Next(p.HpMin, p.HpMax) * mult);
        var attack = (int)(Rng.Next(p.AtkMin, p.AtkMax) * mult);
        var defense = (int)(Rng.Next(p.DefMin, p.DefMax) * mult);
        var element = Elements[Rng.Next(Elements.Length)];
        var race = Races[Rng.Next(Races.Length)];
        var name = NameGenerator.Generate(noble: rarity is "epic" or "legendary");

        return new units(player, name, rarity, hp, attack, defense, type, element, race, DateTime.Now);
    }

    public static string RandomRarity()
    {
        var roll = Rng.NextDouble();
        return roll switch
        {
            < 0.50 => "common",
            < 0.75 => "uncommon",
            < 0.90 => "rare",
            < 0.98 => "epic",
            _ => "legendary"
        };
    }
}