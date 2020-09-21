using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System;

namespace Cynthia.Card
{
    [CardEffectId("70060")]//装甲蟹蜘蛛 ArmoredArachas
    public class ArmoredArachas : CardEffect,IHandlesEvent<AfterCardConsume>
    {//吞噬1个友军单位，获得其战力作为增益。使吞噬自身的单位获得额外的等同于自身战力的增益。
        public ArmoredArachas(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying,bool isReveal)
		{
            var card = (await Game.GetSelectPlaceCards(Card, selectMode: SelectModeType.MyRow)).SingleOrDefault();
            if (card != default)
            {
                await Card.Effect.Consume(card);
            }
            return 0;
        }
        public async Task HandleEvent(AfterCardConsume @event)
        {
            if (@event.Target != Card)
            {
                return;
            }
            await @event.Source.Effect.Boost(Card.CardPoint(), Card);
        }
    }
}
