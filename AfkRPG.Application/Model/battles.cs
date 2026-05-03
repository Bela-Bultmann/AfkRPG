using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class battles {

   public battles(users _player, floor _floor, bool _result, DateTime _from, DateTime _to) {
	player = _player;
	floor = _floor;
	result = _result;
	from = _from;
	to = _to;
	}

    #pragma warning disable CS8618
    protected battles() { }

	public int Id {get; private set;}
	public users player {get; set;}
 	public floor floor {get; set;}
 	public bool result {get; set;}
 	public DateTime from {get; set;}
 	public DateTime to {get; set;}

}
