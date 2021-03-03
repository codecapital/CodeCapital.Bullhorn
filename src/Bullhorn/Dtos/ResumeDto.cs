using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Dtos
{
    public class ResumeDto
    {
        public ParsedCandidateDto Candidate { get; set; }

        public List<CandidateEducationDto> CandidateEducation { get; set; }

        public List<CandidateResumeWorkHistoryDto> CandidateWorkHistory { get; set; }

        public ResumeDto()
        {
            CandidateEducation = new List<CandidateEducationDto>();
            CandidateWorkHistory = new List<CandidateResumeWorkHistoryDto>();
            Candidate = new ParsedCandidateDto();
        }

    }
}
