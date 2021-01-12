using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StackOverflowProject.ServiceLayer;

namespace StackOverflowProject.ApiControllers
{
    public class QuestionsController : ApiController
    {
        
        IAnswersService ans;

        public QuestionsController(IAnswersService ans)
        {
            this.ans = ans;
        }

        public void Post(int AnswerID, int UserID, int Value)
        {
            this.ans.UpdateAnswerVotesCount(AnswerID, UserID, Value);

        }
        

    }
}
