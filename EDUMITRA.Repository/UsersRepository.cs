using EDUMITRA.Database;
using EDUMITRA.Datamodel.Entities.Users;
using EDUMITRA.Datamodel.Interfaces;
using EDUMITRA.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Repository
{
    public  class UsersRepository:BaseRepository
    {
        public readonly IEDUMITRADatabase _database = null;
        public UsersRepository()
        {
            _database = new EDUMITRADatabase();
        }
        public async Task<List<ApplicationParentMenuResponse>> GetAllMenu(IEDUMITRAServiceUser serviceUser)
        {
            List<ApplicationParentMenuResponse> response = new List<ApplicationParentMenuResponse>();

            var dbCommand = _database.GetStoredProcCommand("[AAC].GetMenuDetail");

            _database.AddInParameter(dbCommand, "@UserMasterID", serviceUser.UserMasterID);
            _database.AddInParameter(dbCommand, "@ApplicationID", serviceUser.ApplicationID);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    ApplicationParentMenuResponse row = new ApplicationParentMenuResponse();

                    row.MenuID = GetInt32Value(dataReader, "MenuID").Value;
                    row.DisplayOrder = GetInt32Value(dataReader, "DisplayOrder").Value;

                    row.Target = GetStringValue(dataReader, "Target");
                    row.Tooltip = GetStringValue(dataReader, "Tooltip");
                    row.Description = GetStringValue(dataReader, "Description");
                    row.Title = GetStringValue(dataReader, "Title");
                    row.RoutePath = GetStringValue(dataReader, "RoutePath");

                    List<ApplicationMenuResponse> mm = new List<ApplicationMenuResponse>();

                    var dbCommand1 = _database.GetStoredProcCommand("[AAC].GetSubMenuDetail");

                    _database.AddInParameter(dbCommand1, "@UserMasterID", serviceUser.UserMasterID);
                    _database.AddInParameter(dbCommand1, "@ApplicationID", serviceUser.ApplicationID);
                    _database.AddInParameter(dbCommand1, "@ParentId", row.MenuID);
                    using (var dataReader1 = await _database.ExecuteReaderAsync(dbCommand1))
                    {
                        while (dataReader1.Read())
                        {
                            ApplicationMenuResponse xx = new ApplicationMenuResponse();
                            xx.MenuID = GetInt32Value(dataReader1, "MenuID").Value;
                            xx.ParentID = GetInt32Value(dataReader1, "ParentID").Value;
                            xx.DisplayOrder = GetInt32Value(dataReader1, "DisplayOrder").Value;

                            xx.Target = GetStringValue(dataReader1, "Target");
                            xx.Tooltip = GetStringValue(dataReader1, "Tooltip");
                            xx.Description = GetStringValue(dataReader1, "Description");
                            xx.Title = GetStringValue(dataReader1, "Title");
                            xx.RoutePath = GetStringValue(dataReader1, "RoutePath");

                            mm.Add(xx);
                        }
                    }
                    row.submenu = mm;

                    response.Add(row);
                }
            }
            return response;
        }

        public async Task<List<ApplicationMenuResponse>> GetAllSubMenu(int Menuid, IEDUMITRAServiceUser serviceUser)
        {
            List<ApplicationMenuResponse> response = new List<ApplicationMenuResponse>();

            var dbCommand = _database.GetStoredProcCommand("[AAC].GetSubMenuDetail");

            _database.AddInParameter(dbCommand, "@UserMasterID", serviceUser.UserMasterID);
            _database.AddInParameter(dbCommand, "@ApplicationID", serviceUser.ApplicationID);
            _database.AddInParameter(dbCommand, "@ParentId", Menuid);

            using (var dataReader = await _database.ExecuteReaderAsync(dbCommand))
            {
                while (dataReader.Read())
                {
                    ApplicationMenuResponse row = new ApplicationMenuResponse();

                    row.MenuID = GetInt32Value(dataReader, "MenuID").Value;
                    row.ParentID = GetInt32Value(dataReader, "ParentID").Value;
                    row.DisplayOrder = GetInt32Value(dataReader, "DisplayOrder").Value;

                    row.Target = GetStringValue(dataReader, "Target");
                    row.Tooltip = GetStringValue(dataReader, "Tooltip");
                    row.Description = GetStringValue(dataReader, "Description");
                    row.Title = GetStringValue(dataReader, "Title");
                    row.RoutePath = GetStringValue(dataReader, "RoutePath");

                    response.Add(row);
                }
            }
            return response;
        }
    }
}
