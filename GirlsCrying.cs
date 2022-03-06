using Fluent;
 
public class GirlsCrying : FluentScript
{
  public override FluentNode Create()
  {
    return Yell("*sob sob*")*
    Yell("there going to hang your brother")*
    Yell("*sob* the evil wizard set him up")*
    Show()*
    Options(
      Option("Im going to help him")*
        Yell("thank you")*
        End()*
      Option("meh")*
        Yell("*sob*")*
        End()
    );
  }
}