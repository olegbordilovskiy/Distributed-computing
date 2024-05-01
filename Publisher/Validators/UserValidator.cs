using DC_REST.DTOs.Request;

namespace DC_REST.Validators
{
	public class UserValidator : IValidator<UserRequestTo>
	{
		public bool Validate(UserRequestTo userRequestTo)
		{
			if (userRequestTo == null) return false; 
			if (userRequestTo.login.Length < 2 || userRequestTo.login.Length > 64) return false;
			if (userRequestTo.password.Length < 8 || userRequestTo.password.Length > 128) return false;
			if (userRequestTo.firstname.Length < 2 || userRequestTo.firstname.Length > 64) return false;
			if (userRequestTo.lastname.Length < 2 || userRequestTo.lastname.Length > 64) return false;
			return true;
		}
	}
}
