namespace JogoGourmet.Model
{
    public class Prato
    {
        public string Descricao { get; set; }
        public string Caracteristica { get; set; }
        public string Categoria { get; set; }

        public Prato(string descricao, string caracteristica, string categoria)
        {
            Descricao = descricao;
            Caracteristica = caracteristica;
            Categoria = categoria;
        }
    }
}