using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YSManagmentSystem.DAL.Data;
using YSManagmentSystem.Domain.Product;
using YSManagmentSystem.web.Models.DataTable;

namespace YSManagmentSystem.BLL.Categories
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IDapperRepo _dapper;
        public CategoryServices(IDapperRepo dapper)
        {
            _dapper = dapper;
        }

      
        public int AddCategory(Category model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@CategoryName", model.CategoryName);
            param.Add("@Description", model.Description);

            var result = _dapper.CreateUserReturnInt("dbo.AddCategory", param);
            if (result > 0)
            { }
            return result;
        }
        public int UpdateCategory(Category model)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", model.Id);
            param.Add("@CategoryName", model.CategoryName);
            param.Add("@Description", model.Description);

            var result = _dapper.CreateUserReturnInt("dbo.UpdateCategory", param);
            if (result > 0)
            { }
            return result;
        }
        public Category GetCategoryByID(int Id)
        {

            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", Id);
            var cat = _dapper.ReturnList<Category>("dbo.GetCategoryByID", param).FirstOrDefault();

            return cat;
        }
        public int DeleteCategory(int id)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var cat = _dapper.CreateUserReturnInt("dbo.DeleteCategory", param);

            return cat;
        }

        public async Task<DataTableResponse<CategoryDetail>> GetAllCategoryDT(DTReq request)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("SearchText", request.SearchText, DbType.String);
            param.Add("SortExpression", request.SortExpression, DbType.String);
            param.Add("StartRowIndex", request.StartRowIndex, DbType.Int32);
            param.Add("PageSize", request.PageSize, DbType.Int32);

            var sup = _dapper.ReturnCategoryListMultiple("GetAllCategoryDT", param);
            var Response = new DataTableResponse<CategoryDetail>()
            {
                draw = request.draw,
                data = sup.Result.Rec,
                recordsFiltered = sup.Result.TotalRecord,
                recordsTotal = sup.Result.TotalRecord,

            };
            return Response;
        }
    }
}
