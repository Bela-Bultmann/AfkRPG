using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfkRPG.Application.Model;

public class users {

   public users(string _username, DateTime _created_at, DateTime _last_logout, bool _isadmin, string _email, string _password) {
	username = _username;
	created_at = _created_at;
	last_logout = _last_logout;
	isadmin = _isadmin;
	email = _email;
	password = _password;
	}

    #pragma warning disable CS8618
    protected users() { }

	public int Id {get; private set;}
	public string username {get; set;}
 	public DateTime created_at {get; set;}
 	public DateTime last_logout {get; set;}
 	public bool isadmin {get; set;}
 	public string email {get; set;}
 	public string password {get; set;}

}
