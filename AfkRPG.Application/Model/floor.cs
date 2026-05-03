using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class floor {

   public floor(int _floor_number, threat _threat, int _reward_gold) {
	floor_number = _floor_number;
	threat = _threat;
	reward_gold = _reward_gold;
	}

    #pragma warning disable CS8618
    protected floor() { }

	public int Id {get; private set;}
	public int floor_number {get; set;}
 	public threat threat {get; set;}
 	public int reward_gold {get; set;}

}
