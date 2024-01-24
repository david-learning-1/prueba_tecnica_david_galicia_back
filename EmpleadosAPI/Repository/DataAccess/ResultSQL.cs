using System.Data;

namespace EmpleadosAPI.Repository.Common
{
    public class ResultSQL
    {
        public DataTable TblResult { get; set; }
        public string Error { get; set; }
        public int Affectedrows { get; set; }
        public int ResdulId { get; set; }
        
    }
}
