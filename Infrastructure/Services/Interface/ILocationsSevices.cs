using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interface
{
    public interface ILocationsSevices
    {
        public Task<IEnumerable<SessionViewModel>> GetAllSessions();
        public Task<IEnumerable<RoleViewModel>> GetAllRoles();
        public Task<IEnumerable<EmployeeViewModel>> GetAllFormMastersAsync();
        public Task<IEnumerable<SectionViewModel>> GetAllSectionAsync();
        public Task<IEnumerable<ProgramViewModel>> GetAllProgramAsync();
        public Task<IEnumerable<StateViewModel>> GetStatesAsync();
        public Task<IEnumerable<LocalGovernmentViewModel>> GetLocalGovernmentsAsync(Guid stateId);
    }
}
