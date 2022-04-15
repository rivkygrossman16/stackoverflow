using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Data
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public User user { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
