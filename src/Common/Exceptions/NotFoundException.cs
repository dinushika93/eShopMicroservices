namespace Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message){

    }

     public NotFoundException(string entity, string key) : base($"{entity} cannot be found for {key}") 
     { 

     }

}
