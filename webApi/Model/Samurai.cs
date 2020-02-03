namespace webApi.Model
{
    public class Samurai
    {
        public Samurai()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public SecretIdentity SecretIdentity { get; set; }

        public string Age { get; set; }

        public int Killing { get; set; }

    }
}
