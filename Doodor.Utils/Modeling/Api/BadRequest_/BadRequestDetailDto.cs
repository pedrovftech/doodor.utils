namespace Doodor.Utils.Utilities.Modeling.Api
{
    public class BadRequestDetailDto : Dto
    {
        private string _tag;

        public BadRequestDetailDto() { }
        public BadRequestDetailDto(string descricao, string tag)
        {
            Descricao = descricao;
            Tag = tag;
        }

        public string Descricao { get; set; }
        public string Tag
        {
            // Sempre retorna algo para poder agrupar itens não tagueados.
            get => string.IsNullOrEmpty(_tag) ? "__geral__" : _tag;
            set => _tag = value;
        }
    }
}