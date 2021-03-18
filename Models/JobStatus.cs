using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearchOrganizer.Models
{
    public enum JobStatus
    {
        None,
        Apply,
        Applied,
        Research,
        Researched,
        SendFollowUPEmail,
        FollowUpSent,
        Interview,
        Interviewed,
        SendThankNote,
        SendFollowUP2,
        FollowUp2Sent,
        Finished
    }
}
