namespace CoffeMachine
{
    public partial class PlaceOrder
    {
        public class ComboBoxItemModel
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; 
            }

        }

       
    }
}
