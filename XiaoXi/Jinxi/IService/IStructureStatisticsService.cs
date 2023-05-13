using HelpClassLibrary.Dto;

namespace Jinxi.IService
{
    public interface IStructureStatisticsService
    {
        object TestSqlSugar();
        object QueryAll(QueryParametersDto model);
    }
}
