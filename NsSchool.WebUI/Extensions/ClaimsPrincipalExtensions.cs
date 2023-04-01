using NsSchool.Data.Enums;
using System.Security.Claims;
using System.Security.Principal;

namespace NsSchool.WebUI.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static bool IsLogged(this ClaimsPrincipal user)
		{
			if(user.Claims.FirstOrDefault(x=>x.Type=="id")!=null)
			{
				return true;
				
			}
			else
			{
				return false;
			}
		}

		public static string GetFirstName(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "firstName")?.Value;
		}

		public static string GetLastName(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "lastName")?.Value;
		}

		public static string GetUserName(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "userName")?.Value;
		}

		public static string GetImagePath(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(x => x.Type == "imagePath")?.Value;
		}



		public static int GetId(this ClaimsPrincipal user)
		{
			return int.Parse(user.Claims.FirstOrDefault(x => x.Type == "id")?.Value);
		}

		public static bool IsAdmin(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Admin.ToString())
				return true;
			else
				return false;
		}

		public static bool IsParent(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Parent.ToString())
				return true;
			else
				return false;
		}

		public static bool IsTeacher(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Teacher.ToString())
				return true;
			else
				return false;
		}

		public static bool IsStudent(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Student.ToString())
				return true;
			else
				return false;
		}

		public static bool IsPerson(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Person.ToString())
				return true;
			else
				return false;
		}

		public static bool IsAccouting(this ClaimsPrincipal user)
		{
			if (user.Claims.FirstOrDefault(x => x.Type == "userType")?.Value == UserTypeEnum.Accounting.ToString())
				return true;
			else
				return false;
		}


		public static void AddUpdateClaim(this IPrincipal currentPrincipal, string? key, string? value)
		{
			var identity = currentPrincipal.Identity as ClaimsIdentity;
			if (identity == null)
				return;

			// check for existing claim and remove it
			var existingClaim = identity.FindFirst(key);
			if (existingClaim != null)
				identity.RemoveClaim(existingClaim);

			// add new claim
			identity.AddClaim(new Claim(key, value));

		}

		public static List<Claim> GetClaims(this ClaimsPrincipal user)
		{
			return user.Claims.ToList();
		}
	
	}
}
