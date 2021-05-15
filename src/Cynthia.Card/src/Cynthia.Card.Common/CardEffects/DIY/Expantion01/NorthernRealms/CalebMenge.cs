using System.Linq;
using System.Threading.Tasks;
using Alsein.Extensions;
using System.Collections.Generic;

namespace Cynthia.Card
{
    [CardEffectId("70079")]//迦勒·曼吉 CalebMenge
    public class CalebMenge : CardEffect
    {//摧毁一个友方“法师”单位，获得等同于其基础战力的强化。若为银色单位，额外获得3点强化。若为金色单位，额外获得5点强化。
        public CalebMenge(GameCard card) : base(card) { }
        public override async Task<int> CardPlayEffect(bool isSpying, bool isReveal)
        {
            var selectCards = await Game.GetSelectPlaceCards(Card,
                filter: x =>  x.HasAnyCategorie(Categorie.Mage),
                selectMode: SelectModeType.MyRow);
            if (!selectCards.TrySingle(out var targetCard))
            {
                return 0;
            }
            await Card.Effect.Strengthen(targetCard.Status.Strength,Card);
            if(targetCard.CardInfo().IsAnyGroup(Group.Silver))
                await Card.Effect.Strengthen(3,Card);
            else if(targetCard.CardInfo().IsAnyGroup(Group.Gold))
                await Card.Effect.Strengthen(5,Card);
            await targetCard.Effect.ToCemetery(CardBreakEffectType.Scorch);
            return 0;
        }
    }
}