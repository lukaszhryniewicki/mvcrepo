using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FORUM_DYSKUSYJNE.CustomMembershipMVC
{
	
	public class CustomMembershipProvider : MembershipProvider
	{
		
		public override bool ValidateUser(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				return false;
			}

			using (ForumDatabase dbContext = ForumDatabase.Create())
			{
				var user = dbContext.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();

				return (user != null) ? true : false;
			}
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			using (ForumDatabase dbContext = ForumDatabase.Create())
			{
				var user = dbContext.Users.Where(u => u.Username == username).FirstOrDefault();

				if (user == null)
				{
					return null;
				}
				var selectedUser = new CustomMembershipUser(user);

				return selectedUser;
			}
		}
		public override string GetUserNameByEmail(string email)
		{
			using (ForumDatabase dbContext = ForumDatabase.Create())
			{
				string username = dbContext.Users.Where(u => u.Email == email).Select(u => u.Email).FirstOrDefault();

				return !string.IsNullOrEmpty(username) ? username : string.Empty;
			}
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool EnablePasswordReset
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool EnablePasswordRetrieval
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int MinRequiredPasswordLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override int PasswordAttemptWindow
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override string PasswordStrengthRegularExpression
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool RequiresUniqueEmail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}
	}
	public class CustomMembershipUser : MembershipUser
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public ICollection<Role> Role { get; set; }
        public bool IsActive { get; set; }

		public CustomMembershipUser(User user) : base("CustomMembershipProvider", user.Username, user.UserId, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
		{
			UserId = user.UserId;
			Username = user.Username;
			Role = user.Role;
            IsActive = user.IsActive;
		}
	}
	public class CustomRole : RoleProvider
	{ 

		public override bool IsUserInRole(string username, string roleName)
		{
			var userRoles = GetRolesForUser(username);
			return userRoles.Contains(roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			if (!HttpContext.Current.User.Identity.IsAuthenticated)
			{
				return null;
			}

			var userRoles = new string[] { };

			using (ForumDatabase dbContext = ForumDatabase.Create())
			{
				var selectedUser = dbContext.Users.Where(u => u.Username == username).FirstOrDefault();

				if (selectedUser != null)
				{
					userRoles = new[] { selectedUser.Role.Select(r => r.RoleName).ToString() };
				}

				return userRoles.ToArray();
			}
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
	}

	public class CustomPrincipal : IPrincipal
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string[] Roles { get; set; }

		public IIdentity Identity
		{
			get; private set;
		}

		public bool IsInRole(string role)
		{
			if (Roles.Contains(role))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public CustomPrincipal(string username)
		{
			Identity = new GenericIdentity(username);
		}
	}

	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{

		protected virtual CustomPrincipal CurrentUser
		{
			get { return HttpContext.Current.User as CustomPrincipal; }
		}

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			RedirectToRouteResult routeData = null;

			if (CurrentUser == null)
			{
				routeData = new RedirectToRouteResult
					(new System.Web.Routing.RouteValueDictionary
					(new
					{
						controller = "Account",
						action = "Login",
					}
					));
			}
			else
			{
				routeData = new RedirectToRouteResult
				(new System.Web.Routing.RouteValueDictionary
				 (new
				 {
					 controller = "Error",
					 action = "AccessDenied"
				 }
				 ));
			}
			filterContext.Result = routeData;
		}
	}
    
} 
