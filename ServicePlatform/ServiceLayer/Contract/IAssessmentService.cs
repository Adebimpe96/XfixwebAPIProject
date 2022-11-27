using ServicePlatform.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Contract
{
    public interface IAssessmentService
    {
        Task<List<Assessment>> GetAll();
        Task<Assessment> GetByAssessmentById(int assessmentId);
    }
}
