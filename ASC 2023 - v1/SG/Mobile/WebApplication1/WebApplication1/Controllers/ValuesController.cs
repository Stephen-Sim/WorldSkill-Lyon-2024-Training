using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        public sg_dbEntities ent { get; set; }
        public ValuesController()
        {
            ent = new sg_dbEntities();
        }

        [HttpGet]
        public object GetQuizzes()
        {
            var q = ent.Quizzes.ToList().Select(x => new
            {
                ID = x.QuizId,
                Name = x.Title
            });

            return (q);
        }

        [HttpGet]
        public object GetData(int quizId, int typeId, bool isExpert)
        {
            var data = ent.Attempts.ToList().Where(x => x.Question.QuizId == quizId && x.User.Role != (isExpert ? "Competitor" : "")).GroupBy(x => new
            {
                x.User,
                DateTime = DateTime.ParseExact(x.DateTime, "d/M/yyyy H:mm", null) 
            }).Select(x => new
            {
                Name = $"{x.Key.User.Name}\n({x.Key.User.Role})",
                Attempt = new Func<string>(() =>
                {
                    var count = ent.Attempts.ToList().Where(y => y.Question.QuizId == quizId
                        && y.UserId == x.Key.User.UserId
                        && DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null) < x.Key.DateTime).GroupBy(y => new
                        {
                            y.User,
                            DateTime = DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null)
                        }).Count();

                    return (count + 1).ToString();
                })(),
                Completion = new Func<string>(() =>
                {
                    var count = ent.Attempts.ToList().Where(y => y.Question.QuizId == quizId
                        && y.UserId == x.Key.User.UserId
                        && DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null) == x.Key.DateTime).Count();

                    if (count == ent.Questions.Where(y => y.QuizId == quizId).Count())
                    {
                        return "Completed";
                    }

                    return $"In progess({count * 1.0 / ent.Questions.Where(y => y.QuizId == quizId).Count() * 100}%)";
                })(),
                Grade = new Func<string>(() =>
                {
                    var count = ent.Attempts.ToList().Where(y => y.Question.QuizId == quizId
                        && y.UserId == x.Key.User.UserId
                        && DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null) == x.Key.DateTime).Count();

                    if (count == ent.Questions.Where(y => y.QuizId == quizId).Count())
                    {
                        count = ent.Attempts.ToList().Where(y => y.Question.QuizId == quizId
                            && y.UserId == x.Key.User.UserId
                            && DateTime.ParseExact(y.DateTime, "d/M/yyyy H:mm", null) == x.Key.DateTime 
                            && y.Answer == y.Question.CorrectAnswer).Count();

                        var result = (count * 1.0 / ent.Questions.Where(y => y.QuizId == quizId).Count() * 100).ToString();

                        return $"{result}%";
                    }

                    return $"-";
                })(),
                Date = x.Key.DateTime.ToString("dd MMMM yyyy h:mm tt"),
                x.Key.DateTime
            }).ToList();

            if (typeId == 1)
            {

            }
            else if (typeId == 2)
            {
                data = data.Where(x => x.DateTime >= DateTime.Today.AddDays(30)).ToList();
            }
            else if (typeId == 3)
            {
                data = data.Where(x => x.DateTime >= DateTime.Today.AddDays(60)).ToList();
            }
            else if (typeId == 4)
            {
                data = data.Where(x => x.DateTime >= DateTime.Today.AddDays(90)).ToList();
            }
            else if (typeId == 5)
            {
                data = data.Where(x => x.Completion != "Completed").ToList();
            }
            else if (typeId == 6)
            {
                var users = ent.Users.ToList().Where(x => x.Role != (isExpert ? "Competitor" : "")).ToList();

                var quiz = ent.Quizzes.First(x => x.QuizId == quizId);

                var list = new List<string>();

                foreach (var user in users)
                {
                    if (quiz.ForCompetitor == "Y" || user.Role == "Expert")
                    {
                        list.Add(user.UserId);
                    }
                }

                var attempt_users = ent.Attempts.ToList().Where(x => x.Question.QuizId == quizId && x.User.Role != (isExpert ? "Competitor" : "")).Select(x => x.User.UserId).Distinct();

                var not_attempt = list.Except(attempt_users).Select(x => new
                {
                    Name = $"{ent.Users.First(y => y.UserId == x).Name}\n({ent.Users.First(y => y.UserId == x).Role})",
                    Attempt = "0",
                    Completion = "-",
                    Grade = "-",
                    Date = "-",
                });

                return not_attempt;
            }

            return data;
        }
    }
}
