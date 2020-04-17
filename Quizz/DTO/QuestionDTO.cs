using System.Collections.Generic;
using Quizz.Enums;

namespace Quizz.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public IList<AnswerDTO> Answers { get; set; }
    }
}
