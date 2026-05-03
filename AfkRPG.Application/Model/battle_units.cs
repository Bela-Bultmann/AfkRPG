using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class battle_units {

   public battle_units(battles _battle, units _unit, decimal _performance) {
	battle = _battle;
	unit = _unit;
	performance = _performance;
	}

    #pragma warning disable CS8618
    protected battle_units() { }
    #pragma warning restore CS8618
    public int Id {get; private set;}
	public battles battle {get; set;}
 	public units unit {get; set;}
 	public decimal performance {get; set;}

}
