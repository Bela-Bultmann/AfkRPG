namespace AfkRPG.Api;

public static class NameGenerator
{
    private static readonly Random Rng = Random.Shared;

    private static readonly string[] SimpleStarts = {
        "han", "ni", "an", "ada", "bor", "mor", "tor", "eld", "alf", "bri", "cal",
        "dar", "ern", "fin", "gar", "hob", "ing", "jan", "kel", "lan", "mar",
        "nor", "olf", "par", "ran", "sig", "tam", "ulf", "var", "wen", "yor",
        "abe", "bel", "cor", "dun", "eve", "fal", "gil", "hal", "ida", "jon",
        "kar", "lem", "mab", "ned", "ora", "per", "ros", "ser", "til", "una"
    };

    private static readonly string[] SimpleEnds = {
        "a", "an", "ar", "as", "en", "er", "in", "is", "on", "or",
        "us", "yn", "el", "il", "un", "ot", "at", "ek", "ger", "ok", "ix"
    };

    private static readonly string[] NoblePrefixes = {
        "aer", "alth", "aur", "bael", "caer", "cael", "dae", "eld", "elyn",
        "ereb", "fael", "gael", "gor", "ial", "ith", "kael", "lyr", "mal",
        "mor", "nael", "oel", "phal", "rael", "sael", "thal", "uel", "vael",
        "wyr", "xael", "yel", "zael", "aeth", "byr", "cyr", "dyr", "elyn",
        "faer", "gyr", "hael", "iael", "jael", "kyr", "lael", "mael", "nyr"
    };

    private static readonly string[] NobileMiddles = {
        "an", "ar", "dor", "el", "en", "er", "ian", "iel", "il", "in",
        "ion", "ir", "is", "ith", "on", "or", "oth", "un", "ur", "ys",
        "ael", "eil", "oer", "aur", "ior", "ath", "eth", "oth", "yth", "yrr"
    };

    private static readonly string[] NobleSuffixes = {
        "aeus", "aith", "alius", "amon", "anel", "anis", "anor", "anus",
        "arak", "arel", "ares", "aron", "arys", "ath", "avel", "avin",
        "axen", "ayis", "azel", "dael", "dain", "dalis", "damus", "danis",
        "darel", "daron", "dath", "daxis", "delin", "delis", "deon", "deras",
        "diel", "dilis", "dilon", "dimus", "direl", "diron", "diros", "dirus",
        "elius", "elon", "elun", "elus", "enor", "eorl", "eran", "eras",
        "erel", "eren", "eris", "eron", "eros", "eryn", "esar", "esis",
        "grha", "gron", "grus", "iael", "iath", "idon", "idor", "idus",
        "imus", "inel", "inus", "irel", "iron", "iros", "irun", "irus",
        "onel", "onus", "oran", "oras", "orel", "oren", "oris", "oron",
        "orus", "oryn", "osar", "osis", "unel", "unus", "uran", "uras"
    };

    private static readonly string[] NobleTitles = {
        "the Great", "the Wise", "the Bold", "the Just", "the Swift",
        "the Undying", "the Iron", "the Golden", "the Silver", "the Cursed",
        "the Exiled", "the Reborn", "the Fallen", "the Risen", "the Ancient",
        "of the North", "of the West", "of the East", "of the South",
        "of the Storm", "of the Flame", "of the Void", "of the Deep",
        "of the High Keep", "of the Iron Throne", "of the Silver Tower",
        "of the Ashen Wastes", "of the Forgotten Realm", "of the Sunken Vale",
        "Dragonborn", "Ironblood", "Stormcaller", "Voidwalker", "Flamebearer",
        "Shadowbane", "Lightbringer", "Soulreaper", "Dawnbreaker", "Nightfall"
    };

    private static readonly string[] PeasantTitles = {
        "the Miller", "the Smith", "the Baker", "the Farmer", "the Fisher",
        "the Cooper", "the Tanner", "the Weaver", "the Hunter", "the Shepherd",
        "from Ashfield", "from Millbrook", "from Dunnhaven", "from Stonegate",
        "from the Valley", "from the Hills", "from the Moor", "from the Fens",
        "son of Bor", "son of Tor", "son of Gar", "daughter of Mab",
        "the Stout", "the Lame", "the Red", "the Grey", "the Tall", "the Small"
    };

    private static readonly string[] ExoticConsonantClusters = {
        "yrr", "thr", "str", "ghr", "khr", "vrr", "zhr", "dhr", "brr", "grr"
    };

    private static readonly string[] ExoticVowelEnds = {
        "otha", "itha", "utha", "aetha", "oetha", "yrra", "irra", "urra"
    };

    // ── Core generator ───────────────────────────────────────────────────
    public static string Generate(bool noble)
    {
        // Rare chance (5%) for exotic cluster name regardless of class
        if (Rng.NextDouble() < 0.05)
            return GenerateExotic(noble);

        return noble ? GenerateNoble() : GeneratePeasant();
    }

    private static string GeneratePeasant()
    {
        var name = SimpleStarts[Rng.Next(SimpleStarts.Length)];

        // 40% chance of a second syllable
        if (Rng.NextDouble() < 0.4)
            name += SimpleEnds[Rng.Next(SimpleEnds.Length)];

        name = Capitalize(name);

        // 20% chance of a peasant title
        if (Rng.NextDouble() < 0.2)
            name += " " + PeasantTitles[Rng.Next(PeasantTitles.Length)];

        return name;
    }

    private static string GenerateNoble()
    {
        var name = NoblePrefixes[Rng.Next(NoblePrefixes.Length)];

        // 60% chance of a middle syllable
        if (Rng.NextDouble() < 0.6)
            name += NobileMiddles[Rng.Next(NobileMiddles.Length)];

        name += NobleSuffixes[Rng.Next(NobleSuffixes.Length)];
        name = Capitalize(name);

        // 50% chance of a noble title
        if (Rng.NextDouble() < 0.5)
            name += " " + NobleTitles[Rng.Next(NobleTitles.Length)];

        return name;
    }

    private static string GenerateExotic(bool noble)
    {
        var cluster = ExoticConsonantClusters[Rng.Next(ExoticConsonantClusters.Length)];
        var ending = ExoticVowelEnds[Rng.Next(ExoticVowelEnds.Length)];
        var middle = NobileMiddles[Rng.Next(NobileMiddles.Length)];
        var name = Capitalize(cluster + middle + ending);

        var titles = noble ? NobleTitles : PeasantTitles;
        if (Rng.NextDouble() < 0.6)
            name += " " + titles[Rng.Next(titles.Length)];

        return name;
    }

    private static string Capitalize(string s) =>
        string.IsNullOrEmpty(s) ? s : char.ToUpper(s[0]) + s[1..];
}