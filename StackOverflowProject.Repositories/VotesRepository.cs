using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
namespace StackOverflowProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int AnswerID, int UserID, int Value);
    }

    public class VotesRepository : IVotesRepository
    {
        
        StackOverflowDatabaseDbContext db;
        public VotesRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void UpdateVote(int AnswerID, int UserID, int Value)
        {
            int updateValue = 0;
            if (Value > 0)
                updateValue = 1;
            else if (Value < 0)
                updateValue = -1;
            else
                updateValue = 0;

            Vote vote = db.Votes.Where(temp => temp.AnswerID == AnswerID && temp.UserID == UserID).FirstOrDefault();
            if (vote != null)
            {
                vote.VoteValue = updateValue;
            }
            else
            {
                Vote newVote = new Vote() { AnswerID = AnswerID, UserID = UserID, VoteValue = updateValue };
                db.Votes.Add(newVote);
            }
            db.SaveChanges();
        }
    }
}
