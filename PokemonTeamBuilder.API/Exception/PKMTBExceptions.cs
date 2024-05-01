namespace PokemonTeamBuilder.API.Exceptoins;
public class EmptyListException:Exception{
    public EmptyListException(){}
    public EmptyListException(string message):base(message){}
    public EmptyListException(string message, Exception inner):base(message, inner){}    
}

public class PkmTeamSizeException:Exception
{
    public PkmTeamSizeException(){}
    public PkmTeamSizeException(string message):base(message){}
    public PkmTeamSizeException(string message, Exception inner):base(message, inner){}
}