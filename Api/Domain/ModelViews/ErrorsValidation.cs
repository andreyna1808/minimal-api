namespace minimal_api.Dominio.ModelViews;

public struct ErrorsValidation
{
    public List<string> Mensagens { get; set; }

    public ErrorsValidation()
    {
        Mensagens = new List<string>();
    }
}
