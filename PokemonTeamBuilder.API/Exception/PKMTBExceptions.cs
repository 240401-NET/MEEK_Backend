namespace PokemonTeamBuilder.API.Exceptoins;
public class EmptyListException:Exception{
    public EmptyListException(){}
    public EmptyListException(string message):base(message){}
    public EmptyListException(string message, Exception inner):base(message, inner){}
}

public class ObjectExistException:Exception{
    public ObjectExistException(){}
    public ObjectExistException(string message):base(message){}
    public ObjectExistException(string message, Exception inner):base(message, inner){}
}

public class BadNameException:Exception{
    public BadNameException(){

    }
    public BadNameException(string _message):base(_message){

    }
    public BadNameException(string _message, Exception _inner):base(_message, _inner){

    }
}