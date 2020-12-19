namespace FS.DataObject.Moviment.Request
{
    public class CreateMovimentRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
        
        public int Type { get; set; }
    }
}