namespace framework.Models.Commons;

public class OperationResult
{
    public bool Success { get; set; } = true;
    public int? ErrorCode { get; set; }
    public List<SystemMessage>? Errors { get; internal set; }
    public List<SystemMessage>? Infos { get; internal set; }

    public OperationResult AddError(string code, string error)
    {
        Success = false;
        if (Errors == null)
            Errors = new List<SystemMessage>()
            {
                new SystemMessage(code, error)
            };
        else
            Errors.Add(new SystemMessage(code, error));

        return this;
    }

    public OperationResult AddInfo(string code, string error)
    {
        if (Infos == null)
        {
            Infos =new List<SystemMessage>()
            {
                new SystemMessage(code, error)
            };
        }
        else
            Infos.Add(new SystemMessage(code, error));

        return this;
    }
}

public record SystemMessage(string Code, string Error);
