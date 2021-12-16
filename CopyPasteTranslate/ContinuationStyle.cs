using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyPasteTranslate
{
    public enum ContinuationStyle
    {
        None,
        NoneTrailingDots,
        NoneEllipsisForPauses,
        NoneLeadingTrailingDots,
        OnlyTrailingDots,
        LeadingTrailingDots,
        LeadingTrailingEllipsis,
        LeadingTrailingDash,
        LeadingTrailingDashDots,
    }
}
