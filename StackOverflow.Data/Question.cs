using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<QuestionTags> QuestionTags { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Like> Likes { get; set; }
    }
}
