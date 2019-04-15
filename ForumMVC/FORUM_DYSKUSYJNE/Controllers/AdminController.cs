using FORUM_DYSKUSYJNE.Database;
using FORUM_DYSKUSYJNE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using FORUM_DYSKUSYJNE.CustomMembershipMVC;
using FORUM_DYSKUSYJNE.DataAccess;
using FORUM_DYSKUSYJNE.Contracts;

namespace FORUM_DYSKUSYJNE.Controllers
{	
	[CustomAuthorize(Roles ="Admin")]
	public class AdminController : Controller
	{
		private IUnitOfWork _unitOfWork;
		public AdminController(IUnitOfWork unitOfWork)
		{
			_unitOfWork=unitOfWork;
		}
		
		public ActionResult Panel()
		{

			return View();
		}
		
		public ActionResult CreateSection()
		{
			
			var model = new PanelView()
			{

				Groups = _unitOfWork.Repository<Group>().GetAll(),
				
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult CreateSection(PanelView panelView)
		{
			if(panelView.SelectedSection == null)
			{
				var orderNull = _unitOfWork.Sections.GetTopOrder();
				var sectionDataNull = new Section()
				{
					Order = orderNull+1,
					Name = panelView.Name,
					Description = panelView.Description,
					GroupId = Convert.ToInt32(panelView.SelectedGroup)
				};
				_unitOfWork.Sections.Add(sectionDataNull);

				_unitOfWork.Complete();
				return RedirectToAction("Index", "Home");
			}
			//dodaj przed wybranym
			var sectionid = Convert.ToInt32(panelView.SelectedSection);
			var section = _unitOfWork.Sections.Get(sectionid);
			var order = section.Order;
			var items = _unitOfWork.Sections.Find(id => id.Order >= order);

			foreach (var item in items )
			{
				item.Order = item.Order + 1;
			}

			var sectionData = new Section()
			{
				Order = order,
				Name = panelView.Name,
				Description = panelView.Description,
				GroupId = Convert.ToInt32(panelView.SelectedGroup)
			};

			_unitOfWork.Sections.Add(sectionData);
			_unitOfWork.Complete();

			return RedirectToAction("Index", "Home");
		}

		public ActionResult EditDeleteForum()
		{
			var viewModel = new HomeView()
			{
				Groups = _unitOfWork.Repository<Group>().GetAll(),
				Sections = _unitOfWork.Sections.OrderBy(x=>x.Order),
			};

			return View(viewModel);
		}

		public ActionResult DeleteSection(int id)
		{
			var delete = _unitOfWork.Sections.Get(id);

			_unitOfWork.Sections.Remove(delete);
			_unitOfWork.Complete();

			return RedirectToAction("EditDeleteForum", "Admin");
		}

		public ActionResult EditSection(int id)
		{
			var edit = _unitOfWork.Sections.Get(id);
			var model = new PanelView()
            {
                Name = edit.Name,
                Description = edit.Description,
                Groups = _unitOfWork.Repository<Group>().GetAll(),
                SelectedGroup = edit.Group.GroupName,
				SelectedSection = _unitOfWork.Sections.GetSelectedSectionName(edit),
                editedSectionId = edit.SectionId
			};
			var sectionNames = _unitOfWork.Sections.Find(x => x.GroupId == edit.GroupId);
            
            ViewBag.Sections = sectionNames;

			return View(model);
		}

        [HttpPost]
        public ActionResult EditSection(PanelView panelView)
        {

            //dodaj przed wybranym
            var editedSection = _unitOfWork.Sections.Get(panelView.editedSectionId);
			var groupid = (Convert.ToInt32(panelView.SelectedGroup));
			var group = _unitOfWork.Repository<Group>().Get(groupid);
            if (editedSection.Name == panelView.SelectedSection)
            {
                editedSection.Group = group;
                editedSection.Description = panelView.Description;
                editedSection.Name = panelView.Name;
            }
            else
            {
                var sectionid = Convert.ToInt32(panelView.SelectedSection);
				var section = _unitOfWork.Sections.Get(sectionid);
                var order = section.Order;

				var items = _unitOfWork.Sections.Find(x => x.Order >= order);

				foreach (var item in items)
                {
                    item.Order = item.Order + 1;
                }

				editedSection.Order = order;
				editedSection.Group = group;
                editedSection.Description = panelView.Description;
                editedSection.Name = panelView.Name;
				
			}
			_unitOfWork.Complete();

            return RedirectToAction("Index", "Home");
        }

		public ActionResult Privileges()
		{
			var users = _unitOfWork.Repository<User>().GetAll();
			return View(users);
		}

		public ActionResult PrivilegesDetail(int id)
		{
			var model = new PrivilegesView()
			{
				User = _unitOfWork.Repository<User>().Get(id),
				Sections = _unitOfWork.Sections.GetAll(),
				Ranks = _unitOfWork.Repository<Rank>().GetAll(),

			};
			return View(model);
		}

		[HttpPost]
		public ActionResult PrivilegesDetail(PrivilegesView privilegesView,int id)
		{
			var user = _unitOfWork.Repository<User>().Get(id);
			if(privilegesView.SelectedRank != null)
			{
				var rankForUser = _unitOfWork.Repository<Rank>().Get((int)privilegesView.SelectedRank);
				user.Rank = (Rank)rankForUser;
			}

			foreach (var role in _unitOfWork.Repository<Role>().GetAll())
			{
				user.Role.Remove(role);
			}
			
			if (privilegesView.SelectedRoles == null)
			{

				ModelState.AddModelError("Warning no roles", "At least one role must be chosen for user : "+user.Username);
			}
			else
			{
				foreach (var role in privilegesView.SelectedRoles)
				{
					var rolex = _unitOfWork.Repository<Role>().Find(r => r.RoleName == role).First();
					user.Role.Add(rolex);


				}
				
				foreach (var section in _unitOfWork.Sections.GetAll())
				{
					user.Section.Remove(section);
				}

				if(privilegesView.UserSections != null)
				{
					foreach (var section in privilegesView.UserSections)
					{
						var idx = Convert.ToInt16(section);
						var sectionx = _unitOfWork.Sections.Get(idx);
						user.Section.Add(sectionx);

					}
				}
				_unitOfWork.Complete();
				
			}
			
			var model = new PrivilegesView()
			{
				User = _unitOfWork.Users.Get(id),
				Sections = _unitOfWork.Sections.GetAll(),
				UserSections = privilegesView.UserSections,

			};
			return RedirectToAction("PrivilegesDetail","Admin",model);
		}

		public ActionResult Announcement()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Announcement(Announcement model)
		{
			if (model.ExpirationDate <= DateTime.Now && !(model.ExpirationDate.Equals(DateTime.MinValue)))
			{
				ModelState.AddModelError("TimeError", "We can't go back to the past");
				return View("Announcement");
			}
			if (ModelState.IsValid)
			{
				var announcement = new Announcement()
				{
					Content = model.Content,
					CreateDate = DateTime.Now,
					ExpirationDate = model.ExpirationDate,
				};

				_unitOfWork.Repository<Announcement>().Add(announcement);
				_unitOfWork.Complete();
				return RedirectToAction("Index", "Home");

			}

			return View("Announcement");

		}

		public ActionResult ForbiddenWords()
		{
			var words = _unitOfWork.Repository<ForbiddenWord>().GetAll();
			var model = new ForbiddenWordViewModel()
			{
				forbiddenWords = words,
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult ForbiddenWords(ForbiddenWordViewModel model)
		{

			var words = _unitOfWork.Repository<ForbiddenWord>().GetAll();
			
			foreach (var x in words)
			{
				if(x.Word == model.word)
				{
					model.forbiddenWords = words;
					ModelState.AddModelError("duplicate", "Given word is already in the list");
					return View("ForbiddenWords",model);
				}
			}

			if(ModelState.IsValid)
			{
				var word = new ForbiddenWord()
				{
					Word = model.word,
				};

				_unitOfWork.Repository<ForbiddenWord>().Add(word);
				_unitOfWork.Complete();
			}
			return RedirectToAction("ForbiddenWords", model);
		}

		public ActionResult DeleteWord(int? id)
		{
			if (id == null) return HttpNotFound();
			var word = _unitOfWork.Repository<ForbiddenWord>().Get((int)id);
			_unitOfWork.Repository<ForbiddenWord>().Remove(word);
			_unitOfWork.Complete();
			return RedirectToAction("ForbiddenWords");
		}

		public JsonResult GetSection(int group)
		{
			var sections = _unitOfWork.Sections.GetOrderedSections(group);
			
			return Json(sections, JsonRequestBehavior.AllowGet);
		}

	}
}