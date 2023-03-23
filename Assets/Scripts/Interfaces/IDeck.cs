using PokerRandomDefense.GamePlay;

public interface IDeck
{
    int Count {get;}
    Card Draw();
    void Insert(Card card);
}
