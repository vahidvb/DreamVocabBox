namespace Entities.Model
{
    public class BaseModel<Tkey>
    {
        public Tkey Id { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public DateTime RegisterDate { get; set; }

        public BaseModel()
        {
            RegisterDate = DateTime.Now;
        }
    }
}