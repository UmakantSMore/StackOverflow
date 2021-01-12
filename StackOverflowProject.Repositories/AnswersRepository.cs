using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int AnswerID, int UserID, int Value);
        void DeleteAnswer(int AnswerID);
        List<Answer> GetAnswersByQuestionID(int QuestionID);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }
    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;

        public AnswersRepository() {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void DeleteAnswer(int AnswerID)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == AnswerID).FirstOrDefault();
            if (ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByAnswerID(int AnswerID)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.AnswerID == AnswerID).ToList();
            return ans;
        }

        public List<Answer> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> ans = db.Answers.Where(temp => temp.QuestionID == QuestionID).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return ans;
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if (ans != null) {
                ans.AnswerText = a.AnswerText;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int AnswerID, int UserID, int Value)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == AnswerID).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount += Value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionID, Value);
                vr.UpdateVote(AnswerID, UserID, Value);
            }
        }
    }
}
