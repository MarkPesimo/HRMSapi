using HRModel.ViewModel.Global;
using HRMS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HRModel.ViewModel.Client.Client_model;

namespace HRMS_API.Repository
{
    public class ClientProfileRepository
    {
        private apwdbEntities _conn { get; set; }
        private GlobalRepository _globalrepository { get; set; }

        public ClientProfileRepository()
        {
            if (_conn == null) { _conn = new apwdbEntities(); }
            if (_globalrepository == null) { _globalrepository = new GlobalRepository(); }
        }

        public int GetClientId(string _guid)
        {
            return (from d in _conn.REC_CLIENT where d.guid == _guid select d.id).SingleOrDefault();
        }

        public List<ClientMonitoring_model> GetMonitoring(int _appid, string _keyword, int _companyid)
        {
            List<ClientMonitoring_model> _obj = new List<ClientMonitoring_model>();

            if (_appid == SystemVariable_model.AppModuleId.HRMS)
            {
                _obj = (from x in _conn.REC_CLIENT
                        join cc in _conn.REC_CLIENT_COMPANY on x.id equals cc.client_id
                        join i in _conn.INDUSTRies on x.industry_id equals i.id
                        where cc.company_id == _companyid
                        select x
                           ).AsEnumerable()
                           .Select(d => new ClientMonitoring_model()
                           {
                               Id = int.Parse(d.id.ToString()),
                               ClientGUID = d.guid,
                               ClientName = d.client_name,
                               ContactPerson = d.contact_person,
                               ContactNumber = d.contact_no + " / " + d.mobile_no,
                               EmailAddress = d.email_address,
                               TINno = d.tin_no,
                               Industry = d.INDUSTRY.industry_name,
                               Status = d.status == true ? "Active" : "Inactive",
                               CreatedBy = d.SYS_USER.username,
                               DateCreated = d.date_encoded.Value.ToShortDateString()
                           }).ToList();
            }
            else
            {
                _obj = (from x in _conn.REC_CLIENT
                        join cc in _conn.REC_CLIENT_COMPANY on x.id equals cc.client_id
                        join i in _conn.INDUSTRies on x.industry_id equals i.id
                        select x
                          ).AsEnumerable()
                          .Select(d => new ClientMonitoring_model()
                          {
                              Id = int.Parse(d.id.ToString()),
                              ClientGUID = d.guid,
                              ClientName = d.client_name,
                              ContactPerson = d.contact_person,
                              ContactNumber = d.contact_no + " / " + d.mobile_no,
                              EmailAddress = d.email_address,
                              TINno = d.tin_no,
                              Industry = d.INDUSTRY.industry_name,
                              Status = d.status == true ? "Active" : "Inactive",
                              CreatedBy = d.SYS_USER.username,
                              DateCreated = d.date_encoded.Value.ToShortDateString()
                          }).ToList();
            }


            return _obj;
        }

        public ClientProfile_model Get(int _id)
        {
            return (from x in _conn.REC_CLIENT
                    where x.id == _id
                    select x).AsEnumerable()
                    .Select(d => new ClientProfile_model()
                    {
                        Id = d.id,
                        ClientGUID = d.guid,
                        ClientName = d.client_name,
                        ClientAddress = d.client_address,
                        BillingAddress = d.billing_address,
                        ZIPCode = d.zip_code,
                        TINno = d.tin_no,
                        IsVatable = d.Vatable,
                        VATType = d.Vat_Type,
                        IsWithHoldingTax = d.is_withholding_tax,
                        WHTRate = d.withholding_tax_rate,
                        ContactNo = d.contact_no,
                        ContactPerson = d.contact_person,
                        ContactPersonTitle = d.contact_title,
                        EmailAddress = d.email_address,
                        MobileNo = d.mobile_no,
                        Website = d.website,
                        ClientType = d.client_type,
                        IndustryId = d.industry_id,
                        IndustryName = d.INDUSTRY.industry_name,
                        Remarks = d.remarks,
                        EmployerId = d.employer_id,
                        EmployerName = d.EMPLOYER.employer_name,
                        Status = d.status ?? false,

                        UserId = d.userid,
                        CreatedBy = d.SYS_USER.username,
                        DateCreated = d.date_encoded.Value

                    }).SingleOrDefault();
        }

        public int Manage(ClientProfile_model _model)
        {
            System.Data.Entity.Core.Objects.ObjectParameter _return_value = new System.Data.Entity.Core.Objects.ObjectParameter("RET_ID", typeof(int));

            _conn.SP_I_MASTER_CLIENT(
                _model.Mode,
                _model.ClientName,
                _model.ClientAddress,
                _model.ContactNo,
                _model.EmailAddress,
                _model.ContactPerson,
                _model.Remarks,
                true,
                _model.TINno,
                _model.IndustryId,
                _model.ContactPersonTitle,
                _model.MobileNo,
                _model.Website,
                _model.ClientType,
                _model.IsWithHoldingTax,
                _model.WHTRate,
                _model.IsVatable,
                _model.VATType,
                _model.BillingAddress,
                _model.Id,
                _model.UserId,
                _return_value
            );

            return Convert.ToInt32(_return_value.Value);
        }
         
         
    }
}