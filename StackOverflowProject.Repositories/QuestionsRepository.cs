using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int QuestionID, int Value);
        void UpdateQuestionAnswersCount(int QuestionID, int Value);
        void UpdateQuestionViewsCount(int QuestionID, int Value);
        void DeleteQuestion(int QuestionID);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int QuestionID);
    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDatabaseDbContext db;
        public QuestionsRepository() {
            db = new StackOverflowDatabaseDbContext();
        }

        public void DeleteQuestion(int QuestionID)
        {
            Question ques = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (ques != null)
            {
                db.Questions.Remove(ques);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestionByQuestionID(int QuestionID)
        {
            List<Question> ques = db.Questions.Where(temp => temp.QuestionID == QuestionID).ToList();
            return ques;
        }

        public List<Question> GetQuestions()
        {
            List<Question> ques = db.Questions.OrderByDescending(temp => temp.QuestionDateAndTime).ToList();
            return ques;
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionAnswersCount(int QuestionID, int Value)
        {
            Question ques = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (ques != null)
            {
                ques.AnswersCount += Value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question ques = db.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if (ques != null)
            {
                ques.QuestionName = q.QuestionName;
                ques.QuestionDateAndTime = q.QuestionDateAndTime;
                ques.CategoryID = q.CategoryID;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int QuestionID, int Value)
        {
            Question ques = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (ques != null)
            {
                ques.ViewsCount += Value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int QuestionID, int Value)
        {
            Question ques = db.Questions.Where(temp => temp.QuestionID == QuestionID).FirstOrDefault();
            if (ques != null)
            {
                ques.VotesCount += Value;
                db.SaveChanges();
            }
        }
    }
}
