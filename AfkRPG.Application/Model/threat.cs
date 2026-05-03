using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class threat {

   public threat(string _biome, int _enemyStrength, int _enemyAmount, decimal _disatvantageMult) {
	biome = _biome;
	enemyStrength = _enemyStrength;
	enemyAmount = _enemyAmount;
	disatvantageMult = _disatvantageMult;
	}

    #pragma warning disable CS8618
    protected threat() { }
#pragma warning restore CS8618
    public int Id {get; private set;}
	public string biome {get; set;}
 	public int enemyStrength {get; set;}
 	public int enemyAmount {get; set;}
 	public decimal disatvantageMult {get; set;}

}
