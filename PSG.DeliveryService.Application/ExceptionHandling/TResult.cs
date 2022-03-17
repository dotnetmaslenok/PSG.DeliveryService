namespace PSG.DeliveryService.Application.ExceptionHandling;

public class TResult
{
    public bool Succeded { get; }

    public TResult(bool succeded)
    {
        Succeded = succeded;
    }
}