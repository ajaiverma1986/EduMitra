using EDUMITRA.Datamodel.Entities.Users;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Provider.Shared;
using EDUMITRA.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Provider
{
    public class UserDetailsProvider: BaseProvider
    {
        public readonly UsersRepository _repository = null;
        public UserDetailsProvider()
        {
            _repository = new UsersRepository();
        }
        public async Task<List<ApplicationParentMenuResponse>> GetallMenu(IEDUMITRAServiceUser serviceUser)
        {
            List<ApplicationParentMenuResponse> response = new List<ApplicationParentMenuResponse>();

            response = await _repository.GetAllMenu(serviceUser);
            return response;
        }
        public async Task<List<ApplicationMenuResponse>> GetallSubMenu(int Menuid, IEDUMITRAServiceUser serviceUser)
        {
            List<ApplicationMenuResponse> response = new List<ApplicationMenuResponse>();

            response = await _repository.GetAllSubMenu(Menuid, serviceUser);
            return response;
        }
    }
}
