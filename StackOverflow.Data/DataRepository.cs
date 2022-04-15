using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class DataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Question> GetQuestions()
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Questions.Include(q => q.Likes).Include(q => q.Answers).Include(a => a.QuestionTags).ThenInclude(n => n.Tag).OrderByDescending(i => i.Date).ToList();
        }

        public void AddUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            using var context = new StackOverflowDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new StackOverflowDataContext(_connectionString);

            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }
           
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isValid ? user : null;

        }

        public User GetByEmail(string email)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Users.FirstOrDefault(x => x.EmailAddress == email);
        }

        public void AddQuestion(Question question,List<string> questionTags)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();
            foreach(var tag in questionTags)
            {
                int tagId;
                var tagGotten=GetTag(tag);
                if (tagGotten != null)
                {
                    tagId = tagGotten.Id;
                }
                else
                {
                    tagId = AddTag(tag);
                }
                AddQuestionTag(new QuestionTags { QuestionId = question.Id, TagId = tagId });
                context.SaveChanges();
            }
        }
        public Question GetQuestion(int id)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            return context.Questions.Include(q => q.Likes).Include(q => q.Answers).ThenInclude(n=>n.user).Include(i=>i.QuestionTags).ThenInclude(n=>n.Tag).FirstOrDefault(i => i.Id == id);
        }
        private Tag GetTag(string name)
        {
            using var sodc = new StackOverflowDataContext(_connectionString);
            return sodc.Tags.FirstOrDefault(t => t.Name == name);
        }
        private int AddTag(string name)
        {
            using var sodc = new StackOverflowDataContext(_connectionString);
            var tag = new Tag();
            tag.Name = name;
            sodc.Tags.Add(tag);
            sodc.SaveChanges();
            return tag.Id;
        }
        private void AddQuestionTag(QuestionTags questionTags)
        {
            using var sodc = new StackOverflowDataContext(_connectionString);
            sodc.QuestionTags.Add(questionTags);
            sodc.SaveChanges();
        }
        public void AddLike(int QuestionId,int UserId)
        {
            using var sodc = new StackOverflowDataContext(_connectionString);
            sodc.Likes.Add(new Like { QuestionId = QuestionId, UserId = UserId });
            sodc.SaveChanges();
        }
        public List<Like> GetLikes(int id)
        {
            using var sodc = new StackOverflowDataContext(_connectionString);
            return sodc.Likes.Where(x => x.QuestionId == id).ToList();
        }
        public bool AlreadyLiked(int QuestionId,int UserId)
        {
            using var Context = new StackOverflowDataContext(_connectionString);
            return Context.Likes.Any(x => x == new Like { QuestionId = QuestionId, UserId = UserId });
        }
        public void AddAnswer(Answer answer)
        {
            using var context = new StackOverflowDataContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
    }
}
