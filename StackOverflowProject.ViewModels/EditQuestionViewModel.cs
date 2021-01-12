using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StackOverflowProject.ViewModels
{
    public class EditQuestionViewModel
    {
        public int QuestionID { get; set; }
        public string QuestionName{ get; set; }
        public DateTime QuestionDateAndTime { get; set; }
        public int CategoryID { get; set; }

    }
}
