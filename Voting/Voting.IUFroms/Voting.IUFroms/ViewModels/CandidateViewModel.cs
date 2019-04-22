using System;
using System.Collections.Generic;
using System.Text;
using Voting.Common.Model;

namespace Voting.IUFroms.ViewModels
{
    public class CandidateViewModel
    {
        private readonly Events events;
        public CandidateViewModel(Events events)
        {
            this.events = events;
        }
    }
}
