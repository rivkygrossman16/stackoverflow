using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackOverflow.Data;
using StackOverflow.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new DataRepository(_connectionString);

            var Questions = repo.GetQuestions();
            foreach (var qs in Questions)
            {
                if (qs.Body.Length >= 200)
                {
                    string s = qs.Body;
                    var part = s.Substring(0, 200);
                    part += "...";
                    qs.Body = part;

                };
            }
                QuestionViewModel vm = new QuestionViewModel
                {
                    Questions = Questions
                };

                return View(vm);
            
        }
        [Authorize]
        public IActionResult Question()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Question question,List<string> tags)
        {
            var email=User.Identity.Name;
            question.Date = DateTime.Now;
            var repo = new DataRepository(_connectionString);
            var user = repo.GetByEmail(email);
            question.Name = user.Name;
            repo.AddQuestion(question,tags);
            return Redirect("/");
        }
        public IActionResult ViewQuestion(int id)
        {
            var repo = new DataRepository(_connectionString);
            var ques = repo.GetQuestion(id);
            QuesViewModel QuesViewModel = new QuesViewModel();
            if(User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var user = repo.GetByEmail(email);
                var Liked = repo.AlreadyLiked(id, user.Id);
                QuesViewModel.LikedAlready = Liked;
                QuesViewModel.LoggedIn = true;
            }
            else
            {
                QuesViewModel.LoggedIn = false;
            }
            var likes = repo.GetLikes(id);
            QuesViewModel.Question = ques;
            QuesViewModel.NumLikes = likes.Count;

            return View(QuesViewModel);
        }
        [HttpPost]
        public IActionResult Like(int QuestionId)
        {
            var repo = new DataRepository(_connectionString);
            var email = User.Identity.Name;
            var user = repo.GetByEmail(email);
            repo.AddLike(QuestionId, user.Id);
            return Json(QuestionId);
        }
        public IActionResult GetLikes(int id)
        {
            var repo = new DataRepository(_connectionString);
            var Likes = repo.GetLikes(id);
            return Json(Likes.Count);
        }
        public IActionResult AddAnswer(Answer anwer)
        {
            var repo = new DataRepository(_connectionString);
            var email = User.Identity.Name;
            var user = repo.GetByEmail(email);
            anwer.UserId = user.Id;
            anwer.Date = DateTime.Now;
            repo.AddAnswer(anwer);
            return Redirect("/home/index");
        }

    }

}
