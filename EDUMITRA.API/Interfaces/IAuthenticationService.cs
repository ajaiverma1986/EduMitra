using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Datamodel.Shared;
using System.Threading.Tasks;

namespace EDUMITRA.API.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ErrorResponse> IsValidToken(IEDUMITRAServiceUser FIAUser, bool IKnoeeAPIUser = true, bool slidingExpiration = true);
        Task<ErrorResponse> Validate(IEDUMITRAServiceUser FIAUser, bool validateBothToken = true, bool slidingExpiration = true);
    }
}
