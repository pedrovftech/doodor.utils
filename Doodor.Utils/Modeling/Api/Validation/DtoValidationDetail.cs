namespace Doodor.Utils.Utilities.Modeling.Api.Validation
{
    public class DtoValidationDetail
    {
        private string _memberName;

        public DtoValidationDetail(string message, string memberName)
        {
            Message = message;
            MemberName = memberName;
        }

        public DtoValidationDetail(string mensagem) :
            this(mensagem, null)
        { }

        public DtoValidationDetail() { } // Serialization friendly ctor

        public string Message { get; set; }
        public string MemberName
        {
            // Sempre retorna algo para poder agrupar itens não tagueados.
            get => string.IsNullOrEmpty(_memberName) ? "__geral__" : _memberName;
            set => _memberName = value;
        }
    }
}