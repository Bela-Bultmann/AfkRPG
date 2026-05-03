using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class units {

   public units(users _player, string _name, string _rarity, int _base_hp, int _base_attack, int _base_defense, string _type, string _element, string _race, DateTime _created_at) {
	player = _player;
	name = _name;
	rarity = _rarity;
	base_hp = _base_hp;
	base_attack = _base_attack;
	base_defense = _base_defense;
	type = _type;
	element = _element;
	race = _race;
	created_at = _created_at;
	}

    #pragma warning disable CS8618
    protected units() { }

	public int Id {get; private set;}
	public users player {get; set;}
 	public string name {get; set;}
 	public string rarity {get; set;}
 	public int base_hp {get; set;}
 	public int base_attack {get; set;}
 	public int base_defense {get; set;}
 	public string type {get; set;}
 	public string element {get; set;}
 	public string race {get; set;}
 	public DateTime created_at {get; set;}

}
