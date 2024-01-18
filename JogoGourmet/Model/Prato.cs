namespace JogoGourmet.Model
{
    public class Prato
    {
        public string Descricao { get; set; }
        public string Caracteristica { get; set; }

        public Prato(string descricao, string caracteristica)
        {
            Descricao = descricao;
            Caracteristica = caracteristica;
        }
    }
}