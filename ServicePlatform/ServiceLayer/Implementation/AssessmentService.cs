using Microsoft.EntityFrameworkCore;
using ServicePlatform.DataLayer;
using ServicePlatform.DataLayer.Models;
using ServicePlatform.ServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicePlatform.ServiceLayer.Implementation
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ApiContext _context;

        public AssessmentService(ApiContext context)
        {
            _context = context;
        }
        
        public async Task<List<Assessment>> GetAll()
        {
            try
            {
                var assessments = await _context.Assessments.ToListAsync();

                return assessments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Assessment> GetByAssessmentById(int assessmentId)
        {
            try
            {
                var assessment = await _context.Assessments
                    .Where(x => x.AssessmentId == assessmentId).FirstOrDefaultAsync();

                return assessment;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
