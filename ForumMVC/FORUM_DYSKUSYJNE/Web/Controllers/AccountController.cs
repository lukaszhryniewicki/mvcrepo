using FORUM_DYSKUSYJNE.Core.Contracts;
using FORUM_DYSKUSYJNE.Core.Models;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace FORUM_DYSKUSYJNE.Controllers
{
	
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDataService _dataService;

		public AccountController(IUnitOfWork unitOfWork,IDataService dataService)
		{
			_unitOfWork=unitOfWork;
			_dataService = dataService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Login(string ReturnUrl = "")
		{
			
			if (User.Identity.IsAuthenticated)
			{
				return LogOut();
			}
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginView loginView, string ReturnUrl = "")
		{
				if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(loginView.Username, loginView.Password))
				{
					var user = (CustomMembershipUser)Membership.GetUser(loginView.Username, false);
					if (user.IsActive == false)
					{
						ModelState.AddModelError("", "Please activate your account through your email: " + user.Email);
						return View(loginView);
					}

					CustomSerializeModel userModel = new CustomSerializeModel()
					{
						UserId = user.UserId,
						Username = user.Username,
						RoleName = user.Role.Select(r => r.RoleName).ToList()
					};

					HttpCookie cookie = FormsAuthentication.GetAuthCookie(loginView.Username, loginView.RememberMe);
					DateTime ExpirationTime = loginView.RememberMe == true ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(1);
					cookie.Name = "Cookie1";
					if (loginView.RememberMe)
					{
						cookie.Expires = DateTime.Now.AddMonths(1);
					}

					string userData = JsonConvert.SerializeObject(userModel);
					FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
					(
						1, loginView.Username, DateTime.Now, ExpirationTime, loginView.RememberMe, userData, FormsAuthentication.FormsCookiePath
					);
					string enTicket = FormsAuthentication.Encrypt(authTicket);

					cookie.Value = enTicket;
					Response.Cookies.Add(cookie);

					if (ReturnUrl == "")
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return Redirect(ReturnUrl);
					}
				
				}
			}
			ModelState.AddModelError("User not in database", "Username or password invalid");

			return View(loginView);
		}

		[HttpGet]
		public ActionResult Registration()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public ActionResult Registration(RegistrationView registrationView)
		{
			
			bool statusRegistration = false;
			string messageRegistration = string.Empty;

			if (ModelState.IsValid)
				{
				Guid activationCode = Guid.NewGuid();
				// Email Verification  
				var userName = _unitOfWork.Repository<User>().Find(x => x.Username == registrationView.Username).FirstOrDefault();
				string userNameMail = Membership.GetUserNameByEmail(registrationView.Email);
				if (userName != null || !string.IsNullOrEmpty(userNameMail))
				{
					if(userName !=null)
						ModelState.AddModelError("Warning Username", "Username already Exists");
					
					if (!string.IsNullOrEmpty(userNameMail))
						ModelState.AddModelError("Warning Email", "Email already Exists");
					
					return View(registrationView);
				}

				//Save User Data   

				ICollection<Role> roles = _unitOfWork.Repository<Role>().Find(x => x.RoleName == "User").ToList();
					var rank = _unitOfWork.Repository<Rank>().Find(x => x.RankName == "New member").First();
					var user = new User()
					{
						Username = registrationView.Username,
						Name = registrationView.Name,
						Email = registrationView.Email,
						Password = registrationView.Password,
						Location = registrationView.Location,
						BirthdayDate = registrationView.BirthdayDate,
						ActivationCode = activationCode,
						Rank = rank,
						
					};
					user.Role = roles;
				_unitOfWork.Repository<User>().Add(user);
				_unitOfWork.Complete();

			   //VerificationEmail  
				VerificationEmail(registrationView.Email, activationCode.ToString());
				messageRegistration = "Your account has been created successfully.";
				statusRegistration = true;
			}
			else
			{
				messageRegistration = "Something went wrong!";
			}
			ViewBag.Message = messageRegistration;
			ViewBag.Status = statusRegistration;

			return View(registrationView);
		}

		[HttpGet]
		public ActionResult ActivationAccount(string id)
		{
			bool statusAccount = false;
			var userAccount = GetUserByCode(id);				

				if (userAccount != null)
				{
					userAccount.IsActive = true;
				_dataService.SaveChanges();
					statusAccount = true;
				}
				else if(userAccount == null)
				{
				ViewBag.Message = "No user with such code";
				}
				else if(userAccount.IsActive == true)
				{
					ViewBag.Message = "User has already been activated";
				}
				else
				{
					ViewBag.Message = "Something went wrong";
				}
			

			ViewBag.Status = statusAccount;
			LoginWithActivationCode(id);
			return View();
		}

		[CustomAuthorize(Roles="User")]
		public ActionResult LogOut()
		{
			HttpCookie cookie = new HttpCookie("Cookie1", "");
			cookie.Expires = DateTime.Now.AddYears(-1);
			Response.Cookies.Add(cookie);
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account", null);
		}

		[NonAction]
		public void VerificationEmail(string email, string activationCode)
		{
			var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
			var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

			var fromEmail = new MailAddress("AndrzejDuda6969xxx@gmail.com", "Activation Account - AKKA");
			var toEmail = new MailAddress(email);

			var fromEmailPassword = "costam123";
			string subject = "Activation Account !";

			string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
			};

			using (var message = new MailMessage(fromEmail, toEmail)
			{
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			})
			smtp.Send(message);
		}

		[NonAction]
		public void LoginWithActivationCode(string code)
		{
			var userByCode = _unitOfWork.Repository<User>().Find(x => x.ActivationCode.ToString() == code).First();

			if (userByCode != null)
			{
				CustomSerializeModel userModel = new CustomSerializeModel()
				{
					UserId = userByCode.UserId,
					Username = userByCode.Username,
					RoleName = userByCode.Role.Select(r => r.RoleName).ToList()
				};

				string userData = JsonConvert.SerializeObject(userModel);
				FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
				(
					1, userByCode.Username, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
				);

				string enTicket = FormsAuthentication.Encrypt(authTicket);
				HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
				Response.Cookies.Add(faCookie);
			}
		}

		public ActionResult AccountDetail(int id=-1)
		{

			if (id == -1) return HttpNotFound();
			var user = _unitOfWork.Repository<User>().Get(id);

			return View(user);
		}

		[HttpPost]
		public ActionResult AccountDetail(User userView)
		{
			var user = _unitOfWork.Repository<User>().Get(userView.UserId);

			byte[] imageData = null;

			if (Request.Files.Count > 0)
			{
				HttpPostedFileBase poImgFile = Request.Files["UserAvatar"];
				if(poImgFile.ContentLength<=0)
				{
					ModelState.AddModelError("AvatarResolutionError", "No avatar added");
					return View(user);
				}
				using (var binary = new BinaryReader(poImgFile.InputStream))
				{
					imageData = binary.ReadBytes(poImgFile.ContentLength);
				}
			}

			if(imageData.Length <= 3145728)
			{
				System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(imageData));

				if(image.Height <= 500 && image.Width <= 500)
				{
					user.Avatar = imageData;
					_unitOfWork.Complete();
				}
				else
				{
					ModelState.AddModelError("AvatarResolutionError", "Avatar resolution too big");
				}
			}
			else
			{
				ModelState.AddModelError("AvatarSizeError", "Avatar size too big");
			}
			return View(user);
		}
		
		public FileContentResult UserAvatar(int id)
		{
				
			var user = _unitOfWork.Repository<User>().Get(id);
			return new FileContentResult(user.Avatar, "image/jpeg");
		}
		
		public ActionResult ForgotPassword(string message = null, int? overload = null)
		{
			if (message != null)
				ViewBag.Message =message;

			return View();
		}

		[HttpPost]
		public ActionResult ForgotPassword(string email)
		{
			 
				if(string.IsNullOrEmpty(email))
			{
				
				return RedirectToAction("ForgotPassword", new { message = "Empty email" });
			}

			var user = _dataService.GetDbSet<User>()
				.Where(x => x.Email == email)
				.FirstOrDefault();
				
			if (user == null)
			{

				return RedirectToAction("ForgotPassword", new { message = "No user with such email" });

			}
			
				string linkCode = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
				var url = string.Format("/Account/ChangePassword/{0}", linkCode);
				var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

				var fromEmail = new MailAddress("AndrzejDuda6969xxx@gmail.com", "ForgotPassword - EMAIL");
				var toEmail = new MailAddress(email);

				var fromEmailPassword = "costam123";
				string subject = "Forgotten password - Forum !";

				string body = "<br/> Please click on the following link in order to generate new password" + "<br/><a href='" + link + "'> Forgot password ! </a>";

				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
				};

				using (var message = new MailMessage(fromEmail, toEmail)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				})
					smtp.Send(message);
				
				user.ForgottenPasswordCode = linkCode;
				_unitOfWork.Complete();
		
			return RedirectToAction("ForgotPassword",new { message ="Password sent"  });
		}

		public ActionResult ChangePassword(string id)
		{
			var user = GetUserByCode(id);

			if (user == null)
			{
				return RedirectToAction("ForgotPassword", "Action", new { message = "Something went wrong :(" });
			}
			var model = new ChangePasswordViewModel()
			{
				Code = id,
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordViewModel model)
		{


			var user = GetUserByCode(model.Code);

			user.Password = model.Password;
			user.ForgottenPasswordCode = null;
			_unitOfWork.Complete();
			
			return RedirectToAction("Login", "Account");
		}

		private User GetUserByCode(string code)
		{
			return  _dataService.GetDbSet<User>()
				.Where(x => x.ActivationCode.ToString() == code)
				.FirstOrDefault();
		}


	}
}
