using HelpClassLibrary.Dto;

namespace HRManage.IService
{
    public interface IStructureStatisticsService
    {
        object TestSqlSugar();
        object QueryAll(QueryParametersDto model);
    }
}
