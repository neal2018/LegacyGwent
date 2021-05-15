namespace Cynthia.Card
{
    //卡牌变形后
    public class AfterTransform : Event
    {
        public GameCard Target { get; set; }
        public GameCard Source { get; set; }
        public AfterTransform(GameCard target, GameCard source)
        {
            Target = target;
            Source = source;
        }
    }
}