using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Common.Interfaces
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);
    }
}
