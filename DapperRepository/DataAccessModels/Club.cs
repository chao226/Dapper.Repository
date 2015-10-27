using System.ComponentModel;

namespace DataAccessModels
{
    public class Club : BaseModel
    {
        public Club()
        {
            TableName = "clubs";
        }

        [DisplayName("name")]
        public string Name { get; set; }

        [SearchField("clubId")]
        public int clubId { get; set; }

    }
}
