using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS.REPO.Repository.Employee
{
    public class EmployeeSkillRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeSkillRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public List<SkillViewDropdownModel> GetDropdownSkills()
        {
            return (from s in _conn.REC_Skill
                    select new SkillViewDropdownModel
                    {
                        SkillId = s.id,
                        FunctionId = s.function_id,
                        SkillType = s.skill_type,
                        SkillDescription = s.Skill_description,
                        SkillDetails = s.Skill_details,
                        Status = s.status,
                        UserId = s.user_id,
                        DateCreated = s.date_created
                    }).OrderBy(x => x.SkillDescription).ToList();
        }

        public List<SkillViewDropdownModel> GetSkills()
        {
            return (from s in _conn.REC_Skill
                    select new SkillViewDropdownModel
                    {
                        SkillId = s.id,
                        FunctionId = s.function_id,
                        SkillType = s.skill_type,
                        SkillDescription = s.Skill_description,
                        SkillDetails = s.Skill_details,
                        Status = s.status,
                        UserId = s.user_id,
                        DateCreated = s.date_created
                    }).OrderBy(x => x.SkillDescription).ToList();
        }

        public SkillModel GetSkill(int _id)
        {
            return (from x in _conn.REC_CANDIDATE_SKILL
                    where x.id == _id
                    select x
                ).AsEnumerable()
                .Select(d => new SkillModel()
                {
                    Id = int.Parse(d.id.ToString()),
                    CandidateId = d.candidate_id,
                    SkillId = d.skill_id,
                    SkillName = d.REC_Skill.Skill_description,                    
                    UserId = d.user_id,
                    CreatedBy = d.SYS_USER.username,
                    DateCreated = d.date_created
                }).SingleOrDefault();
        }

        public List<SkillViewModel> GetSkills(int _empid)
        {
            return (from d in _conn.REC_CANDIDATE_SKILL
                    join s in _conn.REC_Skill on d.skill_id equals s.id
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.candidate_id equals l.candidate_id
                    where l.emp_id == _empid
                    select d).AsEnumerable()
                      .Select(x => new SkillViewModel()
                      {
                          Id = x.id,    
                          SkillName = x.REC_Skill.Skill_description,
                          SkillType = x.REC_Skill.skill_type,
                          Remarks = x.REC_Skill.Skill_details
                      }).ToList();
        }

        public int Manage(SkillModel _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.USP_I_MANAGE_CANDIDATE_SKILL(
                _model.Id,
                _model.CandidateId,
                _model.SkillId,
                _model.Mode,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
    }
}