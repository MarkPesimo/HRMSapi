using HRModel.ViewModel.Employees;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS_API.Repository.Employee
{
    public class EmployeeEducationRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public EmployeeEducationRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }

            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public EducationalBackgroundModel GetEducation(int _id)
        {
       
            return (from d in _conn.REC_CANDIDATE_EDUCATION
                    where d.School_Id == _id
                    select d).AsEnumerable()
                  .Select(x => new EducationalBackgroundModel()
                  {
                      Id = x.School_Id,
                      CandidateId = x.candidate_id,
                      SchoolId = x.SchoolLibID,
                      SchoolName = x.School.SchoolName,
                      SchoolLevelId = x.SchoolLevelID,
                      SchoolLevel = x.SchoolLevel.SchoolLevelDescn,
                      DegreeId = int.Parse( x.DegreeID.ToString()),
                      DegreeName = x.Degree.DegreeName,
                      Awards = x.Awards,
                      FromDate = x.School_From,
                      ToDate = x.School_To,
                      Remarks = x.School_Remarks,
                      IsGraduated = bool.Parse( x.Graduated.ToString())
                  }).SingleOrDefault();
           
        }

        public List<EducationalBackgroundViewModel> GetEducations(int _empid)
        {
            return (from d in _conn.REC_CANDIDATE_EDUCATION
                    join l in _conn.REC_CANDIDATE_EMPLOYEE_LINK on d.candidate_id equals l.candidate_id
                    join sl in _conn.SchoolLevels on d.SchoolLevelID equals sl.LevelID
                    join dg in _conn.Degrees on d.DegreeID equals dg.DegreeID
                    where l.emp_id == _empid
                    select d).AsEnumerable()
                  .Select(x => new EducationalBackgroundViewModel()
                  {
                      Level = x.SchoolLevel.SchoolLevelDescn,
                      SchoolName = x.School.SchoolName,
                      Degree = x.Degree.DegreeName,
                      Period = x.School_From.Value.Year + "-" + x.School_To.Value.Year,
                      Graduate = x.Graduated.Value == true ? "Graduate" : "Under Graduate"
                  }).ToList();
        }

        public bool ManageEducation(EducationalBackgroundModel _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RETURN_ID", typeof(int));
            try
            {
                _conn.SP_I_MANAGE_EDUCATION(
                    _model.Mode,
                    _model.CandidateId,
                    _model.SchoolName,
                    _model.SchoolLevel,
                    _model.Remarks,
                    _model.DegreeName,
                    _model.FromDate,
                    _model.ToDate,
                    _model.SchoolId,
                    _model.SchoolLevelId,
                    _model.DegreeId,
                    _model.Id,
                    _model.UserId,
                    _model.Awards,
                    _model.IsGraduated
                );

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}