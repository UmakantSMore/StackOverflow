using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetails(EditQuestionViewModel qvm);
        void UpdateQuestionVotesCount(int QuestionID, int Value);
        void UpdateQuestionAnswersCount(int QuestionID, int Value);
        void UpdateQuestionViewsCount(int QuestionID, int Value);
        void DeleteQuestion(int QuestionID);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);

    }

    public class QuestionsService : IQuestionsService
    {
        IQuestionsRepository qr;
        public QuestionsService()
        {
            qr = new QuestionsRepository();
        }

        public void DeleteQuestion(int QuestionID)
        {
            qr.DeleteQuestion(QuestionID);
        }

        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID = 0)
        {
            Question q = qr.GetQuestionByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel qvm = null;
            if (q != null)
            {
                var config = new MapperConfiguration(cfg => 
                {
                    cfg.CreateMap<Question, QuestionViewModel>();
                    cfg.CreateMap<User, UserViewModel>();
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.CreateMap<Answer, AnswerViewModel>();
                    cfg.CreateMap<Vote, VoteViewModel>();
                    cfg.IgnoreUnmapped();

                });

                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(q);

                foreach (var item in qvm.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserID == UserID).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
            }
            return qvm;
        }

        public List<QuestionViewModel> GetQuestions()
        {
            //List<Question> q = qr.GetQuestions();
            //var config = new MapperConfiguration(temp => { temp.CreateMap<Question, QuestionViewModel>(); temp.IgnoreUnmapped(); });
            //IMapper mapper = config.CreateMapper();
            //List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            //return qvm;

            List<Question> q = qr.GetQuestions();
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Question, QuestionViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<Answer, AnswerViewModel>();
                cfg.CreateMap<Vote, VoteViewModel>();

                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return qvm;

            //List<Question> q = qr.GetQuestions();
            //var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); });
            //IMapper mapper = config.CreateMapper();
            //List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            //return qvm;
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<NewQuestionViewModel, Question>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);

        }

        public void UpdateQuestionAnswersCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionAnswersCount(QuestionID, Value);
        }

        public void UpdateQuestionDetails(EditQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(temp => { temp.CreateMap<EditQuestionViewModel, Question>(); temp.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);

        }

        public void UpdateQuestionViewsCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionViewsCount(QuestionID, Value);
        }

        public void UpdateQuestionVotesCount(int QuestionID, int Value)
        {
            qr.UpdateQuestionVotesCount(QuestionID, Value);

        }
    }
}
